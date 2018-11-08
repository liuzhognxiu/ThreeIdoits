using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
/// <summary>
/// 缓存池分为2中类型：
/// 缓存非Mono脚本：如果要缓存GameObject，使用脚本引用的方式缓存（例如CSAvater，CAAudio）
/// 缓存资源:例如Atlas
/// </summary>
public class CSObjectPoolMgr : CSGameMgrBase<CSObjectPoolMgr> 
{
    Dictionary<string, CSObjectPoolBase> mDic = new Dictionary<string, CSObjectPoolBase>();
    CSBetterList<CSObjectPoolBase> mList = new CSBetterList<CSObjectPoolBase>();
    /// <summary>
    /// 从缓存池里面获得GameObject，并且从缓存池里面去除,返回的Gameobject.active = false
    /// </summary>resPath 资源路径，也是缓存池名称
    /// <param name="poolName">资源路径</param>
    CSObjectPoolItem GetPoolItem(string resName, string resPath,EPoolType poolType, int poolNum = 0, bool isForever = false)
    {
        CSObjectPoolBase pool = null;
        string poolName = resPath;
        if (mDic.ContainsKey(poolName))
        {
            pool = mDic[poolName];
        }
        else
        {
            if (poolNum == 0) return null;
            CSStringBuilder.Clear();

            GameObject poolGO = new GameObject();
            Transform trans = poolGO.transform;
            trans.parent = transform;
            if (poolType == EPoolType.Normal)
            {
                pool = poolGO.AddComponent<CSObjectPoolNormal>();
                CSStringBuilder.Append("Normal Pool->", resName);
                poolGO.name = CSStringBuilder.ToString();
            }
            else if (poolType == EPoolType.Resource)
            {
                pool = poolGO.AddComponent<CSObjectPoolAtlas>();
                CSStringBuilder.Append("Atlas Pool->", resName);
                poolGO.name = CSStringBuilder.ToString();
            }
            pool.resName = resName;
            //pool.resType = resType;
            pool.Init(this);
            mDic.Add(poolName, pool);
            mList.Add(pool);
        }
        pool.poolNum = poolNum;
        pool.poolName = poolName;
        pool.isForever = isForever;
        return pool.GetGOFromPool();
    }

    public CSObjectPoolBase GetPool(string poolName)
    {
        if (mDic.ContainsKey(poolName)) return mDic[poolName];
        return null;
    }

    /// <summary>
    /// 例如：当一个缓存资源进入倒计时（这个时候该Atlas的HasBeenDestroy = false）,这个时候启动一个ModelLoadBase，由于ModelLoadBase是所有请求资源加载完了才进行回调，如果在回调之前，
    /// 该资源被Destroy了，那么Atlas的数据将丢失，所以要在ModelLoadBase调用的时候，将这个资源的倒计时停止，表示这个资源我在稍后会使用到它，
    /// 当然会出现这样一种情况：如果回调被覆盖，之前的资源就一直在内存里面，除非切换场景或者重新使用这个资源
    /// </summary>
    /// <param name="poolNameShow"></param>
    /// <param name="poolName"></param>
    public void MarkStopDeleteTimeCount(string poolNameShow, string poolName)
    {

    }

    public CSObjectPoolItem GetAndAddPoolItem_Resource(string poolNameShow, string poolName, GameObject go,bool isForever = false)
    {
        return GetAndAddPoolItem(poolNameShow, poolName, go, EPoolType.Resource, 1, isForever, null, null);
    }

    /// <summary>
    /// 代码永久缓存（除非切换场景）
    /// </summary>
    /// <param name="poolNameShow"></param>
    /// <param name="poolName"></param>
    /// <param name="go"></param>
    /// <param name="poolNum"></param>
    /// <param name="isForever"></param>
    /// <param name="type"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public CSObjectPoolItem GetAndAddPoolItem_Class(string poolNameShow, string poolName, GameObject go,Type type, params object[] args)
    {
        return GetAndAddPoolItem(poolNameShow, poolName, go, EPoolType.Normal, 1000, true, type, args);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="poolNameShow"></param>
    /// <param name="poolName"></param>
    /// <param name="go">例如CSAvater，如果不使用了，需要将go（CSAvater。GO）隐藏掉，不然场景上面会有残留</param>
    /// <param name="poolType"></param>
    /// <param name="poolNum"></param>
    /// <param name="isForever"></param>
    /// <returns></returns>
    CSObjectPoolItem GetAndAddPoolItem(string poolNameShow, string poolName, GameObject go,
        EPoolType poolType, int poolNum, bool isForever, Type type, params object[] args)
    {
        CSObjectPoolItem poolItem = GetPoolItem(poolNameShow, poolName, poolType, poolNum, isForever);
        CSObjectPoolBase pool = mDic[poolName];
        poolItem.go = go;
        if (poolItem.objParam == null&&type != null)
        {
            poolItem.objParam = Activator.CreateInstance(type,args);
        }
        pool.AddPoolItem(poolItem);
        return poolItem;
    }

    public void RemovePoolItem(CSObjectPoolItem item,bool isDestroyResImmi = false)
    {
        if (item == null) return;

        if (item.owner!=null)
        {
            CSObjectPoolBase pool = item.owner;

            if (pool != null)
            {
                pool.RemovePoolItem(item, isDestroyResImmi);
            }
        }
    }

    public bool DestroyPool(string poolName)
    {
        if (mDic.ContainsKey(poolName))
        {
            CSObjectPoolBase pool = mDic[poolName];
            bool isDestroy = false;
            if (pool != null)
            {
                isDestroy = SFOut.IResourceManager.DestroyResource(poolName, false);
                if (isDestroy)
                {
                    pool.CSOnDestroy();
                    mList.Remove(pool);
                }
            }
            if (isDestroy)
            {
                mDic.Remove(poolName);
                return true;
            }
            
        }
        return false;
    }

    public void Update()
    {
        for (int i = mList.Count - 1; i >= 0; i--)
        {
            CSObjectPoolBase b = mList[i];
            b.CSUpdate();
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Dictionary<string, CSObjectPoolBase>.Enumerator cur = mDic.GetEnumerator();
        while (cur.MoveNext())
        {
            cur.Current.Value.CSOnDestroy(false);
        }
        mList.Clear();
        mList = null;
        mDic.Clear();
        mDic = null;
    }
}
