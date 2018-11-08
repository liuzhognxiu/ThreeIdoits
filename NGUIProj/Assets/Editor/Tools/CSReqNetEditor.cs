///网络层代码生成器
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Xml.Linq;
using System.Xml;

public class CSNetReqEditor : Editor
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
    public const string UIVIEW_PATH = "/Script/Network/Common";

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
        "public partial class Net\r\n" +
        "__LK__\r\n" +
        "     {0}\r\n" +
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

    [MenuItem("Tools/Selected Xml to req")]
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
        List<string> MessageList = new List<string>();
        foreach (XmlElement ex in result)
        {
            UnityEngine.Debug.Log("协议号为：" + messagesid + ex.Attributes["id"].Value);
            UnityEngine.Debug.Log("协议名为：" + ex.Attributes["class"].Value);
            if (ex.Attributes["type"].Value == "toServer")
            {
                CaseList.Add("/// <summary>");
                CaseList.Add("///" + ex.Attributes["desc"].Value);
                CaseList.Add("/// <summary>");
                CaseList.Add("public static void " + ex.Attributes["class"].Value + "()");
                CaseList.Add("__LK__");
                if (ex.FirstChild != null && ex.FirstChild.Attributes["class"] != null)
                {
                    string[] ClassList = ex.FirstChild.Attributes["class"].Value.Split('.');
                    string ClassName = ClassList[ClassList.Length - 1];
                    //  UnityEngine.Debug.Log(ClassName);
                    CaseList.Add("    " + FileName + "." + ClassName + " req = new " + FileName + "." + ClassName + "();");
                }
                CaseList.Add("    CSNetwork.Instance.SendMsg((int)ECM." + ex.Attributes["class"].Value + ", req);");
                CaseList.Add("__RK__");
            }
        }

        MessageList.Add("\r\n //这里生成的代码可以直接放入msgEnum中");
        MessageList.Add("/*");
        foreach (XmlElement ex in result)
        {
            MessageList.Add("/// <summary>" + ex.Attributes["desc"].Value + "  </summary>");
            MessageList.Add(ex.Attributes["class"].Value + " = " + messagesid + ex.Attributes["id"].Value + ",");
        }
        MessageList.Add("*/");
        string CaseCode = string.Join("\r\n    ", CaseList.ToArray());
        string MessageCode = string.Join("\r\n    ", MessageList.ToArray());
        string code = string.Format(BEHAVIOUR_FORMAT, CaseCode + MessageCode);
        code = code.Replace("__LK__", "{");
        code = code.Replace("__RK__", "}");

        string path = Application.dataPath + UIVIEW_PATH;

        string behaviourCodeFile = Path.Combine(path, "Req_" + FileName) + ".txt";

        using (FileStream fs = File.Create(behaviourCodeFile))
        {
            byte[] bytes = Encoding.Default.GetBytes(code);
            fs.Write(bytes, 0, bytes.Length);
        }

        Debug.Log(string.Format("代码生成成功!{0}", behaviourCodeFile));
        UnityEngine.Debug.Log("此方法并未给出对应结构以及对应方法的参数，请手动添加对应的结构体与参数");
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
