using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Timers;

public interface ISFResUpdateMgr
{
    bool CheckIsNeedDownload(string relatePath);
    int GetResourceType(string relatePath);
    void AddToDownloadQueue(Resource res);
    void AddToCompleteQueue(Resource res);
}

public class Resource : HttpDownload
{
    private ResourceListType resListType = ResourceListType.None;
    public ResourceListType ResListType { get { return resListType; } }

    private int updateId;
    public int UpdateId { get { return updateId; } }
    private string relatePath;
    public string RelatePath { get { return relatePath; } }
    private int byteNum;
    public int ByteNum { get { return byteNum; } set { byteNum = value; } }
    private int index;
    public int Index { get { return index; } }
    private int resourceType = -1;
    public int ResType { get { return resourceType; } }
    private int needPreDownload = -1;

    public bool NeedPreDownload
    {
        get
        {
            if (needPreDownload < 0)
            {
                return relatePath.Contains("ScaleMap") ||
                    relatePath.Contains("byte") ||
                    relatePath.Contains("Android/") ||
                    relatePath.Contains("iOS/") ||
                    relatePath.Contains("SceneRes") ||
                    relatePath.Contains("ResourceRes");
            }
            else
            {
                if (needPreDownload == 1)
                    return true;
                else
                    return false;
            }
        }
    }

    public bool needCallBack = false;
    public bool needAddToBackProgress = false;

    public Resource(ResourceListType resListType, int updateId, string strData)
    {
        this.resListType = resListType;

        this.updateId = updateId;

        if (string.IsNullOrEmpty(strData))
        {
            if (Debug.developerConsoleVisible) Debug.Log("Resource construct failed, string data reference null or empty");
            return;
        }

        string[] strArr = strData.Split('#');
        if (strArr.Length >= 3)
        {
            relatePath = strArr[0];
            if (!int.TryParse(strArr[1], out byteNum))
            {
                if (Debug.developerConsoleVisible) Debug.Log("Parse resource byteNum error " + relatePath);
                return;
            }
            if (!int.TryParse(strArr[2], out index))
            {
                if (Debug.developerConsoleVisible) Debug.Log("Parse resource index error " + relatePath);
                return;
            }
        }
        if (strArr.Length >= 4)
        {
            if (!int.TryParse(strArr[3], out resourceType))
            {
                if (Debug.developerConsoleVisible) Debug.Log("Parse resource download type error" + relatePath);
            }
        }
        if (strArr.Length >= 5)
        {
            if (!int.TryParse(strArr[4], out needPreDownload))
            {
                if (Debug.developerConsoleVisible) Debug.Log("Parse needPreDownload error" + relatePath);
            }
        }
    }

    public Resource(string relatePath, int byteNum, int index, DownloadType type)
    {
        this.relatePath = relatePath;
        this.byteNum = byteNum;
        this.index = index;
        this.type = type;
        resourceType = GetResourceType(relatePath);
    }

    public Resource(string relatePath, int byteNum, int index, int resourceType, DownloadType type)
    {
        this.relatePath = relatePath;
        this.byteNum = byteNum;
        this.index = index;
        this.type = type;
        this.resourceType = resourceType;
    }

    private int GetResourceType(string relatePath)
    {
        if (string.IsNullOrEmpty(relatePath))
        {
            return -1;
        }
        else
        {
            if (relatePath.Contains("Table") ||
                relatePath.Contains("Android/") ||
                relatePath.Contains("iOS/"))
                return 1;
            else
                return 0;
        }
    }

    public override string url
    {
        get
        {
            if (resourceType == (int)ResourceType.CommonResources)
            {
                return SFOut.URL_mCommonResURL + relatePath;
            }
            else
            {
                return SFOut.URL_mServerResURL + relatePath;
            }
        }
    }

    public override string localPath { get { return SFOut.URL_mClientResPath + relatePath; } }

    protected override void FinishDownload()
    {
        SFOut.IResUpdateMgr.AddToCompleteQueue(this);
        base.FinishDownload();
    }

    protected override void Destroy() { }

