
//author LiZongFu
//date 2016.5.11

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFSpriteFrame : CSBaseFrame
{
    public override void SetAtlasPostProc(ISFAvater IAvater)
    {
        base.SetAtlasPostProc(IAvater);
        setFirstName();
        IAvater.getIModel.SetFPS();
        Play();
        if (mCurrentNames.Count > 1)
        {
            if (StopFrame == EActionStopFrameType.LastFrame)
            {
                mIndex = mCurrentNames.Count - 1;
                mSprite.SpriteName = mCurrentNames[mIndex];
            }
        }
    }

    public override void UpdateFrame()
    {
        if (mActive && mCurrentNames.Count > 1 && FPS > 0)
        {
            //time += Time.deltaTime;
            if (StopFrame == EActionStopFrameType.LastFrame)
            {
                mIndex = mCurrentNames.Count - 1;
            }
            mDelta += SFRealTime.deltaTime;

            if (FPS <= 0) FPS = 0;

            if (mDelta > 1) mDelta = 0;

            float rate = 1f / FPS;
            
            if (rate < mDelta)
            {
                mIndex++;

                mDelta = (rate > 0f) ? mDelta - rate : 0f;

                if (mIndex >= mCurrentNames.Count)
                {
                    if (mSprite == null || mSprite.SpriteName == null)
                    {
                        return;
                    }
                    if (StopFrame == EActionStopFrameType.First) // 死亡以外的东西需要到第一帧
                    {
                        mIndex = 0;
                    }
                    else if(mIndex >= mCurrentNames.Count)
                    {
                        mIndex = mCurrentNames.Count - 1;
                    }
                    else if (mIndex < 0)
                    {
                        mIndex = 0;
                    }

                    mActive = Loop;
                    
                    if (!Loop)
                    {
                        if (onFinish != null)
                        {
                            onFinish();
                        }

                        OnNoLoopPlayFinish();

                        mSprite.SpriteName = mCurrentNames[mIndex];
                    }
                }

                if (mActive)
                {
                    mSprite.SpriteName = mCurrentNames[mIndex];
                }
            }
        }
    }

    public override void OnNoLoopPlayFinish()
    {
        if (avater == null) return;
        //if (mSprite.Atlas.name.Contains("Dead"))
        //{
        //    string s = "";
        //}
        avater.IOnOldCellChange();
    }
}
