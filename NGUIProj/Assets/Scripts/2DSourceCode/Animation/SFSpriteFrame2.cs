
//author LiZongFu
//date 2016.5.11

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFSpriteFrame2 : CSBaseFrame2
{
    public override void SetAtlasPostProc(ISFAvater IAvater)
    {
        base.SetAtlasPostProc(IAvater);
        //FrameAni.setFirstName();
        //FrameAni.Play();
        if (mCurrentNames.Count > 1)
        {
            if (StopFrame == EActionStopFrameType.LastFrame)
            {
                curFrame = mCurrentNames.Count - 1;
                mSprite.SpriteName = mCurrentNames[curFrame];
            }
        }
    }

    public override void Play(bool isReset = true)
    {
        if (mCurrentNameCount == 0 || mCurrentNameCount == 1)
            mIsPlaying = false;
        else
            mIsPlaying = true;
        if (isReset)
        {
            curFrame = 0;
            OnChangeOneFrame(curFrame);
        }
    }
    public override void UpdateFrame()
    {
        if (mIsPlaying && mCurrentNameCount > 1 && FPS > 0)
        {
            //time += Time.deltaTime;
            if (StopFrame == EActionStopFrameType.LastFrame)
            {
                curFrame = mCurrentNameCount - 1;
            }
            mDelta += SFRealTime.deltaTime;

            if (FPS <= 0) FPS = 0;

            if (mDelta > 1) mDelta = 0;

            float rate = 1f / FPS;

            if (rate < mDelta)
            {
                curFrame++;

                mDelta = (rate > 0f) ? mDelta - rate : 0f;

                if (curFrame >= mCurrentNameCount)
                {
                    if (StopFrame == EActionStopFrameType.First)
                    {
                        curFrame = 0;
                    }
                    else if (curFrame >= mCurrentNameCount)
                    {
                        curFrame = mCurrentNameCount - 1;
                    }
                    else if (curFrame < 0)
                    {
                        curFrame = 0;
                    }

                    mIsPlaying = Loop;
                    
                    if (!Loop)
                    {
                        if (onFinish != null)
                        {
                            onFinish();
                        }

                        OnNoLoopPlayFinish();

                        if(curFrame<mCurrentNames.Count)mSprite.SpriteName = mCurrentNames[curFrame];
                    }
                }
                OnChangeOneFrame(curFrame);
                if (mIsPlaying)
                {
                    if (curFrame < mCurrentNames.Count) mSprite.SpriteName = mCurrentNames[curFrame];
                }
            }
        }
    }

    public override void OnNoLoopPlayFinish()
    {
        if (avater == null) return;
        avater.IOnOldCellChange();
    }

    public override void OnChangeOneFrame(int frame)
    {
        if (avater == null) return;
        avater.OnChangeOneFrame(frame);
    }
}
