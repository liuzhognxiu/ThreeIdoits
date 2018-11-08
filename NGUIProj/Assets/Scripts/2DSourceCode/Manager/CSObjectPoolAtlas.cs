using UnityEngine;
using System.Collections;

public class CSObjectPoolAtlas : CSObjectPoolBase
{
    public override CSObjectPoolItem GetGOFromPool()
    {
        CSObjectPoolItem item = null;
        if (mList.size > 0)
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
        if (refCount == 0&&item.go!=null)
        {
            item.go.SetActive(true);
        }
        if (!mList.Contains(item))
        {
            refCount++;
            item.isUse = true;
            mList.Remove(item);
            mList.Add(item);//isUse = true:插入到最后
        }
        else//Atlas如果存在，外部调用AddPoolItem只是添加一个引用计数
        {
            refCount++;
            item.isUse = true;
        }
    }

    public override void RemovePoolItem(CSObjectPoolItem item, bool isDestroyResImmi = false)
    {
        if (mList.Contains(item))
        {
            refCount--;
            refCount = Mathf.Max(refCount, 0);
            item.isUse = false;
            mList.Remove(item);
            mList.Insert(0, item);//isUse false:未使用的放到最前面
            if (refCount == 0)
            {
                if (item.go != null)
                    item.go.SetActive(false);
                if (!isDestroyResImmi)
                {
                    mLastNotUseTime = Time.time;
                }
                else
                {
                    mLastNotUseTime = 0;
                }
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
            bool isDestroy = mMgr.DestroyPool(poolName);
            if (!isDestroy)
            {
                mLastNotUseTime = mLastNotUseTime + 10 > Time.time ? Time.time : mLastNotUseTime + 10;
                mList.Add(item);
            }
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
