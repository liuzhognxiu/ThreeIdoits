
//-----------------------------------------
//Resource
//author LiZongFu
//time 2016.3.28
//-----------------------------------------

using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

public class CSResourceWWW : CSResource
{
    public AssetBundle assetBundle;
    public delegate void OnLoadedTable(CSResourceWWW res);
    public event OnLoadedTable onLoadedTable;
    bool isReloading = false;
    byte reloadTime = 0;
    float mapBeginGetDataEndTime = 0;
    public CSResourceWWW(string name, string path, ResourceType type) : base(name, path, type)
    {
        Path = path;
    }

    public override void Load()
    {
        if (IsDone)
        {
            base.onLoaded.CallBack(this);
            base.onLoaded.Clear();
            if (onLoadedTable != null)
            {
                onLoadedTable(this);
            }
            return;
        }

        LoadProc(Path);
    }

    private string relatePath;

    void LoadProc(string path)
    {
        relatePath = path.Replace(SFOut.URL_mClientResUrl, "");

        if (!SFOut.IGame.IsLoadLocalRes)
        {
            if (SFOut.IResUpdateMgr.CheckIsNeedDownload(relatePath))
            {
                DealNeedWaitHotUpdate();
                return;
            }
        }

        IsDone = false;
        SFOut.Game.StartCoroutine(GetData(path));
    }

    IEnumerator GetData(string path)
    {
        if (LocalType == ResourceType.Map || SFOut.IResourceManager.IsUIRes(this))
        {
            mapBeginGetDataEndTime = UnityEngine.Time.time + 2;
        }
        if (LocalType == ResourceType.MapBytes
|| LocalType == ResourceType.TableBytes)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(path))
            {
                yield return www.Send();
                if (www.isError)
                {
                    if (Debug.developerConsoleVisible) Debug.Log(www.error + " " + path);
                    if (SFOut.IGame != null && SFOut.IGame.IsLoadLocalRes)
                    {
                        OnLoadedErrorProc();
                    }
                    else
                    {
                        DealNeedWaitHotUpdate();
                    }
                }
                else
                {
                    MirroyBytes = www.downloadHandler.data;
                    LoadFinish();
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.GetAssetBundle(path))
            {
                yield return www.Send();

                if (www.isError)
                {
                    if (Debug.developerConsoleVisible) Debug.Log(www.error + " " + path);
                    if (SFOut.IGame != null && SFOut.IGame.IsLoadLocalRes)
                    {
                        OnLoadedErrorProc();
                    }
                    else
                    {
                        DealNeedWaitHotUpdate();
                        if (LocalType == ResourceType.Map || SFOut.IResourceManager.IsUIRes(this))
                        {
                            mapBeginGetDataEndTime = 0;
                            yield break;
                        }
                    }
                }
                else
                {
                    assetBundle = DownloadHandlerAssetBundle.GetContent(www);
                    if (LocalType == ResourceType.SceneRes)
                    {
                        OnFinishLoadedScene();
                        yield break;
                    }
                    yield return SyncLoadMainAsset(true);
                }
            }
        }
        if (LocalType == ResourceType.Map || SFOut.IResourceManager.IsUIRes(this))
        {
            if (MirrorObj == null)
            {
                yield break;
            }
        }
        mapBeginGetDataEndTime = 0;
    }

    void OnFinishLoadedScene()
    {
        loadedTime = UnityEngine.Time.time;
        IsDone = true;
        base.onLoaded.CallBack(this);
        base.onLoaded.Clear();
        if (onLoadedTable != null)
            onLoadedTable(this);
        SFOut.IResourceManager.wwwLoaded(this);
        mapBeginGetDataEndTime = 0;
    }

    public override void UpdateLoading()
    {
        if (mapBeginGetDataEndTime != 0 && UnityEngine.Time.time > mapBeginGetDataEndTime)
        {
            if (reloadTime >= 2)//暫時屏蔽，等確認重下可行的情況下，再把這個 int.MaxValue改成1或者其他
            {
                mapBeginGetDataEndTime = 0;//客戶端下載失敗，重下后還是下載失敗，跳過下載
                OnLoadedErrorProc();
            }
            else
            {
                isReloading = true;
                reloadTime++;
            }
        }
        if (isReloading)
        {
            isReloading = false;
            SFOut.Game.StartCoroutine(GetData(Path));
        }
    }

    public void DealNeedWaitHotUpdate()
    {
        loadedTime = UnityEngine.Time.time;
        IsDone = false;
        SFOut.IResourceManager.wwwLoaded(this);
        DownloadFromServer();
    }

    public override void ResHotUpdateCallBack_HttpLoad()
    {
        isHotLoading = false;
        IsDone = false;
        SFOut.Game.StartCoroutine(GetData(Path));
    }

    void LoadFinish(bool isFromLocalLoad = false)
    {
        if (assetBundle != null)
            assetBundle.Unload(false);
        if (isFromLocalLoad && LocalType == ResourceType.Map)
        {
            if (MirrorObj == null)
            {
                return;
            }
        }
        loadedTime = UnityEngine.Time.time;
        IsDone = true;
        base.onLoaded.CallBack(this);
        base.onLoaded.Clear();
        if (onLoadedTable != null)
            onLoadedTable(this);
        SFOut.IResourceManager.wwwLoaded(this);
    }

#if (!UNITY_4_7&&!UNITY_4_6)
    UnityEngine.Object GetMainAsset()
    {
        if (assetBundle == null) return null;
        string[] strs = assetBundle.GetAllAssetNames();
        if (strs.Length > 0)
        {
            return assetBundle.LoadAsset(strs[0]);
        }
        return null;
    }
#endif

    IEnumerator SyncLoadMainAsset(bool isFromLocalLoad = false)
    {
        if (SFOut.Game == null || assetBundle == null) yield break;
        string[] strs = assetBundle.GetAllAssetNames();
        if (strs.Length > 0)
        {
            if (LocalType == ResourceType.ScaleMap ||
                LocalType == ResourceType.MapBytes ||
                mResourceAssistType == ResourceAssistType.ForceLoad)
            {
                MirrorObj = assetBundle.LoadAsset(strs[0]);
                LoadFinish();
            }
            else
            {
                string arName = strs[0];
                AssetBundleRequest ar = assetBundle.LoadAssetAsync(arName);
                yield return ar;
                MirrorObj = ar.asset;
                ar = null;
                LoadFinish(isFromLocalLoad);
            }
        }
    }

    void OnLoadedErrorProc()
    {
        loadedTime = UnityEngine.Time.time;
        IsDone = true;
        base.onLoaded.CallBack(this);
        base.onLoaded.Clear();
        if (onLoadedTable != null)
            onLoadedTable(this);
    }

    public void ClearTablCallBack()
    {
        onLoadedTable = null;
    }

    private void DownloadFromServer()
    {
        isHotLoading = true;

        Resource res;

        if (SFOut.IResUpdateMgr.CheckIsNeedDownload(relatePath))
        {
            int resType = SFOut.IResUpdateMgr.GetResourceType(relatePath);
            res = new Resource(relatePath, 0, 0, resType, DownloadType.GamingDownload);
        }
        else
        {
            res = new Resource(relatePath, 0, 0, DownloadType.GamingDownload);
        }

        SFOut.IResUpdateMgr.AddToDownloadQueue(res);
    }
}
