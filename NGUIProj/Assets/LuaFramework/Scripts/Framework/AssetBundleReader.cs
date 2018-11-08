using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using MogoEngine.Utils;

public enum PathType
{
    PT_Persistent,   //persistentDataPath
    PT_Streaming     //StreamingAssets
}

public class AsyncLoadOperation
{
    public bool IsUpdating
    {
        get;
        set;
    }
    public bool IsFailed
    {
        get;
        set;
    }
    public bool IsDone
    {
        get;
        set;
    }
}

public class AssetBundleReader
{
    private string ServerPath = string.Format("file://{0}/assetBundle/", Environment.CurrentDirectory).PathNormalize();
    private AssetBundleManifest m_manifest = null;
    private AssetBundle[] m_abs;
    public AssetBundleReader(string name)
    {
        IsLoading = false;
        IsLoaded = false;
        if (!name.Equals(string.Empty))
        {
            IsLoading = true;
            IsLoaded = false;

            _LoadOrDownload(ResourceController.DataPath, name);
            //Game.Instance.StartCoroutine(_LoadOrDownload(ResourceController.DataPath, name));
        }
    }

    void _LoadOrDownload(string path, string name)
    {
        AssetBundleData = ResourceController.Instance.LoadAssetBundle(name);
        IsLoaded = true;
        IsLoading = false;

        //return;
        //WWW mwww = new WWW(path + name);
        //yield return mwww;

        //if (mwww.error != null)
        //{
        //    Debug.logger.LogError("Error", "error happens");
        //}

        //if (!string.IsNullOrEmpty(mwww.error))
        //{
        //    Debug.logger.Log(mwww.error);
        //}
        //else
        //{
        //    if (ResourceController.Instance.Manifest != null)
        //    {
        //        m_manifest = ResourceController.Instance.Manifest;
        //    }
        //    else
        //    {
        //        //这里拿到了这个资源包下的所有资源的依赖关系表
        //        AssetBundle mab = mwww.assetBundle;
        //        m_manifest = (AssetBundleManifest)mab.LoadAsset("AssetBundleManifest");
        //        mab.Unload(false);
        //    }
        //}

        ////根据要加载的物件的名字，来取其依赖物件，并加载
        //string[] dps = m_manifest.GetAllDependencies(name);
        //m_abs = new AssetBundle[dps.Length];
        //Debug.logger.Log(name + " " + dps.Length);
        //for (int i = 0; i < dps.Length; i++)
        //{
        //    string dUrl = path + dps[i];
        //    Debug.logger.Log(dUrl);
        //    //这里用来判断这个依赖物件是否是最新的，如果本地已不是最新的了，就下载下来
        //    WWW dwww = WWW.LoadFromCacheOrDownload(dUrl, m_manifest.GetAssetBundleHash(dps[i]));
        //    yield return dwww;
        //    m_abs[i] = dwww.assetBundle;
        //}

        ////这里是判断真正要加载的物件是否本地是最新的，如果不是就再下载下来
        //WWW www = WWW.LoadFromCacheOrDownload(path + name, m_manifest.GetAssetBundleHash(name));
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    AssetBundleData = www.assetBundle;
        //    IsLoaded = true;
        //    IsLoading = false;
        //}
    }

    public void UnLoadAssetBundle()
    {
        if (AssetBundleData != null)
            AssetBundleData.Unload(false);

        //foreach (AssetBundle ab in m_abs)
        //{
        //    ab.Unload(false);
        //}
    }

    public UnityEngine.Object LoadMainAsset()
    {
        if (this.AssetBundleData == null)
        {
            return null;
        }
        return this.AssetBundleData.mainAsset;
    }

    public bool IsLoading
    {
        get;
        set;
    }
    public bool IsLoaded
    {
        get;
        set;
    }

    public AssetBundle AssetBundleData
    {
        get;
        set;
    }
}


public class AssetBundleReaderManager : Singleton<AssetBundleReaderManager>,IInit,IDispose
{
    private Dictionary<string, AssetBundleReader> m_cacheAssets = new Dictionary<string, AssetBundleReader>();
    public AssetBundleReader LoadAssetBundle(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        AssetBundleReader reader = new AssetBundleReader(name);
        return reader;
    }


    public AssetBundleReader LoadStreamBundle(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        if (m_cacheAssets.ContainsKey(name))
        {
            return m_cacheAssets[name];
        }

        AssetBundleReader reader = new AssetBundleReader(name);
        if (reader != null)
            m_cacheAssets.Add(name, reader);

        return reader;
    }

    void IInit.Init()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}