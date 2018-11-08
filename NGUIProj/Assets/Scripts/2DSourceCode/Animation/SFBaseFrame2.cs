
//author LiZongFu
//date 2016.5.11

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFBaseFrame2 : BaseFrame, IBaseFrame
{
//#if UNITY_EDITOR
//    public bool isDebug = false;
//#endif
    [SerializeField]
    private int mFPS = 10;
    public int FPS
    {
        get { return mFPS; }
        set { mFPS = value; }
    }
    [SerializeField]
    private bool mLoop = false;
    public bool Loop
    {
        get { return mLoop; }
        set { mLoop = value; }
    }
    [SerializeField]
    private bool mIsChange = true;
    public bool isChange
    {
        get { return mIsChange; }
        set { mIsChange = value; }
    }

    public ISFAvater mAvater;
    public ISFAvater avater
    {
        get { return mAvater; }
        set { mAvater = value; }
    }

    public SFAtlas atlas
    {
        get
        {
            if (mSprite != null) return mSprite.getAtlas;
            return null;
        }
    }

    protected bool mIsPlaying = false;
    [SerializeField]
    protected int mCurFrame = 0;
    protected float mDelta = 0f;
    
    protected CSSpriteBase mSprite;

    protected CSBetterList<string> mCurrentNames = new CSBetterList<string>();

    public int mCurrentNameCount = 8;//默认10个贴图

    protected MeshRenderer mMeshRenderer;

    public virtual int curFrame
    {
        get {
            return mCurFrame; }
        set
        {
            mCurFrame = value;
        }
    }

    public bool IsPlayingOnLastOneFrame()
    {
        return mCurFrame == mCurrentNameCount - 1;
    }

    public virtual int CurrentNameCount
    {
        get { return mCurrentNameCount; }
        set { mCurrentNameCount = value; }
    }

    public virtual float EstimateTakeTime
    {
        get
        {
            float t = ((1000f / (float)FPS) * (float)mCurrentNameCount) *0.001f;
            return t;
        }
    }
    public virtual void OnNoLoopPlayFinish()
    {

    }

    public virtual bool isPlaying
    {
        get { return mIsPlaying; }
    }

    public virtual void Start()
    {
        if (this == null) return;
        mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public virtual void Stop()
    {

    }

    public virtual void Initialization()
    {
        if (this == null) return;
        mSprite = gameObject.GetComponent<CSSpriteBase>();
    }

    public virtual void Play(bool isReset = true)
    {
#if ClientDebug
        if (avater != null && avater.AvatarType == EAvatarType.MainPlayer&&mSprite!=null&&mSprite.getAtlas != null&&mSprite.getAtlas.name.Contains("Attack"))
        {
            if(Debug.developerConsoleVisible)Debug.Log("CSBaseFrame = " + mSprite.getAtlas.name);
        }
#endif
        if (mCurrentNameCount == 0 || mCurrentNameCount == 1)
            mIsPlaying = false;
        else
            mIsPlaying = true;
        if (isReset)
        {
            curFrame = 0;
            if (mSprite != null && curFrame < mCurrentNames.Count)
            {
                mSprite.SpriteName = mCurrentNames[curFrame];
            }
        }
    }

    public virtual void SetAtlasPostProc(ISFAvater avater)
    {
        if (mSprite != null && curFrame < mCurrentNames.Count)
        {
            string s =  mCurrentNames[curFrame];
            mSprite.SpriteName =s;
            //Debug.Log(s);
        }
//        #if UNITY_EDITOR
//        if (!isDebug) return;
//        if (mSprite.SpriteName != null && mSprite.Atlas != null)
//        {
//            bool isFind = false;
//            for (int i = 0; i < mSprite.Atlas.spriteList.Count; i++)
//            {
//                UISpriteData data = mSprite.Atlas.spriteList[i];
//                if (data.name == mSprite.SpriteName)
//                {
//                    isFind = true;
//                    break;
//                }
//            }
//            if (!isFind)
//            {
//                Debug.LogError("Not Find = " + mSprite.SpriteName + " " + mSprite.Atlas.name);
//            }
//        }
//        else if (mSprite.SpriteName != null)
//        {
//            Debug.LogError("Not Find = " + mSprite.SpriteName);
//        }
//        else if(mSprite.Atlas != null)
//        {
//            Debug.LogError("Not Find = " +  mSprite.Atlas.name);
//        }
//        else
//        {
//            Debug.LogError("Not Find = ");
//        }
//#endif
    }

    public virtual void Pause()
    {
        mIsPlaying = false;
    }

    public virtual void setLoop(bool bl)
    {
        Loop = bl;
        mIsPlaying = true;
    }

    public virtual bool getLoop()
    {
        return Loop;
    }

    public virtual void setMeshRenderer(bool bl)
    {
        if (this == null) return;
        if (mMeshRenderer == null)
        {
            mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

        if (mMeshRenderer != null)
        {
            mMeshRenderer.enabled = bl;
        }
    }

    public void RefreshNames(CSBetterList<string> list, bool isReset = false)
    {
        if (list != null && list.Count > 0)
        {
            if (isReset)
                ResetToBeginning();
            mCurrentNames = list;
        }
    }

    public virtual void Clear()
    {
        mCurrentNames.Clear();
    }

    public void ClearAtlas()
    {
        if (mSprite != null)
            mSprite.getAtlas = null;
    }

    public virtual void RefreshNames()
    {
        if (this == null) return;
        if (mSprite == null)
            mSprite = gameObject.GetComponent<CSSpriteBase>();

        if (mSprite != null && mSprite.getAtlas != null)
        {
            RebuildSpriteList();
        }
    }

    public virtual void RebuildSpriteList()
    {
    }
    /// <summary>
    /// 重置
    /// </summary>
    public virtual void ResetToBeginning()
    {
        curFrame = 0;
    }

    public virtual void OnDestroy()
    {
        mSprite = null;
        mCurrentNames.Clear();
    }

    public virtual void UpdateFrame()
    {
    }
    public virtual void OnChangeOneFrame(int frame) { }
}
