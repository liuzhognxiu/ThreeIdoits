using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;

#if UNITY_EDITOR
using UnityEditor;
#endif

//常用工具类
public class Utility
{
    public static bool IsFileExist(string filePath, bool isIgnoreExtension = false)
    {
        if (!isIgnoreExtension)
        {
            return File.Exists(filePath);
        }
        string dirPath = filePath + "/../";
        int starIndex = filePath.LastIndexOf("/") + 1;
        int length = filePath.LastIndexOf(".") - starIndex;
        string fileName = filePath.Substring(starIndex, length);
        if (Directory.Exists(dirPath))
        {
            DirectoryInfo info = new DirectoryInfo(dirPath);
            FileInfo[] files = info.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = files[i];
                if (fileInfo.Extension.Contains("meta")) continue;
                string fName = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."));
                if (fName == fileName)
                {
                    //Debug.Log("fileInfo = "+fileInfo.FullName);
                    return true;
                }
                //Debug.Log(fileInfo.Name);
                //if(fileInfo.Name)
            }
        }
        return false;
    }

    private static bool IsInt(string text)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= 0) return false;


        for (int i = 0; i < text.Length; i++)
        {
            char ch = text[i];
            // Integer number validation
            if (ch < '0' && ch > '9') return false;
        }

        return true;
    }

    public static Transform FindChildren(Transform parent, string name)
    {
        if (parent == null) return null;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name == name)
            {
                return parent.GetChild(i);
            }
            else
            {
                Transform trs = FindChildren(parent.GetChild(i), name);
                if (trs != null)
                {
                    return trs;
                }
            }
        }
        return null;
    }

    public static string GetTimeStr(int time)
    {
        if (time <= 0) return "";
        string str = "";
        int h = time / 3600;
        string s_h = h < 10 ? "0" + h.ToString() : h.ToString();
        int m = (time % 3600) / 60;
        string s_m = m < 10 ? "0" + m.ToString() : m.ToString();
        int s = time % 3600 % 60;
        string s_s = s < 10 ? "0" + s.ToString() : s.ToString();
        str = s_h + ":" + s_m + ":" + s_s;
        return str;
    }

    public static string GetTimeStrMS(int time)
    {
        if (time <= 0) return "";
        string str = "";
        int m = time / 60;
        string min = (m < 10) ? "0" + m.ToString() : m.ToString();
        int s = time % 60;
        string sec = (s < 10) ? "0" + s.ToString() : s.ToString();
        str = string.Format("{0}:{1}", min, sec);
        return str;
    }


    //毫秒转天数
    public static int GetMsecTimeToDay(long time)
    {
        if (time <= 0) return 0;
        int day = (int)(time / (1000 * 3600 * 24));
        return day;
    }


    public static List<UITipsView> TipsList = new List<UITipsView>();
    public static List<UITipsView> GameTipsList = new List<UITipsView>();
    public static UITipsView CenterTips;

    /// <summary>
    /// 显示底端信息提示
    /// </summary>
    /// <param name="content"></param>
    /// <param name="color"></param>
    /// <param name="timer"></param>
    public static void ShowTips(string content, Color color, float timer = 1.5f)
    {
        if (TipsList.Count >= 5) return;
        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            tips.gameObject.SetActive(true);
            tips.ShowTips(content, timer, color,false);
            tips.SizeTipSize(16);
        }
    }

    public static void ShowCenterTipsBig(string content, Color color, float timer = 1.5f)
    {
        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            CenterTips = tips;
            tips.CacheTrans.localPosition = new Vector3(0, 200, 0);
            tips.gameObject.SetActive(true);
            tips.ShowCenterTips(content, timer, color);
            tips.SizeTipSize(18, 32);
        }
    }

    public static void SetCenterTipsFalse()
    {
        if (CenterTips != null && CenterTips.gameObject.activeSelf)
        {
            CenterTips.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 显示底端信息
    /// </summary>
    /// <param name="content"></param>
    public static void ShowRedTips(string content)
    {
        ShowTips(content, Color.red, 1.5f);
    }

    /// <summary>
    /// 显示左下角信息提示
    /// </summary>
    /// <param name="content"></param>
    /// <param name="timer"></param>
    public static void ShowGameInfo(string content, float timer = 3f)
    {
        if (GameTipsList.Count >= 5)
        {
            foreach (var i in GameTipsList)
            {
                if (i.TipPos == UITipsView.TipPosType.LeftDown)
                {
                    i.ImmediatelyBeginAlpha(true);
                }
                break;
            }
        }
        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            tips.transform.localPosition = new Vector3(-560, -85, 0);
            tips.gameObject.SetActive(true);
            tips.Spirte.gameObject.SetActive(false);
            tips.ShowTips(content, timer, Color.white, true);
            tips.SizeTipSize(16);
        }
    }
    /// <summary>
    ///显示右下角的提示
    /// </summary>
    /// <param name="content"></param>
    /// <param name="timer"></param>
    public static void ShowRightInfo(string content, float timer = 3f)
    {
        if (GameTipsList.Count >= 5)
        {
            foreach (var i in GameTipsList)
            {
                if (i.TipPos == UITipsView.TipPosType.Right)
                {
                    i.ImmediatelyBeginAlpha(true);
                }
                break;
            }
        }
        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            tips.transform.localPosition = new Vector3(400, -85, 0);
            tips.gameObject.SetActive(true);
            tips.ShowTips(content, timer, Color.white, true,UITipsView.TipPosType.Right);
            tips.SizeTipSize(24);
        }
    }


    /// <summary> 显示中间提示信息</summary>
    public static void ShowCenterInfo(string content, Color color, float timer = 1.5f)
    {
        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            tips.gameObject.SetActive(true);
            tips.ShowCenterTips(content, timer, color);
            tips.SizeTipSize(16);
        }
    }

    public static void ShowHonorInfo(int _honor)
    {
        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            tips.gameObject.SetActive(true);
            tips.ShowHonorTip(_honor);
        }
    }

    /// <summary> 显示中间向上飘提示信息</summary>
    public static void ShowCenterMoveUpInfo(string content, Color color, float timer = 1.5f, int fontsize = 16)
    {
        if (TipsList.Count >= 5) return;

        for (int i = 0; i < TipsList.Count; i++)
        {
            UITipsView tip = TipsList[i];
            if (tip != null && tip.TipPos == UITipsView.TipPosType.Center)
            {
                Vector3 localPos = tip.transform.localPosition;
                tip.transform.localPosition = localPos + new Vector3(0, 25, 0);
            }
        }

        UITipsView tips = UIPool.Instance.PopUITipPanel();
        if (tips != null)
        {
            tips.gameObject.SetActive(true);
            tips.ShowCenterMoveUpTips(content, timer, color);
            tips.SizeTipSize(fontsize);
        }
    }

    public static void ShowCenterMoveUpInfo2(string content, List<UITipsView> list, float delay, Color color, float duration = 1.5f)
    {
        UITipsView tips = list.Count >= 3 ? list[0] : UIPool.Instance.PopUITipPanel();
        if (list.Count >= 3)
            list.RemoveAt(0);
        if (tips != null)
        {
            tips.gameObject.SetActive(true);
            tips.ShowCenterMoveUpTips(content, list, delay, duration, color);
            tips.SizeTipSize(16);
        }
    }

    public static Transform FindDeepChild(GameObject _target, string _childName)
    {
        Transform tr = null;
        tr = _target.transform.Find(_childName);

        if (tr == null)
        {
            foreach (Transform t in _target.transform)
            {
                tr = FindDeepChild(t.gameObject, _childName);
                if (tr != null) return tr;
            }
        }
        return tr;
    }

    public static void Removedistance(Transform mtr, bool isRemove)
    {
        if (isRemove) mtr.localPosition = mRemovedistance;
        else mtr.localPosition = Vector3.zero;
    }

    public static Vector3 mRemovedistance = new Vector3(-10000f, 0f, 0f);

    public static bool IsBetween(float value, float min, float max)
    {
        return value >= min && value < max;
    }

    public static int GetMaxDepth(GameObject panelGroup)
    {
        UIPanel[] panels = panelGroup.GetComponentsInChildren<UIPanel>(true);
        int maxDepth = -100;
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == 0) maxDepth = panels[i].depth;
            else
            {
                maxDepth = Mathf.Max(panels[i].depth, maxDepth);
            }
        }
        return maxDepth;
    }

    public static bool GetMaxDepth(GameObject panelGroup, out int maxDepth)
    {
        UIPanel[] panels = panelGroup.GetComponentsInChildren<UIPanel>(true);
        maxDepth = 0;
        if (panels == null || panels.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            if (i == 0) maxDepth = panels[i].depth;
            else
            {
                maxDepth = Mathf.Max(panels[i].depth, maxDepth);
            }
        }
        return true;
    }

    public static int GetMinDepth(GameObject panelGroup)
    {
        UIPanel[] panels = panelGroup.GetComponentsInChildren<UIPanel>(true);
        int minDepth = -100;
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == 0) minDepth = panels[i].depth;
            else
            {
                minDepth = Mathf.Min(panels[i].depth, minDepth);
            }
        }
        return minDepth;
    }

    public static void SetSubPanelDepth(GameObject go, int basePanelDepth)
    {
        if (go == null) return;
        UIPanel[] panels = go.GetComponentsInChildren<UIPanel>(true);
        if (panels != null)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].depth = basePanelDepth + i + 1;
            }
        }
    }

    public static Color ConvertCodeToColor(int code16)
    {
        return new Color((code16 & 0xff0000) >> 16, (code16 & 0xff00) >> 8, code16 & 0xff, 255f) / 255f;
    }

    public static Color ConvertCodeToColor(string code16)
    {
        int value;
        if (int.TryParse(code16, System.Globalization.NumberStyles.AllowHexSpecifier, System.Globalization.CultureInfo.InvariantCulture, out value))
            return ConvertCodeToColor(value);
        else return Color.white;
    }

    public static string StringConvertCurrency(string _num)
    {
        for (int i = _num.Length - 3; i > 0; i -= 3)
        {
            _num = _num.Insert(i, ",");
        }
        return _num;
    }

    public static bool Contains(string arrayStr, string value)
    {
        string[] splitArray = arrayStr.Split('#');
        for (int i = 0; i < splitArray.Length; i++)
        {
            if (value.ToString() == splitArray[i])
                return true;
        }
        return false;
    }


    public static string GetCountDown(TimeSpan countDown)
    {
        return countDown.TotalSeconds > 0 ? string.Format("{0}:{1}", countDown.Minutes.ToString().PadLeft(2, '0'), countDown.Seconds.ToString().PadLeft(2, '0')) : "00:00";
    }

    public static Vector3 CalculateUIWorldPos(Transform go)
    {
        Vector3 pos = Vector3.zero;
        while (go != null)
        {
            pos += go.localPosition;
            go = go.parent;
        }
        return pos;
    }

    public static Vector3 CalculateOffsetParent(Transform child, Transform parent)
    {
        if (child == null || parent == null || child == parent) return Vector3.zero;
        Vector3 offset = child.localPosition;
        Transform p = child.parent;
        while (p != null && p != parent)
        {
            offset += p.localPosition;
            p = p.parent;
        }
        return offset;
    }

    public static string RemoveColorCode(string content)
    {
        return new System.Text.RegularExpressions.Regex("\\[[0-9A-Fa-f]{6}\\]|\\[-\\]", RegexOptions.IgnoreCase).Replace(content, "");
    }

    public static void Log(object message, UnityEngine.Object context)
    {
        if (Debug.developerConsoleVisible) Debug.Log(string.Format("{0},{1}", Time.frameCount, message), context);
    }

    public static void Log(params object[] messages)
    {
        System.Text.StringBuilder sb = new StringBuilder(Time.frameCount + ",");
        for (int i = 0; i < messages.Length; i++)
        {
            sb.Append(messages[i] + ",");
        }
        if (Debug.developerConsoleVisible) Debug.Log(sb.ToString());
    }

    public static void LogError(params object[] messages)
    {
        System.Text.StringBuilder sb = new StringBuilder(Time.frameCount + ",");
        for (int i = 0; i < messages.Length; i++)
        {
            sb.Append(messages[i] + ",");
        }
        if (Debug.developerConsoleVisible) Debug.LogError(sb.ToString());
    }

    public static void LogWarning(params object[] messages)
    {
        System.Text.StringBuilder sb = new StringBuilder(Time.frameCount + ",");
        for (int i = 0; i < messages.Length; i++)
        {
            sb.Append(messages[i] + ",");
        }
        if (Debug.developerConsoleVisible) Debug.LogWarning(sb.ToString());
    }

    public static string GetPlatformName()
    {
#if UNITY_EDITOR
        return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
            return GetPlatformForAssetBundles(Application.platform);
#endif
    }
