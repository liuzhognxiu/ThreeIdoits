using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResMgr : MonoBehaviour
{
    private static ResMgr s_Instance = null;
    private Dictionary<string, AssetPack> m_dicAsset = new Dictionary<string, AssetPack>();

    void Awake()
    {
        s_Instance = this;

    }


    public static ResMgr Instance
    {
        get
        {
            return s_Instance;
        }
    }

    public static bool IsValid
    {
        get
        {
            return s_Instance != null;
        }
    }

    public UnityEngine.Shader LoadShaderAssetFromResource(string assetName, bool isKeepInMemory = false, System.Type type = null)
    {
        string abname = assetName + ".unity3d";
        int index = assetName.LastIndexOf('/');
        string assetname = assetName;
        if (index > 0)
        {
            assetname = assetName.Substring(index + 1, assetName.Length - index - 1);
        }
        return ResourceController.Instance.LoadShader(abname, assetname);
    }

    public UnityEngine.AudioClip LoadAudioClipAssetFromResource(string assetName, bool isKeepInMemory = false, System.Type type = null)
    {
        string abname = assetName + ".unity3d";
        int index = assetName.LastIndexOf('/');
        string assetname = assetName;
        if (index > 0)
        {
            assetname = assetName.Substring(index + 1, assetName.Length - index - 1);
        }
        return ResourceController.Instance.LoadAudioClip(abname, assetname);
    }

    public UnityEngine.Object LoadAssetFromResource(string assetName, bool isKeepInMemory = false, System.Type type = null)
    {
        //AssetPack assetPack = null;
        //if (!m_dicAsset.TryGetValue(assetName, out assetPack) || assetPack == null)
        //{
        //    assetPack = _LoadAssetFromResource(assetName, isKeepInMemory, type);
        //    if (assetPack != null) AddAssetToTile(assetName);
        //}
        ////如果指明了要keepInMemory//
        //if (assetPack != null) 
        //    assetPack.isKeepInMemory = isKeepInMemory ? isKeepInMemory : assetPack.isKeepInMemory;
        //else 
        //    return null;
        //return assetPack.asset;

        string abname = assetName + ".unity3d";
        int index = assetName.LastIndexOf('/');
        string assetname = assetName;
        if (index > 0)
        {
            assetname = assetName.Substring(index + 1, assetName.Length - index - 1);
        }
        return ResourceController.Instance.LoadAsset(abname, assetname);
    }

    public void LoadAssetAndInitialize(string assetName, Transform parent, Vector3 pos, System.Action<GameObject> call = null)
    {
        UnityEngine.Object ret = LoadAssetFromResource(assetName);
        if (ret == null)
        {
            AssetBundleReader reader = AssetBundleReaderManager.Instance.LoadAssetBundle(assetName);
            AssetPack ap = null;
            CoroutineAgent.WaitOperation(
                () => reader.IsLoaded,
                () =>
                {
                    if (reader.IsLoaded && reader.AssetBundleData != null)
                    {
                        ret = reader.AssetBundleData.LoadAsset(assetName.AssetFileName().NoExtension());
                        reader.UnLoadAssetBundle();
                    }

                    if (ret != null)
                    {
                        if (!m_dicAsset.ContainsKey(assetName))
                        {
                            ap = new AssetPack(ret, false);
                            m_dicAsset.Add(assetName, ap);
                        }
                        else
                        {
                            ap = m_dicAsset[assetName];
                            Debug.logger.LogWarning("ResMgr", "ResMgr.LoadAssetAndInitialize: asset name->" + assetName + ", already in asset pool");
                        }
                    }
                    else
                    {
                        Debug.logger.LogError("ResMgr", "ResMgr.LoadAssetAndInitialize: assetName->" + assetName + ", finds no asset!");
                    }
                    //targetObj.GetComponent<UITexture>().mainTexture = (Texture)ret;
                    GameObject go = GameObject.Instantiate(ret) as GameObject;
                    go.transform.parent = parent;
                    go.transform.localPosition = pos;
                    go.transform.localScale = Vector3.one;
                    if (call != null)
                        call(go);
                }, this);
        }
        else
        {
            GameObject go = GameObject.Instantiate(ret) as GameObject;
            go.transform.parent = parent;
            go.transform.localPosition = pos;
            go.transform.localScale = Vector3.one;
            if (call != null)
                call(go);
        }
    }

    public void LoadAssetTexture(string assetName, GameObject targetObj, bool isKeepInMemory = false)
    {
        UnityEngine.Object ret = LoadAssetFromResource(assetName);
        if (ret == null)
        {
            AssetBundleReader reader = AssetBundleReaderManager.Instance.LoadAssetBundle(assetName);
            AssetPack ap = null;
            CoroutineAgent.WaitOperation(
                () => reader.IsLoaded,
                () =>
                {
                    if (reader.IsLoaded && reader.AssetBundleData != null)
                    {
                        ret = reader.AssetBundleData.LoadAsset(assetName.AssetFileName().NoExtension());
                        reader.UnLoadAssetBundle();
                    }

                    if (ret != null)
                    {
                        if (!m_dicAsset.ContainsKey(assetName))
                        {
                            ap = new AssetPack(ret, isKeepInMemory);
                            m_dicAsset.Add(assetName, ap);
                        }
                        else
                        {
                            ap = m_dicAsset[assetName];
                            Debug.logger.LogWarning("ResMgr", "ResMgr.LoadAssetTexture: asset name->" + assetName + ", already in asset pool");
                        }
                    }
                    else
                    {
                        Debug.logger.LogError("ResMgr", "ResMgr.LoadAssetTexture: assetName->" + assetName + ", finds no asset!");
                    }
                    targetObj.GetComponent<UITexture>().mainTexture = (Texture)ret;
                }, this);
        }
        else
        {
            targetObj.GetComponent<UITexture>().mainTexture = (Texture)ret;
        }

    }

    /// <summary>
    /// 将资源加入层级
    /// </summary>
    /// <param name="assetName"></param>
    private void AddAssetToTile(string assetName)
    {
        if (m_assetStack.Count == 0)
        {
            m_assetStack.Push(new List<string>());
        }
        List<string> assetTile = m_assetStack.Peek();
        if (!assetTile.Contains(assetName))
        {
            assetTile.Add(assetName);
        }
    }

    private AssetPack _LoadAssetFromResource(string assetName, bool isKeepInMemory, System.Type type)
    {
        AssetPack ret = null;
        UnityEngine.Object asset = null;
        if (type != null)
        {
            asset = Resources.Load(assetName, type);
        }
        else
        {
            asset = Resources.Load(assetName);
        }

        if (asset != null)
        {
            if (!m_dicAsset.ContainsKey(assetName))
            {
                ret = new AssetPack(asset, isKeepInMemory);
                m_dicAsset.Add(assetName, ret);
            }
            else
            {
                ret = m_dicAsset[assetName];
                Debug.logger.LogWarning("ResMgr", "ResMgr._LoadAssetFromResource: asset name->" + assetName + ", already in asset pool");
            }
        }
        else
        {
            //Debug.logger.LogError("ResMgr", "ResMgr._LoadAssetFromResource: assetName->" + assetName + ", finds no asset!");
        }
        return ret;
    }


    // Use this for initialization
    void Start()
    {
        this.gameObject.AddComponent<ResourceController>();
        ResourceController.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 每一层的资源使用统计
    /// </summary>
    private Stack<List<string>> m_assetStack = new Stack<List<string>>();

    private class AssetPack
    {
        /// <summary>
        /// 是否常驻内存
        /// </summary>
        public bool isKeepInMemory = false;

        /// <summary>
        /// 资源
        /// </summary>
        public UnityEngine.Object asset = null;

        /// <summary>
        /// 有多少层再使用本资源
        /// </summary>
        public int stackCount = 0;

        public AssetPack(UnityEngine.Object asse, bool keepInMemory)
        {
            asset = asse;
            isKeepInMemory = keepInMemory;
        }
    }
}
