///网络层代码生成器
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Xml.Linq;
using System.Xml;

public class CSUINetEditor : Editor
{

    #region 格式
    /// <summary>
    /// {0}：变量名
    /// {1}：变量类型
    /// </summary>
    public const string MEMBER_FORMAT = "private {1} {0} = null;";

    /// <summary>
    /// {0}：变量名
    /// </summary>
    public const string MEMBER_NULL = "{0} = null;";

    /// <summary>
    /// {0}：变量名
    /// {1}：变量类型
    /// </summary>
    public const string MEMBER_ASSIGN =
        "private {1} {0} __LK__ get __LK__ return {3} ?? ({3} = Get<{1}>({2}));__RK____RK__";

    /// <summary>
    /// 存取网络层模版路径
    /// </summary>
    public const string UIVIEW_PATH = "/Script/Common/Panel";

    /// <summary>
    /// __LK__：左大括号{
    /// __RK__：右大括号}
    /// {0}：UI名称
    /// {1}：成员变量代码
    /// {2}：变量赋值代码
    /// </summary>
    public const string BEHAVIOUR_FORMAT =
        "using UnityEngine;\r\n" +
        "using System;\r\n" +
        "using System.Collections;\r\n" +
        "using System.Collections.Generic;\r\n\r\n" +
        "public class {0} : CSNetBase\r\n" +
        "__LK__\r\n" +
        "    public override void NetCallback(ECM _type, NetInfo obj)\r\n" +
        "    __LK__\r\n" +
        "        switch (_type)\r\n" +
        "        __LK__\r\n" +
        "       {1}\r\n" +
        "        __RK__\r\n" +
        "    __RK__\r\n" +
        "__RK__\r\n";
    #endregion

    private static string messagesid = "";
    private static string tablePath
    {
        get
        {
            string curPath = Application.dataPath;

            string str1 = "ZTClient/Assets";

            if (curPath.Contains(str1))
            {
                return curPath.Replace(str1, "xml");
            }

            string str0 = "Client/Branch/ClientAndroid2/Assets";

            if (curPath.Contains(str0))
            {
                Debug.Log("Data/Branch/CurrentUseData/table");
                return curPath.Replace(str0, "Data/Branch/CurrentUseData/Normal/table");
            }

            string str2 = "Client/Trunk/ClientAndroid/Assets";

            if (curPath.Contains(str2))
            {
                Debug.Log("Data/Trunk/CurrentUseData/table");
                return curPath.Replace(str2, "Data/Trunk/CurrentUseData/Normal/table");
            }

            string str3 = "Client/Branch/ClientAndroid_sldg/Assets";

            if (curPath.Contains(str3))
            {
                return curPath.Replace(str3, "Data/Branch/CurrentUseData/Normal-sldg/table");
            }


            Debug.Log("get proto  path  faile");
            return string.Empty;
        }
    } //= @"......

    [MenuItem("Tools/Selected Xml")]
    static void ReadSelectXml()
    {
        string excelPath = EditorUtility.OpenFilePanel("Select Table File", tablePath, "xml");

        if (string.IsNullOrEmpty(excelPath))
        {
            UnityEngine.Debug.LogWarning("Please select one xml file");
            return;
        }

        string FileName = excelPath.Substring((tablePath).Length + 1);
        FileName = FileName.Remove(FileName.Length - 4);

        //获取到XML下的所有子节点
        XmlNodeList result = LoadXML(excelPath);//XMl所在的文件地址  

        List<string> CaseList = new List<string>();
        UnityEngine.Debug.Log("生成之后请检查对应自己的协议是否生成");
        foreach (XmlElement ex in result)
        {
            UnityEngine.Debug.Log("协议号为："+messagesid + ex.Attributes["id"].Value);
            UnityEngine.Debug.Log("协议名为："+ex.Attributes["class"].Value);

            //根据发送或者接收类型区分协议内容
            if (ex.Attributes["type"].Value == "toClient")
            {
                CaseList.Add("      case ECM." + ex.Attributes["class"].Value + ":");
                CaseList.Add("          __LK__");
                if (ex.FirstChild != null && ex.FirstChild.Attributes["class"] != null)
                {
                    string[] ClassList = ex.FirstChild.Attributes["class"].Value.Split('.');
                    string ClassName = ClassList[ClassList.Length - 1];
                    //  UnityEngine.Debug.Log(ClassName);
                    CaseList.Add("             " + FileName + "." + ClassName + " reqdata = Network.Deserialize<" + FileName + "." + ClassName + ">(obj);");
                }
                CaseList.Add("          __RK__");
                CaseList.Add("          break;");
            }
        }

        string CaseCode = string.Join("\r\n    ", CaseList.ToArray());

        string code = string.Format(BEHAVIOUR_FORMAT, FileName + "Net", CaseCode);
        code = code.Replace("__LK__", "{");
        code = code.Replace("__RK__", "}");

        string path = Application.dataPath + UIVIEW_PATH;

        string behaviourCodeFile = Path.Combine(path, FileName + "Net") + ".txt";

        using (FileStream fs = File.Create(behaviourCodeFile))
        {
            byte[] bytes = Encoding.Default.GetBytes(code);
            fs.Write(bytes, 0, bytes.Length);
        }

        Debug.Log(string.Format("代码生成成功!{0}", behaviourCodeFile));

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static XmlNodeList LoadXML(string excelPath)
    {
        if (File.Exists(excelPath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            WWW www = new WWW("file:// " + excelPath);
            while (true)
            {
                if (www.isDone)
                {
                    System.IO.StringReader stringReader = new System.IO.StringReader(www.text);
                    xmlDoc.LoadXml(www.text);
                    break;
                }
            }
            messagesid = xmlDoc.SelectSingleNode("messages").Attributes["id"].Value;
            return xmlDoc.SelectSingleNode("messages").ChildNodes;
        }
        return null;
    }
}
