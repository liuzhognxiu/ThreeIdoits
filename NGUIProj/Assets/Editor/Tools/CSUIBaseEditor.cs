using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

/// <summary>
/// UI Lua代码生成器，对应生成UI基础部分代码，为了保证项目开发统一规范
/// </summary>
public class CSUIBaseEditor : Editor
{
    #region 格式

    /// <summary>
    /// {0}：变量名
    /// {1}：物体所在节点
    /// {2}：脚本的类型
    /// </summary>
    public const string MEMBER_FORMAT = "this.{0} = findChildRecursively(this.transform,'{1}').gameObject:GetComponent('{2}')";


    /// <summary>
    /// 声明UI的销毁 
    /// </summary>
    public const string MEMBER_DESTORY = "function {0}.OnDestroy()";

    /// <summary>
    /// 存取UI模板路径
    /// </summary>
    public const string UIVIEW_PATH = "/LuaFramework/lua/MVC/UIView";

    /// <summary>
    /// 存放UILogic的路径
    /// </summary>
    public const string UILOGIC_PATH = "/LuaFramework/lua/MVC/UILogic";

    /// <summary>
    /// 存放UIModel的路径
    /// </summary>
    public const string UIMODEL_PATH = "/LuaFramework/lua/MVC/UIModel";

    /// <summary>
    /// {0}：UI名称
    /// {1}：成员变量代码
    /// {2}：{}
    /// </summary>
    ///         "    {2}\r\n\r\n" +
    public const string BEHAVIOUR_FORMAT =
        "require(\"MVC/UILogic/{0}Logic\")" +
        "{0} = class(ViewBase)\r\n \r\n" +
        "local this = {2} \r\n" +
        "function {0}:ctor() \r\n" +
        "end \r\n" +
        "function UINpcEvent:Reset()\r\n" +
        "    if this.gameObject ~= nil then\r\n" +
        "        GameObject.Destory(this.gameObject)\r\n" +
        "        this.gameObject = nil\r\n" +
        "    end\r\n" +
        "end\r\n\r\n" +
        "function {0}:init()\r\n" +
        "    this.btnBehaviour = nil\r\n" +
        "    this.root = GameObject.Find('UI Root')\r\n" +
        "    this.gameObject = resMgr:LoadPrefab(\"Prefabs/{0}\",this.root)\r\n" +
        "    this.luaBehavior = this.gameObject:GetComponent('LuaBehaviour')\r\n" +
        "    this.transform = this.gameObject.transform\r\n" +
        "    {1}\r\n" +
        "    layerManager:SetLayer(this.gameObject,UILayerType.Window)\r\n" + //目前UI默认在window层，之后根据对应的层级分文件夹，其中Window为对应的文件夹的名字
        "end\r\n\r\n" +
        "function {0}.OnDestroy()\r\n" +
        "    this.{0}logic:Release()\r\n" +
        "end\r\n";
    #endregion

    /// <summary>
    /// 生成luamModel代码
    /// {0}UI名称
    /// {1} {}
    /// </summary>
    public const string LOGICBEHAVIOUR_FORMAT =
    "require \"MVC/UIModel/{0}Model\"\r\n" +
    "require \"Framework/LuaLogic\"\r\n\r\n" +
    "{0}Logic = class(LuaLogic)\r\n\r\n" +
    "local this = {1}\r\n\r\n" +
    "function {0}Logic:ctor()\r\n" +
    "end\r\n\r\n" +
    "function {0}Logic:Initialize(view)\r\n" +
    "   this.view  = view\r\n" +
    "   self:ItemSource(GameData.{0}Model())\r\n" +
    "end\r\n";

    public const string MODELBEHAVIOUR_FORMAT =
    "{0}Model = class(NotifyPropChanged)\r\n\r\n" +
    "local  this = {1}\r\n" +
    "function {0}Model:ctor()\r\n\r\n" +
    "end\r\n";

    [MenuItem("Assets/Tools/生成UI代码")]
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
            memberList.Add(string.Format(MEMBER_FORMAT, _strName, strPath, strType));
            //memberclearList.Add(string.Format(MEMBER_NULL, _strName));
            //memberList.Add(string.Format(MEMBER_ASSIGN, strName, strType, strPath, _strName));
        }
        string memberCode = string.Join("\r\n    ", memberList.ToArray());
        //string assignCode = string.Join("\r\n    ", assignList.ToArray());
        //string memberClearCode = string.Join("\r\n        ", memberclearList.ToArray());


        string code = string.Format(BEHAVIOUR_FORMAT, mainTrans.name, memberCode, "{}");

        string modelcode = string.Format(MODELBEHAVIOUR_FORMAT, mainTrans.name.Substring(2), "{}");

        string LogicCode = string.Format(LOGICBEHAVIOUR_FORMAT, mainTrans.name.Substring(2), "{}");
        //code = code.Replace("__LK__", "{");
        //code = code.Replace("__RK__", "}");

        string path = Application.dataPath + UIVIEW_PATH;

        string modelPath = Application.dataPath + UIMODEL_PATH;

        string logicPath = Application.dataPath + UILOGIC_PATH;

        string behaviourCodeFile = Path.Combine(path, mainTrans.name) + ".lua";

        string modelbehaviourCodeFile = Path.Combine(modelPath, mainTrans.name + "Model") + ".lua";

        string logicbehaviourCodeFile = Path.Combine(logicPath, mainTrans.name + "Logic") + ".lua";


        using (FileStream fs = File.Create(behaviourCodeFile))
        {
            byte[] bytes = Encoding.Default.GetBytes(code);
            fs.Write(bytes, 0, bytes.Length);
        }

        using (FileStream fs = File.Create(modelbehaviourCodeFile))
        {
            byte[] bytes = Encoding.Default.GetBytes(modelcode);
            fs.Write(bytes, 0, bytes.Length);
        }

        using (FileStream fs = File.Create(logicbehaviourCodeFile))
        {
            byte[] bytes = Encoding.Default.GetBytes(LogicCode);
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
        return strName;
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

    /// <summary>
    /// 设置需要获取控件的名字
    /// </summary>
    /// <param name="Mono"></param>
    /// <returns></returns>
    private static string GetType(Transform Mono)
    {
        if (Mono.name.Contains("go_"))
        {
            return "GameObject";
        }
        else if (Mono.name.Contains("sp_"))
        {
            return "UISprite";
        }
        else if (Mono.name.Contains("lb_"))
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
        else if (_name.Contains("sp_"))
        {
            return true;
        }
        else if (_name.Contains("lb_"))
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
