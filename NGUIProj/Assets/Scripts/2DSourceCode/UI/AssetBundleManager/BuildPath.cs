﻿using UnityEngine;
using System.Collections;

public class BuildPath
{

    private static BuildPath instance = null;

    public static BuildPath _Instance
    {
        get
        {
            if (instance == null)
                instance = new BuildPath();
            return instance;
        }
    }

    private string PCPath = Application.dataPath + "/../Captures/";

    private string AdnroidPath = "/storage/emulated/0/T-ShirtPhotos/";

    private string MacPath = Application.dataPath + "/../Captures/";

    private string IphonePath = Application.dataPath + "/../Captures/";

    /// <summary>
    /// 外部资源存放的路径（AssetBundle、xml、txt）
    /// </summary>
    public string ResourceFolder
    {
        get
        {
            return string.Concat(Application.persistentDataPath, "/Android/");

            //switch (Application.platform)
            //{
            //    case RuntimePlatform.Android:   //安卓
            //    case RuntimePlatform.IPhonePlayer:  //Iphone
            //        return string.Concat(Application.persistentDataPath, "/Android/");
            //    case RuntimePlatform.OSXEditor: //MAC
            //    case RuntimePlatform.OSXPlayer:
            //    case RuntimePlatform.WindowsEditor: //windows
            //    case RuntimePlatform.WindowsPlayer:
            //        return string.Concat(Application.dataPath , "/../Android/");
            //    default:
            //        return string.Concat(Application.dataPath , "/../Android/");

            //}
                
        }
    }

    /// <summary>
    /// 初始资源存放的位置（只读）
    /// </summary>
    public string FileStreamFolder
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:   //安卓
                case RuntimePlatform.IPhonePlayer:  //Iphone
                    return string.Concat(Application.streamingAssetsPath, "/Android/");
                case RuntimePlatform.OSXEditor: //MAC
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsEditor: //windows
                case RuntimePlatform.WindowsPlayer:
                    return string.Concat("file://",Application.streamingAssetsPath, "/Android/");
                default:
                    return string.Concat("file://",Application.streamingAssetsPath, "/Android/");
            }
        }
    }


    /// <summary>
    /// 图片存放的路径
    /// </summary>
    public string TargetTexturePath
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:   //安卓
                    return AdnroidPath;
                case RuntimePlatform.WindowsEditor: //windows
                case RuntimePlatform.WindowsPlayer:
                    return PCPath;
                case RuntimePlatform.OSXEditor: //MAC
                case RuntimePlatform.OSXPlayer:
                    return MacPath;
                case RuntimePlatform.IPhonePlayer:  //Iphone
                    return IphonePath;
                default:
                    return null;
            }
        }
    }


}