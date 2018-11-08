using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class CSUIBaseEditor : Editor
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
    /// 存取UI模板路径
    /// </summary>
    public const string UIVIEW_PATH = "/Script/UI/view";

    /// <summary>
    /// __LK__：左大括号{
    /// __RK__：右大括号}
    /// {0}：UI名称
    /// {1}：成员变量代码
    /// {2}：变量赋值代码
    /// </summary>
    ///         "    {2}\r\n\r\n" +
    public const string BEHAVIOUR_FORMAT =
        "using UnityEngine;\r\n" +
        "using System;\r\n" +
        "using System.Collections;\r\n" +
        "using System.Collections.Generic;\r\n\r\n" +
        "public class {0} : UIBase\r\n" +
        "__LK__\r\n" +
        "    {1}\r\n\r\n" +
        "    public override void Init()\r\n" +
        "    __LK__\r\n" +
        "        base.Init();\r\n" +
        "    __RK__\r\n" +
        "    public override void Show()\r\n" +
        "    __LK__\r\n" +
        "        base.Show();\r\n" +
        "    __RK__\r\n" +
        "    public override void Destroy()\r\n" +
        "    __LK__\r\n" +
        "        base.Destroy();\r\n" +
        "        {2}\r\n\r\n" +
        "    __RK__\r\n" +
        "__RK__\r\n";
