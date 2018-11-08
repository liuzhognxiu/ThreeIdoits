using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Helper
{

    static int m_SimulateAssetBundleInEditor = LuaFramework.AppConst.UpdateMode ? 0 : -1;
    const string kSimulateAssetBundles = "SimulateAssetBundles";
    public static bool SimulateAssetBundleInEditor
    {
        get
        {
            if (m_SimulateAssetBundleInEditor == -1)
                m_SimulateAssetBundleInEditor = PlayerPrefs.GetInt(kSimulateAssetBundles, 1);

            return m_SimulateAssetBundleInEditor != 0;
        }
        set
        {
            int newValue = value ? 1 : 0;
            if (newValue != m_SimulateAssetBundleInEditor)
            {
                m_SimulateAssetBundleInEditor = newValue;
                PlayerPrefs.SetInt(kSimulateAssetBundles, newValue);
            }
        }
    }

    public static bool CheckDirectory(string path)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return true;
    }

    public static void GetObjectNamesByExtension(string path, string extension, ref Dictionary<string, string> nameDic)
    {
        string objPath = Application.dataPath + "/" + path;
        string[] directoryEntries;
        try
        {
            //返回指定的目录中文件和子目录的名称的数组或空数组
            directoryEntries = System.IO.Directory.GetFileSystemEntries(objPath);

            for (int i = 0; i < directoryEntries.Length; i++)
            {
                string p = directoryEntries[i];
                //得到要求目录下的文件或者文件夹（一级的）//
                string[] test = { path + "\\" };
                string[] tempPaths = p.Split(test, System.StringSplitOptions.None);

                //tempPaths 分割后的不可能为空,只要directoryEntries不为空//
                if (tempPaths[1].EndsWith(".meta"))
                    continue;

                string[] pathSplit = tempPaths[1].Split('.');
                //文件
                if (pathSplit.Length > 1)
                {
                    if (!tempPaths[1].EndsWith(extension))
                        continue;

                    nameDic.Add(tempPaths[1], p.Replace("\\", "/"));
                }
                //遍历子目录下 递归吧！
                else
                {
                    GetObjectNamesByExtension(path + "/" + pathSplit[0], extension, ref nameDic);
                    continue;
                }
            }
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            Debug.Log("The path encapsulated in the " + objPath + "Directory object does not exist.");
        }
    }

    public static void GetObjectNameToArray(string path, ref Dictionary<string, string> nameDic)
    {
        string objPath = Application.dataPath + "/" + path;
        string[] directoryEntries;
        try
        {
            //返回指定的目录中文件和子目录的名称的数组或空数组
            directoryEntries = System.IO.Directory.GetFileSystemEntries(objPath);

            for (int i = 0; i < directoryEntries.Length; i++)
            {
                string p = directoryEntries[i];
                //得到要求目录下的文件或者文件夹（一级的）//
                string[] test = { path + "\\" };
                string[] tempPaths = p.Split(test, System.StringSplitOptions.None);

                //tempPaths 分割后的不可能为空,只要directoryEntries不为空//
                if (tempPaths[1].EndsWith(".meta"))
                    continue;
                string[] pathSplit = tempPaths[1].Split('.');
                //文件
                if (pathSplit.Length > 1)
                {
                    nameDic.Add(tempPaths[1], p.Replace("\\", "/"));
                }
                //遍历子目录下 递归吧！
                else
                {
                    GetObjectNameToArray(path + "/" + pathSplit[0], ref nameDic);
                    continue;
                }
            }
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            Debug.Log("The path encapsulated in the " + objPath + "Directory object does not exist.");
        }
    }

    public static void SetTweenValue(Component comp, bool tweenFactor)
    {
        SetTweenValue(comp, tweenFactor ? 1 : 0);
    }

    public static void SetTweenValue(Component comp, float tweenFactor)
    {
        if (IsTargetNotNull(comp))
        {
            if (comp is UITweener)
            {
                UITweener tweener = comp as UITweener;
                SetTweenValue(tweener, tweenFactor);
            }
            else
            {
                foreach (UITweener tweener in comp.GetComponents<UITweener>())
                {
                    SetTweenValue(tweener, tweenFactor);
                }
            };
        }
    }

    public static void SetTweenValue(GameObject go, bool tweenFactor)
    {
        SetTweenValue(go, tweenFactor ? 1 : 0);
    }

    public static void SetTweenValue(GameObject go, float tweenFactor)
    {
        if (IsTargetNotNull(go))
        {
            foreach (UITweener tweener in go.GetComponents<UITweener>())
            {
                SetTweenValue(tweener, tweenFactor);
            }
        }
    }

    private static void SetTweenValue(UITweener tweener, float tweenFactor)
    {
        if (tweener)
        {
            tweener.tweenFactor = tweenFactor;
            tweener.Sample(tweenFactor, true);
            tweener.enabled = false;
        }
    }

    public static float PlayForward(Component comp, bool restart)
    {
        if (IsTargetNotNull(comp))
        {
            if (comp is UITweener)
            {
                return PlayForward(comp as UITweener, restart);
            }
            else
            {
                float time = 0;
                foreach (UITweener tweener in ((Component)comp).GetComponents<UITweener>())
                {
                    time = PlayForward(tweener, restart);
                }
                return time;
            };
        }
        return 0;
    }

    public static float PlayForward(GameObject go, bool restart = false)
    {
        if (IsTargetNotNull(go))
        {
            float time = 0;
            foreach (UITweener tweener in go.GetComponents<UITweener>())
            {
                time = PlayForward(tweener, restart);
            }
            return time;
        }
        return 0;
    }

    private static float PlayForward(UITweener tweener, bool restart = false)
    {
        if (tweener)
        {
            if (restart)
            {
                tweener.tweenFactor = 0;
                tweener.Sample(0, true);
            }
            tweener.PlayForward();
            return tweener.delay + tweener.duration;
        }
        return 0;
    }

    public static float PlayReverse(Component comp, bool restart)
    {
        if (IsTargetNotNull(comp))
        {
            if (comp is UITweener)
            {
                return PlayReverse(comp as UITweener, restart);
            }
            else
            {
                float time = 0;
                foreach (UITweener tweener in ((Component)comp).GetComponents<UITweener>())
                {
                    PlayReverse(tweener, restart);
                }
                return time;
            };
        }
        return 0;
    }

    private static float PlayReverse(GameObject go, bool restart = false)
    {
        if (IsTargetNotNull(go))
        {
            float time = 0;
            foreach (UITweener tweener in go.GetComponents<UITweener>())
            {
                PlayReverse(tweener, restart);
            }
            return time;
        }
        return 0;
    }

    private static float PlayReverse(UITweener tweener, bool restart = false)
    {
        if (tweener)
        {
            if (restart)
            {
                tweener.tweenFactor = 1;
                tweener.Sample(1, true);
            }
            tweener.PlayReverse();
            return tweener.delay + tweener.duration;
        }
        return 0;
    }

    private static bool IsTargetNotNull(UnityEngine.Object obj, string message = null)
    {
        return !IsTargetNull(obj, message);
    }

    private static bool IsTargetNull(UnityEngine.Object obj, string message = null)
    {
        if (!obj)
        {
            Debug.LogError(message ?? "Target ui object is null!");
            return true;
        }
        return false;
    }

    public static string UrlEncode(string str)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
        for (int i = 0; i < byStr.Length; i++)
        {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }

        return (sb.ToString().ToUpper());
    }

    //public static DG.Tweening.Core.DOGetter<float> WrapDOGetter_float(LuaInterface.LuaFunction luaFinished)
    //{

    //    DG.Tweening.Core.DOGetter<float> dogetter = new DG.Tweening.Core.DOGetter<float>(() => {
    //        luaFinished.BeginPCall();
    //        luaFinished.PCall();
    //        float ret = (float)luaFinished.CheckNumber();
    //        luaFinished.EndPCall();
    //        return ret;
    //    });
    //    return dogetter;
    //}

    //public static DG.Tweening.Core.DOSetter<float> WrapDOSetter_float(LuaInterface.LuaFunction luaFinished)
    //{

    //    DG.Tweening.Core.DOSetter<float> dosetter = new DG.Tweening.Core.DOSetter<float>((x) => { luaFinished.Call(x); });
    //    return dosetter;
    //}


}
