
//-------------------------------------------------------------------------
//状态
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class    BehaviorState 
{
    public int Behavior
    {
        get { return mBehaviorType; }
    }

    public void Start(ISFAvater p, BehaviorState lastBehv)
    {
        if (mBehaviorStartDel != null)
        {
            mBehaviorStartDel(p, lastBehv);
        }
    }

    public int Update(ISFAvater p)
    {
        if (mBehaviorUpdateDel != null)
        {
            return mBehaviorUpdateDel(p);
        }

        return Behavior;
    }

    public void End(ISFAvater p, BehaviorState nextBehv)
    {
        if (mBehaviorEndDel != null)
        {
            mBehaviorEndDel(p, nextBehv);
        }
    }

    public delegate int UpdateDel(ISFAvater p);
    public delegate void StartDel(ISFAvater p, BehaviorState last);
    public delegate void EndDel(ISFAvater p, BehaviorState next);

    int mBehaviorType;
    StartDel mBehaviorStartDel = null;
    UpdateDel mBehaviorUpdateDel = null;
    EndDel mBehaviorEndDel = null;

    public BehaviorState(int behv, StartDel startDel,
        UpdateDel updateDel, EndDel endDel, string name)
    {
        mBehaviorType = behv;
        mBehaviorStartDel = startDel;
        mBehaviorUpdateDel = updateDel;
        mBehaviorEndDel = endDel;
        //mName = name;
    }
}