#if UNITY_EDITOR
    private static string GetPlatformForAssetBundles(BuildTarget target)
    {

        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "iOS";
            case BuildTarget.WebGL:
                return "WebGL";
            case BuildTarget.WebPlayer:
                return "WebPlayer";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "OSX";
            // Add more build targets for your own.
            // If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
            default:
                return null;
        }
    }
#endif


    private static string GetPlatformForAssetBundles(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.WebGLPlayer:
                return "WebGL";
            case RuntimePlatform.OSXWebPlayer:
            case RuntimePlatform.WindowsWebPlayer:
                return "WebPlayer";
            case RuntimePlatform.WindowsPlayer:
                return "Windows";
            case RuntimePlatform.OSXPlayer:
                return "OSX";
            default:
                return null;
        }
    }


    /// <summary>
    /// 呼叫流程
    /// </summary>
    /// <param name="processName"></param>
    /// <param name="param"></param>
    /// <returns>成功true</returns>
    public static bool CallProcess(string processName, string param)
    {
        System.Diagnostics.ProcessStartInfo process = new System.Diagnostics.ProcessStartInfo
        {
            CreateNoWindow = false,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            FileName = processName,
            Arguments = param,
        };

        UnityEngine.Debug.Log(processName + " " + param);

        System.Diagnostics.Process p = System.Diagnostics.Process.Start(process);
        p.WaitForExit();

        string error = p.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.LogError(processName + " " + param + "  ERROR! " + "\n" + error);

            string output = p.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(output))
            {
                UnityEngine.Debug.Log(output);
            }

            return false;
        }
        return true;
    }


    public struct StringData
    {
        public uint id;
        public uint count;
        public uint strength;
        public StringData(uint _id, uint _count, uint _strength = 0)
        {
            this.id = _id;
            this.count = _count;
            this.strength = _strength;
        }
    }
    #region 动画部分
    public static void PlayTweenPosition(TweenPosition tween, Vector3 from, Vector3 to, float duration = 1f, float delay = 0f)
    {
        if (tween == null) return;
        tween.from = from;
        tween.to = to;
        tween.duration = duration;
        tween.delay = delay;
        tween.PlayTween();
    }

    public static void PlayTweenScale(TweenScale tween, Vector3 from, Vector3 to, float duration = 1f, float delay = 0f)
    {
        if (tween == null) return;
        tween.from = from;
        tween.to = to;
        tween.duration = duration;
        tween.delay = delay;
        tween.PlayTween();
    }

    public static void PlayTweenAlpha(TweenAlpha tween, float from, float to, float duration = 1f, float delay = 0f)
    {
        if (tween == null) return;
        tween.from = from;
        tween.to = to;
        tween.duration = duration;
        tween.delay = delay;
        tween.PlayTween();
    }
    #endregion
}

