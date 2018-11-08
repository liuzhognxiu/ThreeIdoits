using UnityEngine;
using System.Collections;

public class CSObjectPoolNormal : CSObjectPoolBase
{
    public override CSObjectPoolItem GetGOFromPool()
    {
        CSObjectPoolItem item = null;
        if (mList.size > 0&&!mList[0].isUse)//如果正在使用，新建Item，外部去克隆
        {
            item = mList[0];
        }
        else
        {
            item = new CSObjectPoolItem();
            item.owner = this;
        }
        return item;
    }

    public override void AddPoolItem(CSObjectPoolItem item)
    {
        refCount++;
        item.isUse = true;
        mList.Remove(item);
        mList.Add(item);//isUse = true:插入到最后
    }

    public override void RemovePoolItem(CSObjectPoolItem item, bool isDestroyResImmi = false)
    {
        if (mList.Contains(item))
        {
            refCount--;
            refCount = Mathf.Max(refCount, 0);
            item.isUse = false;
            if(item.go!=null)item.go.SetActive(false);
            mList.Remove(item);
            mList.Insert(0, item);//isUse false:未使用的放到最前面
            if (refCount == 0)
            {
                mLastNotUseTime = Time.time;
            }
        }
    }

    protected override void DestroyPoolItem(CSObjectPoolItem item)
    {
        if (item == null) return;
        if (item.isUse) return;//正在使用的话，不进行删除
        if (item.go != null)
        {
            Destroy(item.go);
        }
        item.go = null;
        mList.Remove(item);

        if (mList.size == 0)
        {
            mMgr.DestroyPool(poolName);
        }
    }

    public override void CSUpdate()
    {
        base.CSUpdate();

        if (mList.size == 0) return;

        if (Time.time - mLastRealseTime > releaseInterval)
        {
            if (mList.size > poolNum)
            {
                mLastRealseTime = Time.time;
                DestroyPoolItem(mList[0]);
            }
            else if (!isForever)
            {
                if (refCount == 0)
                {
//#if UNITY_EDITOR
//                    leftReleaseTime = releaseTime - (Time.time - mLastNotUseTime);
//#endif
                    if (Time.time - mLastNotUseTime > releaseTime)
                    {
                        DestroyPoolItem(mList[0]);
                    }
                }
            }
        }
    }

    public override void CSOnDestroy()
    {
        base.CSOnDestroy();
    }

    public override void CSOnDestroy(bool isDestroyGameObject)
    {
        base.CSOnDestroy(isDestroyGameObject);
    }
}