    public enum ResourceType
    {
        None = -1,
        CommonResources,
        Others,
    }
}

public class HttpDownload
{
    public DownloadType type;
    public HttpWebRequest req;
    public HttpWebResponse rsp;
    protected Stream stream;
    protected FileStream fileStream;
    protected byte[] buffer;
    public bool isSucceed = false;
    public int requestTimes = 0;

    public virtual string url
    {
        get { return ""; }
    }
    public virtual string localPath
    {
        get { return ""; }
    }

    public void Download()
    {
        try
        {
            req = WebRequest.Create(url) as HttpWebRequest;
            req.Method = "GET";
            req.Timeout = 120000;
            req.ReadWriteTimeout = 120000;
            req.Proxy = null;
        }
        catch (System.Exception ex)
        {
            if (Debug.developerConsoleVisible) Debug.Log(ex.Message + "    " + url);
        }

        GetResponse();
    }

    protected void GetResponse()
    {
        try
        {
            rsp = req.GetResponse() as HttpWebResponse;

            CheckDirExist();

            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }

            if (buffer == null)
            {
                if (type == DownloadType.BackDownload || type == DownloadType.FailedDownload)
                {
                    buffer = new byte[1024];
                }
                else
                {
                    buffer = new byte[2048];
                }
            }

            using (fileStream = new FileStream(localPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                using (stream = rsp.GetResponseStream())
                {
                    int len = stream.Read(buffer, 0, buffer.Length);

                    while (len > 0)
                    {
                        fileStream.Write(buffer, 0, len);
                        fileStream.Flush();
                        len = stream.Read(buffer, 0, buffer.Length);
                    }
                }

                isSucceed = true;

                FinishDownload();
            }
        }
        catch (WebException webEx)
        {
            if (webEx.Status != WebExceptionStatus.ProtocolError)
            {
                if (Debug.developerConsoleVisible) Debug.LogError("GetResponse Error！" + webEx.Message + "  " + url);
                rsp = null;
                CloseHttp();
                NeedDownloadAgain();
            }
            else
            {
                if (Debug.developerConsoleVisible) Debug.LogError("GetResponse Exception: " + webEx.Message + "  " + url);
                rsp = null;
                FinishDownload();
            }
        }
        catch (IOException ioEx)
        {
            if (!ioEx.Message.Contains("Disk full"))
            {
                if (Debug.developerConsoleVisible) Debug.LogError("IO Exception: " + ioEx.Message + "  " + url);
            }
            CloseHttp();
            NeedDownloadAgain();
        }
    }

    protected virtual void NeedDownloadAgain()
    {
        if (requestTimes < 3 || type == DownloadType.PreDownload)
        {
            if (Debug.developerConsoleVisible) Debug.Log("Download fail " + url + ", download again");
            requestTimes++;
            if (type == DownloadType.PreDownload)
            {
                Thread.Sleep(2000);
            }
            Download();
        }
        else
        {
            if (Debug.developerConsoleVisible) Debug.Log("Download fail " + url + " more than 3 times, finish download");
            FinishDownload();
        }
    }

    protected virtual void CheckDirExist()
    {
        if (!string.IsNullOrEmpty(localPath))
        {
            string dirPath = localPath.Remove(localPath.LastIndexOf('/'));

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }

    protected virtual void FinishDownload()
    {
        CloseHttp();
        Destroy();
    }

    protected virtual void CloseHttp()
    {
        if (fileStream != null)
        {
            fileStream.Close();
            fileStream = null;
        }

        if (stream != null)
        {
            stream.Close();
            stream = null;
        }

        if (req != null)
        {
            req.Abort();
            req.KeepAlive = false;
            req = null;
        }
        if (rsp != null)
        {
            rsp.Close();
            rsp = null;
        }
        if (buffer != null)
        {
            buffer = null;
        }
    }

    protected virtual void Destroy()
    {
    }
}

public enum DownloadType
{
    PreDownload,
    BackDownload,
    GamingDownload,
    FailedDownload,
}

public enum ResourceListType
{
    None = -1,
    Normal,
    Limit,
}