#region 颜色
public struct BBCode
{
    public static string Red { get { return "[eb0d0d]"; } }
    public static string Green { get { return "[40d440]"; } }
    public static string Blue { get { return "[2bcfed]"; } }
    public static string White { get { return "[ffffff]"; } }
    public static string DarkGray { get { return "[5a5b5c]"; } }
    public static string LightGray { get { return "[8c8c8c]"; } }
    public static string Yellow { get { return "[ecbb46]"; } }
    public static string Purple { get { return "[e45bf0]"; } }
    public static string YellowB { get { return "[fbd671]"; } }
    public static string PaleYellow { get { return "[ffeec1]"; } }
    public static string GrayB { get { return "[b2b2b2]"; } }
    public static string Gold { get { return "[e8a657]"; } }
    public static string Claybank { get { return "[c9b39c]"; } }

    public static string GetCode(bool value)
    {
        return value ? Green : Red;
    }
}

public struct CSColor
{
    public static Color orange { get { return new Color(0.996f, 0.4f, 0f); } }
    public static Color magenta { get { return new Color(0.961f, 0, 0.996f); } }
}
#endregion

#region 字符串验证
public static class BanedName
{
    public static string filterText(string str)
    {
        return str;
        //for (int i = 0; i < BanedNameTableManager.Instance.array.rows.Count; i++)
        //{
        //    if (str.Contains(BanedNameTableManager.Instance.array.rows[i].name))
        //    {
        //        str = str.Replace(str, "*");  //如果含有txt文档中的关键字,则替换为"*"

        //        return str;
        //    }
        //}
        //return str;
    }

