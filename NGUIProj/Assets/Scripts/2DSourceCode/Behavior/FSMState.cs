
// author LiZongFu
// date 2016.4

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMState
{
    public ISFAvater avater;

    bool mIsWaitToState = false;

    float mBeginWaitTime = 0;

    float mWaitTime = 0;

    int mWaitState = -1;

    public int CurrentBehavior
    {
        get
        {
            return mCurrentBehaivor != null ? mCurrentBehaivor.Behavior : -1;
        }
    }

    public BehaviorState OverrideBehavior
    {
        get
        {
            return mOverrideBehavior;
        }
    }

    public void InitialAddBehavior(BehaviorState dpb)
    {
        if (mBehaviors.Contains(dpb))
        {
            return;
        }

        mBehaviors.Add(dpb);
    }

    public void Start(int defaultType)
    {
        mDefaultBehavior = GetBehavior(defaultType);
    }

    public BehaviorState GetBehavior(int type)
    {
        for (int i=0;i<mBehaviors.Count;i++) 
        {
            BehaviorState dbp = mBehaviors[i];
            if (dbp.Behavior == type)
            {
                return dbp;
            }
        }

        return null;
    }

    public void Switch2(int bt, bool voidRepeat)
    {
        //if (avater != null && (avater.getAvatarType() == EAvatarType.Player || avater.getAvatarType() == EAvatarType.MainPlayer))
        //{
        //    Debug.LogError("Switch2 = " + (CSMotion)bt + " voidRepeat = " + voidRepeat + " " + avater.getAvatarType());
        //}
        BehaviorState bh = GetBehavior(bt);
        //if (avater != null && (avater.getAvatarType() == EAvatarType.Player || avater.getAvatarType() == EAvatarType.MainPlayer))
        //{
        //    Debug.LogError("Switch2 = " + (CSMotion)bt + " voidRepeat = " + voidRepeat + " " + avater.getAvatarType());
        //}
        if (bh == null) 
        {
            //if (avater != null && (avater.getAvatarType() == EAvatarType.Player || avater.getAvatarType() == EAvatarType.MainPlayer))
            //{
            //    Debug.LogError("return" + " " + avater.getAvatarType());
            //}
            return;
        }
        ResetWait();
        if (bh == mCurrentBehaivor)
        {
            //if (avater != null && (avater.getAvatarType() == EAvatarType.Player || avater.getAvatarType() == EAvatarType.MainPlayer))
            //{
            //    Debug.LogError("same = " + (CSMotion)bh.Behavior + " " + avater.getAvatarType());
            //}
            if (!voidRepeat)
            {
                mCurrentBehaivor.End(mActor, mCurrentBehaivor);
                mCurrentBehaivor.Start(mActor, mCurrentBehaivor);
            }

            return;
        }

        if (mCurrentBehaivor != null)
        {
            //if (avater != null && (avater.getAvatarType() == EAvatarType.Player || avater.getAvatarType() == EAvatarType.MainPlayer))
            //{
            //    Debug.LogError("Switch2 = " + (CSMotion)bt + " voidRepeat = " + voidRepeat + " " + avater.getAvatarType());
            //}
            mCurrentBehaivor.End(mActor, bh);
        }
//#if ClientDebug
//        if (avater != null && (avater.getAvatarType() == EAvatarType.Player || avater.getAvatarType() == EAvatarType.MainPlayer))
//        {
//            if(Debug.developerConsoleVisible)Debug.LogError("Switch2 = " + (CSMotion)bt + " voidRepeat = " + voidRepeat + " " + avater.getAvatarType());
//        }
//#endif
        BehaviorState lastBehv = mCurrentBehaivor;
        mCurrentBehaivor = bh;
        mCurrentBehaivor.Start(mActor, lastBehv);
    }

    public void SetWait(int ret, float waitTime,bool isSetOnce = false)
    {
        if (!mIsWaitToState || !isSetOnce)
        {
            mIsWaitToState = true;
            mWaitTime = waitTime;
            mBeginWaitTime = Time.time;
            mWaitState = ret;
        }
    }

    public void ResetWait()
    {
        mIsWaitToState = false;
    }

    public void UpdateBehaviors()
    {
        if (mCurrentBehaivor == null)
        {
            if (mDefaultBehavior != null)
            {
                mCurrentBehaivor = mDefaultBehavior;
                mCurrentBehaivor.Start(mActor, null);
            }

            return;
        }
        
        int ret = mCurrentBehaivor.Update(mActor);
        ret = GetWaitState(ret);
        if (ret != mCurrentBehaivor.Behavior)
        {
//            #if ClientDebug
//            if (avater != null && (avater.getAvatarType() == EAvatarType.MainPlayer || avater.getAvatarType() == EAvatarType.Player))
//            {
//                if(Debug.developerConsoleVisible)Debug.LogError("Update Pre= " + (CSMotion)mCurrentBehaivor.Behavior + " " + avater.getAvatarType());
//                if(Debug.developerConsoleVisible)Debug.LogError("Update Cur= " + (CSMotion)ret + " " + avater.getAvatarType());
//            }
//#endif
            if (mCurrentBehaivor == mOverrideBehavior)
            {
                // pop override behavior.
                mOverrideBehavior = null;
            }

            if (ret == -1
                && mDefaultBehavior != null)
            {
                ret = mDefaultBehavior.Behavior;
            }

            BehaviorState nextBh = GetBehavior(ret);
            if (nextBh == null)
            {
                nextBh = mDefaultBehavior;
            }

            if (nextBh != null)
            {
                Switch2(nextBh.Behavior, true);
            }
            else
            {
                mCurrentBehaivor = null;
            }
        }
    }

    int GetWaitState(int ret)
    {
        if (!mIsWaitToState||Time.time - mBeginWaitTime<mWaitTime) return ret;
        ResetWait();
        return mWaitState;
    }

    public void Reset()
    {
        ResetWait();
        mBehaviors.Clear();
    }

    public void Release()
    {
        mBehaviors.Release();
    }

    //public ActorAnimationTrigger AnimTrigger
    //{
    //    get { return mAnimTrigger; }
    //}

    public FSMState(ISFAvater ac)
    {
        mActor = ac;
        //mAnimTrigger = new ActorAnimationTrigger(ac);
    }

    BehaviorState mOverrideBehavior = null;
    BehaviorState mCurrentBehaivor = null;
    BehaviorState mDefaultBehavior = null;
    CSBetterList<BehaviorState> mBehaviors = new CSBetterList<BehaviorState>();
    public CSBetterList<BehaviorState> Behaviors
    {
        get { return mBehaviors; }
        set { mBehaviors = value; }
    }
    ISFAvater mActor;
}
