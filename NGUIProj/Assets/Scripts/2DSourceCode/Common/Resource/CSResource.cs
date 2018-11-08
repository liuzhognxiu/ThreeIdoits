
//-------------------------------------------
//资源
//author jiabao
//time 2015.12.28
//-------------------------------------------
using System.Diagnostics;
using System.IO;
using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
public enum ResourceType
{
    None=0,
    Player=1,
    PlayerAtlas=2,
    Monster=3,
    MonsterAtlas=4,
    Npc=5,
    NpcAtlas=6,
    Weapon=7,
    WeaponAtlas=8,
    Effect=9,
    EffectAtlas=10,
    Skill=11,
    SkillAtlas=12,
    TableBytes=13,
    Map=14,
    MapBytes=15,
    UIItem=17,
    MiniMap=18,
    Wing=19,
    WingAtlas=20,
    Audio=21,
    UIMountAtlas = 22,
    ScaleMap = 24,
    UIEffect = 25,
    UIWingTex = 26,
    UIPlayer = 27,
    UIWeapon = 28,
    SceneRes = 29,
    ResourceRes = 30,
}

public enum ResourceAssistType//数字越大，优先级越高
{
    None,
    OtherPet,//宠物和法神
    Player,//其他玩家
    Terrain,//地形
    Monster,//怪物
    NPC,//NPC
    CharactarPet,
    Charactar,//较地形之前
    UI,//UI
    QueueDeal,//一个加载完，才加载下一次
    ForceLoad,//不管当前正在加载的是什么，都强制抛出加载
}
/// <summary>
/// 支持回调参数是Object对象,清除所有回调
/// </summary>
public class CSEventDelegate<T>
{
    public delegate void OnLoaded(T obj);
    CSBetterList<OnLoaded> onLoadedList = new CSBetterList<OnLoaded>();

    void AddCallBack(OnLoaded onLoaded)
    {
        onLoadedList.Insert(0,onLoaded);//保证调用的先后性,插到最前面,调用是从最后一个往前调用的
    }

    public void AddFrontCallBack(OnLoaded onLoaded)
    {
        onLoadedList.Add(onLoaded);
    }

    void RemoveCallBack(OnLoaded onloaded)
    {
        onLoadedList.Remove(onloaded);
    }

    public void CallBack(T obj)
    {
        for (int i = onLoadedList.Count - 1; i >=0 ; i--)//防止回调里面做了-=的操作
        {
            if (onLoadedList[i] != null)
                onLoadedList[i](obj);
        }
    }

    public void Clear()
    {
        onLoadedList.Clear();
    }

    public void Release()
    {
        onLoadedList.Release();
    }

    public static CSEventDelegate<T> operator +(CSEventDelegate<T> dele, OnLoaded onload)
    {
        dele.AddCallBack(onload);
        return dele;
    }

    public static CSEventDelegate<T> operator -(CSEventDelegate<T> dele, OnLoaded onload)
    {
        dele.RemoveCallBack(onload);
        return dele;
    }
}

public class CSResource
{
    public UnityEngine.Object MirrorObj;
    public byte[] MirroyBytes;
    protected string fileName;
    private static string applicationDataPath = string.Empty;
#if (UNITY_4_7||UNITY_4_6)
    private static string assetbunldeStr = ".assetbundle";
#else
    private static string assetbunldeStr = "";
#endif
    protected ResourceAssistType mResourceAssistType = ResourceAssistType.None;
    public int waitingCallBackCount = 0;
    public bool isHotLoading = false;
    public bool isResValid
    {
        get
        {
            if (MirrorObj != null || MirroyBytes != null) return true;
            return false;
        }
    }//下载失败的资源切换场景后需要删除,只针对已经在下载完成的列表
    public ResourceAssistType AssistType
    {
        get { return mResourceAssistType; }
        set { mResourceAssistType = value; }
    }

    public float loadedTime = 0;

    //为了过度数据用的-------贾宝
    public int buffValue = 0;

    private bool mIsCanBeDelete = true;
    public bool IsCanBeDelete
    {
        get { return mIsCanBeDelete; }
        set {
            if (mIsCanBeDelete == value) return;
            mIsCanBeDelete = value;
        }
    }