    public static bool IsContianfilter(string str)
    {
        return false;
        //for (int i = 0; i < BanedNameTableManager.Instance.array.rows.Count; i++)
        //{
        //    if (str.Contains(BanedNameTableManager.Instance.array.rows[i].name))
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }
}
#endregion

#region 时间类
public class Schedule
{
    float mTime = 0.0001f;
    System.Action<Schedule> mDelegate;
    bool mCanceled = false;
    bool mUseWorldTimeScale = false;

    public bool Canceled
    {
        get
        {
            return mCanceled;
        }
    }

    public float Time
    {
        get { return mTime; }
    }

    public bool UsingWorldTimeScale
    {
        get { return mUseWorldTimeScale; }
    }

    public System.Action<Schedule> Callback
    {
        get { return mDelegate; }
    }

    public Schedule()
    {
    }

    public Schedule(float t, System.Action<Schedule> callback)
    {
        mTime = t;
        mDelegate = callback;
    }

    public Schedule(float t, bool worldTimeScale, System.Action<Schedule> callback)
    {
        mTime = t;
        mUseWorldTimeScale = worldTimeScale;
        mDelegate = callback;
    }

    public void Cancel()
    {
        mCanceled = true;
    }

    public void ResetTime(float t)
    {
        mTime = t;
    }
}
public class Timer
{
    public static Timer Instance
    {
        get { return msSingleton; }
    }

    static readonly Timer msSingleton = new Timer();

    public Schedule SetSchedule(float time, System.Action<Schedule> func)
    {
        Schedule sc = new Schedule(time, func);
        mFront.Add(sc);
        return sc;
    }

    public Schedule SetSchedule(float time, bool usingWorldTimeScale, System.Action<Schedule> func)
    {
        Schedule sc = new Schedule(time, usingWorldTimeScale, func);
        mFront.Add(sc);
        return sc;
    }

    public void RemoveSchedule(Schedule sc)
    {
        if (sc == null) return;
        mFront.Remove(sc);
        mBack.Remove(sc);
    }

    public void SetSchedule(Schedule sc)
    {
        if (mBack.Contains(sc))
        {
            return;
        }

        if (!mFront.Contains(sc))
        {
            mFront.Add(sc);
        }
    }

    public void Update()
    {
        if (mFront.Count > 0)
        {
            mBack.AddRange(mFront);
            mFront.Clear();
        }

        float dt = Time.deltaTime;

        for (int i = 0; i < mBack.Count; i++)
        {

            if (mBack[i].Canceled)
            {
                mGarbrage.Add(mBack[i]);
                continue;
            }
            Schedule sc = mBack[i];

            float tmpTime = sc.Time;
            // TODO:  if game is pause,InGameTimeScale value is 0.0f;
            float InGameTimeScale = 1.0f;
            tmpTime -= sc.UsingWorldTimeScale ? (dt * InGameTimeScale) : dt;
            //tmpTime -= dt;
            sc.ResetTime(tmpTime);
            if (tmpTime <= 0)
            {
                if (sc.Callback != null)
                {
                    sc.Callback(sc);
                }

                // 如果重置了时间，不要放进去
                if (sc.Time <= 0)
                {
                    mGarbrage.Add(sc);
                }
            }
        }

        for (int i = 0; i < mGarbrage.Count; i++)
        {
            if (mBack.Contains(mGarbrage[i]))
            {
                mBack.Remove(mGarbrage[i]);
            }
        }
        mGarbrage.Clear();
    }