#endregion

    [MenuItem("Tools/生成UI代码", false, 1101)]
    public static void CreateUIBase()
    {
        GameObject mainObject = Selection.activeGameObject;

        if (mainObject == null)
        {
            Debug.LogError("未选择UI预制体");
            return;
        }

        mainObject = Instantiate(mainObject) as GameObject;

        mainObject.name = Selection.activeGameObject.name;

        Transform mainTrans = mainObject.transform;

        Transform[] array = GetSortArray(mainTrans);

        List<string> memberList = new List<string>();
        List<string> assignList = new List<string>();
        List<string> memberclearList = new List<string>();

        foreach (var item in array)
        {
            string name = "";
            string _strName = string.Format("{1}{0}", name, getName(false, item.gameObject.name, ref name));
            string strName = string.Format("{1}{0}", name, getName(true, item.gameObject.name, ref name));
            string strType = GetType(item);
            string strPath = getPath(mainObject, item);

            memberList.Add("");
            memberList.Add(string.Format(MEMBER_FORMAT, _strName, strType));
            memberclearList.Add(string.Format(MEMBER_NULL, _strName));
            memberList.Add(string.Format(MEMBER_ASSIGN, strName, strType, strPath, _strName));
        }
        string memberCode = string.Join("\r\n    ", memberList.ToArray());
        //string assignCode = string.Join("\r\n    ", assignList.ToArray());
        string memberClearCode = string.Join("\r\n        ", memberclearList.ToArray());

        string code = string.Format(BEHAVIOUR_FORMAT, mainTrans.name, memberCode, memberClearCode);
        code = code.Replace("__LK__", "{");
        code = code.Replace("__RK__", "}");

        string path = Application.dataPath + UIVIEW_PATH;

        string behaviourCodeFile = Path.Combine(path, mainTrans.name) + ".cs";

        using (FileStream fs = File.Create(behaviourCodeFile))
        {
            byte[] bytes = Encoding.Default.GetBytes(code);
            fs.Write(bytes, 0, bytes.Length);
        }

        Debug.Log(string.Format("代码生成成功!{0}", behaviourCodeFile));

        DestroyImmediate(mainObject);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static Transform[] GetSortArray(Transform mfather)
    {
        List<Transform> tatollist = new List<Transform>();
        Transform[] mtfs_active = mfather.GetComponentsInChildren<Transform>(true);
        Transform[] mtfs_Inactive = mfather.GetComponentsInChildren<Transform>(false);
        Transform[] mtfs = mfather.GetComponentsInChildren<Transform>(false);

        Debug.Log(mtfs_active.Length);
        Debug.Log(mtfs_Inactive.Length);
        Debug.Log(mtfs.Length);

        tatollist.AddRange(mtfs_active);
        //tatollist.AddRange(mtfs_Inactive);

        List<Transform> list = new List<Transform>();

        for (int i = 0; i < tatollist.Count; i++)
        {
            Transform tf = tatollist[i];
            if (IsInclude(tf.name)) list.Add(tf);
        }

        return list.ToArray();
    }

    private static string getPath(GameObject _target, Transform tf)
    {
        List<string> ListName = new List<string>();

        string strName = "";

        if (_target.name.Equals(tf.name)) return strName;

        if (tf.parent != _target.transform)
        {
            getPath(_target, tf.gameObject, ref ListName);
        }

        ListName.Reverse();

        foreach (string str in ListName)
        {
            strName += str + "/";
        }
        strName += tf.name;
        return "\"" + strName + "\"";
    }

    private static void getPath(GameObject _target, GameObject Mono, ref List<string> ListName)
    {
        if (_target.name == Mono.transform.parent.name) return;

        ListName.Add(Mono.transform.parent.name);

        getPath(_target, Mono.transform.parent.gameObject, ref ListName);
    }

    private static string getName(bool isPathName, string name, ref string sGoName)
    {
        string[] sName = name.Split('_');
        string str = "";
        if (sName.Length > 0)
        {
            str = sName[0];
            sGoName = sName[1];
        }   

        string s = str[0].ToString();
        string pathName = "m" + s.ToUpper() + str.Substring(1);
        return (isPathName ? pathName : str + sGoName);
    }

    private static string GetType(Transform Mono)
    {
        if (Mono.name.Contains("go_"))
        {
            return "GameObject";
        }
        else if (Mono.name.Contains("spr_"))
        {
            return "UISprite";
        }
        else if (Mono.name.Contains("lab_"))
        {
            return "UILabel";
        }
        else if (Mono.name.Contains("grid_"))
        {
            return "UIGrid";
        }
        else if (Mono.name.Contains("scroll_"))
        {
            return "UIScrollView";
        }
        else if (Mono.name.Contains("slid_"))
        {
            return "UISlider";
        }
        else if (Mono.name.Contains("progress_"))
        {
            return "UIProgressBar";
        }
        else if (Mono.name.Contains("tg_"))
        {
            return "UIToggle";
        }
        else if (Mono.name.Contains("input_"))
        {
            return "UIInput";
        }
        else if (Mono.name.Contains("tl_"))
        {
            return "UITextList";
        }
        else if (Mono.name.Contains("btn_"))
        {
            return "GameObject";
        }
        else if (Mono.name.Contains("tex_"))
        {
            return "UITexture";
        }
        else if (Mono.name.Contains("gridc_"))
        {
            return "UIGridContainer";
        }
        else
        {
            return "un";
        }
    }

    public static bool IsInclude(string _name)
    {
        if (_name.Contains("go_"))
        {
            return true;
        }
        else if (_name.Contains("spr_"))
        {
            return true;
        }
        else if (_name.Contains("lab_"))
        {
            return true;
        }
        else if (_name.Contains("grid_"))
        {
            return true;
        }
        else if (_name.Contains("scroll_"))
        {
            return true;
        }
        else if (_name.Contains("slid_"))
        {
            return true;
        }
        else if (_name.Contains("progress_"))
        {
            return true;
        }
        else if (_name.Contains("tg_"))
        {
            return true;
        }
        else if (_name.Contains("input_"))
        {
            return true;
        }
        else if (_name.Contains("tl_"))
        {
            return true;
        }
        else if (_name.Contains("btn_"))
        {
            return true;
        }
        else if (_name.Contains("tex_"))
        {
            return true;
        }
        else if (_name.Contains("gridc_"))
        {
            return true;
        }

        return false;
    }
}