    public string FileName
    {
        get
        {
            return fileName;
        }
        set
        {
            fileName = value;
        }
    }

    /// <summary>
    /// 辅助Key，主要在模型加载里面用
    /// </summary>
    private long mKey = 0;
    public long Key
    {
        get { return mKey; }
        set { mKey = value; }
    }

    public static string GetPath(string fileName, ResourceType LocalType,bool isLocal)
    {
        string path = "";
        if (isLocal)
        {
            CSStringBuilder.Clear();
            CSStringBuilder.Append(GetModelTypePath(LocalType), fileName);
            path = CSStringBuilder.ToString();//Resource.Load(存的是相对路径)
        }
        else
        {
            CSStringBuilder.Clear();

            bool isAssetBundle = false;
            switch (LocalType)
            {
                case ResourceType.Map:
                case ResourceType.MiniMap:
                case ResourceType.Player:
                case ResourceType.PlayerAtlas:
                case ResourceType.Monster:
                case ResourceType.MonsterAtlas:
                case ResourceType.Npc:
                case ResourceType.NpcAtlas:
                case ResourceType.Weapon:
                case ResourceType.WeaponAtlas:
                case ResourceType.Effect:
                case ResourceType.EffectAtlas:
                case ResourceType.Skill:
                case ResourceType.SkillAtlas:
                case ResourceType.UIItem:
                case ResourceType.Audio:
                case ResourceType.Wing:
                case ResourceType.WingAtlas:
                case ResourceType.UIMountAtlas:
                case ResourceType.ScaleMap:
                case ResourceType.UIEffect:
                case ResourceType.UIWingTex:
                case ResourceType.UIPlayer:
                case ResourceType.UIWeapon:
                case ResourceType.SceneRes:
                case ResourceType.ResourceRes:
                    {
                        isAssetBundle = true;
                    }
                    break;
            }

#if UNITY_EDITOR
            if (SFOut.IGame.IsLoadLocalRes)
            {
                if (string.IsNullOrEmpty(applicationDataPath)) applicationDataPath = Application.dataPath;

                if (UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup == UnityEditor.BuildTargetGroup.iOS)
                {
                    path = applicationDataPath.Replace("Client/Branch/ClientIos/Assets", "Data/Branch/CurrentUseData/Normal/wzcq_ios/");
                }
                else
                {
                    path = applicationDataPath + "/../../Normal/wzcq_android/";
                    //path = applicationDataPath.Replace("Client/Branch/WZAndroidOrigin/Assets", "Data/Branch/CurrentUseData/Normal/wzcq_android/");
                }
                path = CSStringBuilder.Append("file://", path, GetModelTypePath(LocalType), fileName, isAssetBundle ? assetbunldeStr : "").ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(applicationDataPath)) applicationDataPath = Application.persistentDataPath;
                path = CSStringBuilder.Append("file:///", applicationDataPath, "/", GetModelTypePath(LocalType), fileName, isAssetBundle ? assetbunldeStr : "").ToString();
            }
#else
            if (string.IsNullOrEmpty(applicationDataPath)) applicationDataPath = Application.persistentDataPath;
            path = CSStringBuilder.Append("file://", applicationDataPath, "/", GetModelTypePath(LocalType), fileName, isAssetBundle ? assetbunldeStr : "").ToString();
#endif
        }
        return path;
    }

    protected string mPath;
    public string Path
    {
        get { return mPath; }
        set { mPath = value; }
    }

    public bool IsDone = false;

    private float mTime;
    public float Time
    {
        get { return mTime; }
        set { mTime = value; }
    }

        private ResourceType mLocalType;
    public ResourceType LocalType
    {
        get { return mLocalType; }
        set { mLocalType = value; }
    }
    public CSEventDelegate<CSResource> onLoaded = new CSEventDelegate<CSResource>();

    public void ReleaseCallBack()
    {
        onLoaded.Release();
    }

	public CSResource(string name,string path, ResourceType type)
    {
        fileName = name;
        LocalType = type;
    }

    public static string GetModelTypePath(ResourceType type)
    {
        string t = "";
#if (UNITY_4_7 || UNITY_4_6)
        switch (type)
        {
            case ResourceType.Player: return t = "Model/Player/";
            case ResourceType.PlayerAtlas: return t = "Model/Player/Atlas/";
            case ResourceType.Monster: return t = "Model/Monster/";
            case ResourceType.MonsterAtlas: return t = "Model/Monster/Atlas/";
            case ResourceType.Npc: return t = "Model/NPC/";
            case ResourceType.NpcAtlas: return t = "Model/NPC/Atlas/";
            case ResourceType.Weapon: return t = "Model/Weapon/";
            case ResourceType.WeaponAtlas: return t = "Model/Weapon/Atlas/";
            case ResourceType.Effect: return t = "Model/Effect/";
            case ResourceType.EffectAtlas: return t = "Model/Effect/Atlas/";
            case ResourceType.Skill: return t = "Model/Skill/";
            case ResourceType.SkillAtlas: return t = "Model/Skill/Atlas/";
            case ResourceType.TableBytes: return t = "Table/";
            case ResourceType.Map: return t = "Map/";
            case ResourceType.MapBytes: return t = "Map/";
            case ResourceType.UI: return t = "UI/Prefabs/";
            case ResourceType.UIItem: return t = "UI/UIAtlas/";
            case ResourceType.MiniMap: return t = "MiniMap/";
            case ResourceType.Wing: return t = "Model/Wing/";
            case ResourceType.WingAtlas: return t = "Model/Wing/Atlas/";
            case ResourceType.Audio: return t = "Audio/";
            case ResourceType.UIWingAtlas: return t = "Model/UIWing/Atlas/";
            case ResourceType.UIMountAtlas: return t = "Model/UIZuoqi/Atlas/";
            case ResourceType.ScaleMap: return t = "ScaleMap/";
            case ResourceType.UIEffect: return t = "Model/UIEffect/";
            case ResourceType.UIWingTex: return t = "Model/UIWing/";
            case ResourceType.UIPlayer: return t = "Model/UIPlayer/";
            case ResourceType.UIWeapon: return t = "Model/UIWeapon/";
        }
#else
        switch (type)
        {
            case ResourceType.PlayerAtlas: return t = "Model/Player/";
            case ResourceType.MonsterAtlas: return t = "Model/Monster/";
            case ResourceType.NpcAtlas: return t = "Model/NPC/";
            case ResourceType.WeaponAtlas: return t = "Model/Weapon/";
            case ResourceType.Effect: return t = "Model/Effect/";
            case ResourceType.EffectAtlas: return t = "Model/Effect/Atlas/";
            case ResourceType.SkillAtlas: return t = "Model/Skill/";
            case ResourceType.TableBytes: return t = "Table/";
            case ResourceType.Map: return t = "Map/";
            case ResourceType.MapBytes: return t = "Map/";
            case ResourceType.MiniMap: return t = "MiniMap/";
            case ResourceType.WingAtlas: return t = "Model/Wing/";
            case ResourceType.Audio: return t = "Audio/";
            case ResourceType.UIMountAtlas: return t = "Model/UIZuoqi/";
            case ResourceType.ScaleMap: return t = "ScaleMap/";
            case ResourceType.UIEffect: return t = "Model/UIEffect/";
            case ResourceType.UIWingTex: return t = "Model/UIWing/";
            case ResourceType.UIPlayer: return t = "Model/UIPlayer/";
            case ResourceType.UIWeapon: return t = "Model/UIWeapon/";
            case ResourceType.SceneRes: return t = "SceneRes/";
            case ResourceType.ResourceRes: return t = "ResourceRes/";
            default:
                //if (Debug.developerConsoleVisible) Debug.Log("Not Type filled = " + type);
                break;
        }
#endif
        return t;
    }

    public virtual void Load()
    {

    }

    public UnityEngine.Object GetObjInst()
    {
        if (MirrorObj == null) return null;

        if (null != MirrorObj as Texture) return MirrorObj;

        return UnityEngine.Object.Instantiate(MirrorObj);
    }

    public virtual void UpdateLoading()
    {

    }

    public virtual void ResHotUpdateCallBack_HttpLoad()
    {

    }
}