    // 双缓冲，防止计时器回调时更改计时器队列
    List<Schedule> mFront = new List<Schedule>();
    List<Schedule> mBack = new List<Schedule>();
    List<Schedule> mGarbrage = new List<Schedule>();
}
#endregion

#region 中文转换数字 
public class ChineseToNum
{
    /// <summary>  
    /// 转换数字  
    /// </summary>  
    protected static long CharToNumber(char c)
    {
        switch (c)
        {
            case '一': return 1;
            case '二': return 2;
            case '三': return 3;
            case '四': return 4;
            case '五': return 5;
            case '六': return 6;
            case '七': return 7;
            case '八': return 8;
            case '九': return 9;
            case '零': return 0;
            default: return -1;
        }
    }

    /// <summary>  
    /// 转换单位  
    /// </summary>  
    protected static long CharToUnit(char c)
    {
        switch (c)
        {
            case '十': return 10;
            case '百': return 100;
            case '千': return 1000;
            case '万': return 10000;
            case '亿': return 100000000;
            default: return 1;
        }
    }
    /// <summary>  
    /// 将中文数字转换阿拉伯数字  
    /// </summary>  
    /// <param name="cnum">汉字数字</param>  
    /// <returns>长整型阿拉伯数字</returns>  
    public static long ParseCnToInt(string cnum)
    {
        cnum = Regex.Replace(cnum, "\\s+", "");
        long firstUnit = 1;//一级单位                  
        long secondUnit = 1;//二级单位   
        long tmpUnit = 1;//临时单位变量  
        long result = 0;//结果  
        for (int i = cnum.Length - 1; i > -1; --i)//从低到高位依次处理  
        {
            tmpUnit = CharToUnit(cnum[i]);//取出此位对应的单位  
            if (tmpUnit > firstUnit)//判断此位是数字还是单位  
            {
                firstUnit = tmpUnit;//是的话就赋值,以备下次循环使用  
                secondUnit = 1;
                if (i == 0)//处理如果是"十","十一"这样的开头的  
                {
                    result += firstUnit * secondUnit;
                }
                continue;//结束本次循环  
            }
            else if (tmpUnit > secondUnit)
            {
                secondUnit = tmpUnit;
                continue;
            }
            result += firstUnit * secondUnit * CharToNumber(cnum[i]);//如果是数字,则和单位想乘然后存到结果里  
        }
        return result;
    }

    #region 数字转中文
    public static string NumToChinese(int i)
    {
        switch (i)
        {
            default: return "零";
            case 1: return "一";
            case 2: return "二";
            case 3: return "三";
            case 4: return "四";
            case 5: return "五";
            case 6: return "六";
            case 7: return "七";
            case 8: return "八";
            case 9: return "九";
        }
    }
    #endregion
}

#endregion

#region 加密
public class Encryption
{

    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public static string ReplaceValue(string str)
    {
        for (mdicServer.Begin(); mdicServer.Next();)
        {
            str = str.Replace(mdicServer.Key, mdicServer.Value);
        }
        //Debug.Log(str);
        return str;


    }

    public static Map<char, char> mdicServer = new Map<char, char>()
    {
        {'ā','0'},
        {'±','0'},
        {'ě','0'},
        {'ξ','0'},
        {'♂','1'},
        {'τ','1'},
        {'ο','1'},
        {'δ','1'},
        {'φ','2'},
        {'ю','2'},
        {'é','2'},
        {'ī','2'},
        {'ш','3'},
        {'ō','3'},
        {'ē','3'},
        {'п','3'},
        {'б','4'},
        {'ь','4'},
        {'ǒ','4'},
        {'я','4'},
        {'＋','5'},
        {'á','5'},
        {'σ','5'},
        {'α','5'},
        {'ó','6'},
        {'∩','6'},
        {'ǎ','6'},
        {'ε','6'},
        {'í','7'},
        {'ò','7'},
        {'ì','7'},
        {'°','7'},
        {'à','8'},
        {'○','8'},
        {'ψ','8'},
        {'♀','8'},
        {'ζ','9'},
        {'ョ','9'},
        {'è','9'},
        {'ǐ','9'},
        {'"','9'},
        {'λ','"'},
        {'ǘ','"'},
        {'ǔ','"'},
        {'ι','"'},
        {'ǚ',','},
        {'˙',','},
        {'ù',','},
        {'ü',','},
        {'ǖ','['},
        {'ū','['},
        {'ё','['},
        {'з','['},
        {'ǜ',']'},
        {'ú',']'},
        {'ω',']'},
        {'э',']'},
    };

    #region 配置文件加密
    public static Dictionary<object, object> encryptDic = null;

    public static Dictionary<object, object> deencryptDic = null;

    public static void InitConfigEncryptDic()
    {
        encryptDic = new Dictionary<object, object>();
        deencryptDic = new Dictionary<object, object>();

        deencryptDic.Add('ㄋ', '"');
        deencryptDic.Add('δ', '"');
        deencryptDic.Add('ひ', '"');
        encryptDic.Add('"', "ㄋδひ");
        deencryptDic.Add('Β', ',');
        deencryptDic.Add('θ', ',');
        deencryptDic.Add('ゃ', ',');
        encryptDic.Add(',', "Βθゃ");
        deencryptDic.Add('ュ', '.');
        deencryptDic.Add('Е', '.');
        deencryptDic.Add('け', '.');
        encryptDic.Add('.', "ュЕけ");
        deencryptDic.Add('Α', '/');
        deencryptDic.Add('ㄍ', '/');
        deencryptDic.Add('て', '/');
        encryptDic.Add('/', "Αㄍて");
        deencryptDic.Add('С', '0');
        deencryptDic.Add('Ι', '0');
        deencryptDic.Add('Ψ', '0');
        encryptDic.Add('0', "СΙΨ");
        deencryptDic.Add('は', '1');
        deencryptDic.Add('ス', '1');
        deencryptDic.Add('λ', '1');
        encryptDic.Add('1', "はスλ");
        deencryptDic.Add('ト', '2');
        deencryptDic.Add('ㄧ', '2');
        deencryptDic.Add('η', '2');
        encryptDic.Add('2', "トㄧη");
        deencryptDic.Add('せ', '3');
        deencryptDic.Add('κ', '3');
        deencryptDic.Add('Χ', '3');
        encryptDic.Add('3', "せκΧ");
        deencryptDic.Add('﹄', '4');
        deencryptDic.Add('И', '4');
        deencryptDic.Add('ィ', '4');
        encryptDic.Add('4', "﹄Иィ");
        deencryptDic.Add('く', '5');
        deencryptDic.Add('ミ', '5');
        deencryptDic.Add('П', '5');
        encryptDic.Add('5', "くミП");
        deencryptDic.Add('Ζ', '6');
        deencryptDic.Add('ι', '6');
        deencryptDic.Add('ㄗ', '6');
        encryptDic.Add('6', "Ζιㄗ");
        deencryptDic.Add('ㄝ', '7');
        deencryptDic.Add('か', '7');
        deencryptDic.Add('β', '7');
        encryptDic.Add('7', "ㄝかβ");
        deencryptDic.Add('μ', '8');
        deencryptDic.Add('З', '8');
        deencryptDic.Add('ゑ', '8');
        encryptDic.Add('8', "μЗゑ");
        deencryptDic.Add('α', '9');
        deencryptDic.Add('Ν', '9');
        deencryptDic.Add('Ш', '9');
        encryptDic.Add('9', "αΝШ");
        deencryptDic.Add('У', ':');
        deencryptDic.Add('ね', ':');
        deencryptDic.Add('テ', ':');
        encryptDic.Add(':', "Уねテ");
        deencryptDic.Add('し', 'A');
        deencryptDic.Add('А', 'A');
        deencryptDic.Add('Я', 'A');
        encryptDic.Add('A', "しАЯ");
        deencryptDic.Add('Ф', 'B');
        deencryptDic.Add('ゐ', 'B');
        deencryptDic.Add('サ', 'B');
        encryptDic.Add('B', "Фゐサ");
        deencryptDic.Add('︿', 'C');
        deencryptDic.Add('ソ', 'C');
        deencryptDic.Add('き', 'C');
        encryptDic.Add('C', "︿ソき");
        deencryptDic.Add('ァ', 'D');
        deencryptDic.Add('ξ', 'D');
        deencryptDic.Add('Ε', 'D');
        encryptDic.Add('D', "ァξΕ");
        deencryptDic.Add('Υ', 'E');
        deencryptDic.Add('ㄔ', 'E');
        deencryptDic.Add('Γ', 'E');
        encryptDic.Add('E', "ΥㄔΓ");
        deencryptDic.Add('ャ', 'F');
        deencryptDic.Add('Л', 'F');
        deencryptDic.Add('∑', 'F');
        encryptDic.Add('F', "ャЛ∑");
        deencryptDic.Add('ㄒ', 'G');
        deencryptDic.Add('ㄈ', 'G');
        deencryptDic.Add('ゎ', 'G');
        encryptDic.Add('G', "ㄒㄈゎ");
        deencryptDic.Add('ο', 'H');
        deencryptDic.Add('フ', 'H');
        deencryptDic.Add('ㄤ', 'H');
        encryptDic.Add('H', "οフㄤ");
        deencryptDic.Add('Μ', 'I');
        deencryptDic.Add('γ', 'I');
        deencryptDic.Add('カ', 'I');
        encryptDic.Add('I', "Μγカ");
        deencryptDic.Add('υ', 'J');
        deencryptDic.Add('Т', 'J');
        deencryptDic.Add('ツ', 'J');
        encryptDic.Add('J', "υТツ");
        deencryptDic.Add('ョ', 'K');
        deencryptDic.Add('﹁', 'K');
        deencryptDic.Add('χ', 'K');
        encryptDic.Add('K', "ョ﹁χ");
        deencryptDic.Add('Б', 'L');
        deencryptDic.Add('ρ', 'L');
        deencryptDic.Add('Ч', 'L');
        encryptDic.Add('L', "БρЧ");
        deencryptDic.Add('ぃ', 'M');
        deencryptDic.Add('Д', 'M');
        deencryptDic.Add('Г', 'M');
        encryptDic.Add('M', "ぃДГ");
        deencryptDic.Add('σ', 'N');
        deencryptDic.Add('ψ', 'N');
        deencryptDic.Add('も', 'N');
        encryptDic.Add('N', "σψも");
        deencryptDic.Add('ㄘ', 'O');
        deencryptDic.Add('ケ', 'O');
        deencryptDic.Add('Ρ', 'O');
        encryptDic.Add('O', "ㄘケΡ");
        deencryptDic.Add('ㄛ', 'P');
        deencryptDic.Add('︺', 'P');
        deencryptDic.Add('ク', 'P');
        encryptDic.Add('P', "ㄛ︺ク");
        deencryptDic.Add('ッ', 'Q');
        deencryptDic.Add('ζ', 'Q');
        deencryptDic.Add('Й', 'Q');
        encryptDic.Add('Q', "ッζЙ");
        deencryptDic.Add('を', 'R');
        deencryptDic.Add('ㄏ', 'R');
        deencryptDic.Add('ナ', 'R');
        encryptDic.Add('R', "をㄏナ");
        deencryptDic.Add('ω', 'S');
        deencryptDic.Add('ㄨ', 'S');
        deencryptDic.Add('ほ', 'S');
        encryptDic.Add('S', "ωㄨほ");
        deencryptDic.Add('ヘ', 'T');
        deencryptDic.Add('В', 'T');
        deencryptDic.Add('シ', 'T');
        encryptDic.Add('T', "ヘВシ");
        deencryptDic.Add('Ω', 'U');
        deencryptDic.Add('ヮ', 'U');
        deencryptDic.Add('Х', 'U');
        encryptDic.Add('U', "ΩヮХ");
        deencryptDic.Add('ぅ', 'V');
        deencryptDic.Add('の', 'V');
        deencryptDic.Add('︶', 'V');
        encryptDic.Add('V', "ぅの︶");
        deencryptDic.Add('Ж', 'W');
        deencryptDic.Add('ハ', 'W');
        deencryptDic.Add('ε', 'W');
        encryptDic.Add('W', "Жハε");
        deencryptDic.Add('Ы', 'X');
        deencryptDic.Add('Э', 'X');
        deencryptDic.Add('Σ', 'X');
        encryptDic.Add('X', "ЫЭΣ");
        deencryptDic.Add('ㄣ', 'Y');
        deencryptDic.Add('Ξ', 'Y');
        deencryptDic.Add('︾', 'Y');
        encryptDic.Add('Y', "ㄣΞ︾");
        deencryptDic.Add('コ', 'Z');
        deencryptDic.Add('ㄡ', 'Z');
        deencryptDic.Add('︷', 'Z');
        encryptDic.Add('Z', "コㄡ︷");
        deencryptDic.Add('Τ', '[');
        deencryptDic.Add('︹', '[');
        deencryptDic.Add('こ', '[');
        encryptDic.Add('[', "Τ︹こ");
        deencryptDic.Add('セ', ']');
        deencryptDic.Add('Ъ', ']');
        deencryptDic.Add('そ', ']');
        encryptDic.Add(']', "セЪそ");
        deencryptDic.Add('︸', '_');
        deencryptDic.Add('М', '_');
        deencryptDic.Add('ホ', '_');
        encryptDic.Add('_', "︸Мホ");
        deencryptDic.Add('ㄌ', 'a');
        deencryptDic.Add('ㄕ', 'a');
        deencryptDic.Add('め', 'a');
        encryptDic.Add('a', "ㄌㄕめ");
        deencryptDic.Add('ㄇ', 'b');
        deencryptDic.Add('﹂', 'b');
        deencryptDic.Add('ㄜ', 'b');
        encryptDic.Add('b', "ㄇ﹂ㄜ");
        deencryptDic.Add('さ', 'c');
        deencryptDic.Add('ち', 'c');
        deencryptDic.Add('Ё', 'c');
        encryptDic.Add('c', "さちЁ");
        deencryptDic.Add('∏', 'd');
        deencryptDic.Add('ぁ', 'd');
        deencryptDic.Add('ㄩ', 'd');
        encryptDic.Add('d', "∏ぁㄩ");
        deencryptDic.Add('キ', 'e');
        deencryptDic.Add('φ', 'e');
        deencryptDic.Add('ㄞ', 'e');
        encryptDic.Add('e', "キφㄞ");
        deencryptDic.Add('メ', 'f');
        deencryptDic.Add('タ', 'f');
        deencryptDic.Add('ヲ', 'f');
        encryptDic.Add('f', "メタヲ");
        deencryptDic.Add('と', 'g');
        deencryptDic.Add('ㄑ', 'g');
        deencryptDic.Add('Κ', 'g');
        encryptDic.Add('g', "とㄑΚ");
        deencryptDic.Add('ㄖ', 'h');
        deencryptDic.Add('ヰ', 'h');
        deencryptDic.Add('Ο', 'h');
        encryptDic.Add('h', "ㄖヰΟ");
        deencryptDic.Add('︽', 'i');
        deencryptDic.Add('へ', 'i');
        deencryptDic.Add('К', 'i');
        encryptDic.Add('i', "︽へК");
        deencryptDic.Add('Φ', 'j');
        deencryptDic.Add('О', 'j');
        deencryptDic.Add('ㄦ', 'j');
        encryptDic.Add('j', "ΦОㄦ");
        deencryptDic.Add('Р', 'k');
        deencryptDic.Add('ゅ', 'k');
        deencryptDic.Add('Н', 'k');
        encryptDic.Add('k', "РゅН");
        deencryptDic.Add('ゥ', 'l');
        deencryptDic.Add('π', 'l');
        deencryptDic.Add('ㄙ', 'l');
        encryptDic.Add('l', "ゥπㄙ");
        deencryptDic.Add('Ь', 'm');
        deencryptDic.Add('ヌ', 'm');
        deencryptDic.Add('ν', 'm');
        encryptDic.Add('m', "Ьヌν");
        deencryptDic.Add('ヴ', 'n');
        deencryptDic.Add('ム', 'n');
        deencryptDic.Add('た', 'n');
        encryptDic.Add('n', "ヴムた");
        deencryptDic.Add('チ', 'o');
        deencryptDic.Add('む', 'o');
        deencryptDic.Add('Π', 'o');
        encryptDic.Add('o', "チむΠ");
        deencryptDic.Add('ふ', 'p');
        deencryptDic.Add('ヒ', 'p');
        deencryptDic.Add('Δ', 'p');
        encryptDic.Add('p', "ふヒΔ");
        deencryptDic.Add('ネ', 'q');
        deencryptDic.Add('っ', 'q');
        deencryptDic.Add('モ', 'q');
        encryptDic.Add('q', "ネっモ");
        deencryptDic.Add('つ', 'r');
        deencryptDic.Add('ニ', 'r');
        deencryptDic.Add('ㄎ', 'r');
        encryptDic.Add('r', "つニㄎ");
        deencryptDic.Add('Ц', 's');
        deencryptDic.Add('Щ', 's');
        deencryptDic.Add('す', 's');
        encryptDic.Add('s', "ЦЩす");
        deencryptDic.Add('な', 't');
        deencryptDic.Add('﹃', 't');
        deencryptDic.Add('ん', 't');
        encryptDic.Add('t', "な﹃ん");
        deencryptDic.Add('に', 'u');
        deencryptDic.Add('︵', 'u');
        deencryptDic.Add('Λ', 'u');
        encryptDic.Add('u', "に︵Λ");
        deencryptDic.Add('ぉ', 'v');
        deencryptDic.Add('ヶ', 'v');
        deencryptDic.Add('Ю', 'v');
        encryptDic.Add('v', "ぉヶЮ");
        deencryptDic.Add('ㄉ', 'w');
        deencryptDic.Add('∧', 'w');
        deencryptDic.Add('ヱ', 'w');
        encryptDic.Add('w', "ㄉ∧ヱ");
        deencryptDic.Add('ま', 'x');
        deencryptDic.Add('τ', 'x');
        deencryptDic.Add('Η', 'x');
        encryptDic.Add('x', "まτΗ");
        deencryptDic.Add('み', 'y');
        deencryptDic.Add('ㄆ', 'y');
        deencryptDic.Add('ォ', 'y');
        encryptDic.Add('y', "みㄆォ");
        deencryptDic.Add('ㄢ', 'z');
        deencryptDic.Add('ン', 'z');
        deencryptDic.Add('ㄚ', 'z');
        encryptDic.Add('z', "ㄢンㄚ");
        deencryptDic.Add('ぇ', '{');
        deencryptDic.Add('﹀', '{');
        deencryptDic.Add('Θ', '{');
        encryptDic.Add('{', "ぇ﹀Θ");
        deencryptDic.Add('ㄠ', '}');
        deencryptDic.Add('ㄊ', '}');
        deencryptDic.Add('ょ', '}');
        encryptDic.Add('}', "ㄠㄊょ");
    }

    public static String EncrypConfig(String data)
    {
        if (encryptDic == null) InitConfigEncryptDic();
        StringBuilder finalData = new StringBuilder();
        System.Random random = new System.Random();
        for (int i = 0; i < data.Length; i++)
        {
            if (encryptDic.ContainsKey(data[i]))
                finalData.Append(encryptDic[data[i]].ToString()[random.Next(3)]);
            else
                finalData.Append(data[i]);
        }
        return finalData.ToString();
    }

    public static String DeEncrypConfig(String data)
    {
        if (deencryptDic == null) InitConfigEncryptDic();
        StringBuilder finalData = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            if (deencryptDic.ContainsKey(data[i]))
                finalData.Append(deencryptDic[data[i]].ToString());
            else
                finalData.Append(data[i]);
        }
        return finalData.ToString();
    }

    #endregion
}
#endregion