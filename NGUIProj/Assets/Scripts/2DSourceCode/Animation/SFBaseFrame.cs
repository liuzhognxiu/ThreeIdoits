
//author LiZongFu
//date 2016.5.11

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface IBaseFrame
{
    void Play(bool isReset = true);
    void Initialization();
    bool isPlaying { get; }
    void UpdateFrame();
    float EstimateTakeTime { get; }
    bool isChange { get; set; }
    int FPS { get; set; }
    bool Loop { get; set; }
    void setLoop(bool bl);
    bool getLoop();
    ISFAvater avater { get; set; }
    void Clear();
    void RefreshNames();
    void SetAtlasPostProc(ISFAvater avatar);
    void Stop();
    SFAtlas atlas { get; }
    int curFrame { get; set; }
    bool IsPlayingOnLastOneFrame();
    void OnNoLoopPlayFinish();
    void OnChangeOneFrame(int frame);
}

public abstract class BaseFrame : MonoBehaviour
{
    public delegate void OnStart();
    public OnStart onStart;
    public delegate void OnFinish();
    public OnFinish onFinish;
    private EActionStopFrameType mStopFrame = EActionStopFrameType.First;
    public EActionStopFrameType StopFrame
    {
        get { return mStopFrame; }
        set { mStopFrame = value; }
    }
}

public class SFBaseFrame : BaseFrame, IBaseFrame
{
    public virtual void SetAtlasPostProc(ISFAvater avatar)
    {

    }
    private int mFPS = 10;
    public int FPS
    {
        get { return mFPS; }
        set { mFPS = value; }
    }
    private bool mLoop = false;
    public bool Loop
    {
        get { return mLoop; }
        set { mLoop = value; }
    }
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

    protected bool mActive = false;
    protected int mIndex = 0;
    protected float mDelta = 0f;
    
    protected CSSpriteBase mSprite;

    protected CSBetterList<string> mCurrentNames = new CSBetterList<string>();

    public int mCurrentNameCount = 8;//默认10个贴图

    protected MeshRenderer mMeshRenderer;

    public int curFrame
    {
        get 
        {
            return mIndex; 
        }
        set
        {
            mIndex = value;
        }
    }

    public bool IsPlayingOnLastOneFrame()
    {
        return mIndex == mCurrentNames.Count - 1;
    }

    public int frames
    {
        get { return mCurrentNames.Count; }
    }

    public float EstimateTakeTime
    {
        get
        {
            float t = ((1000f / (float)FPS) * (float)mCurrentNames.Count) * 0.001f;
            return t;
        }
    }

    public bool isPlaying
    {
        get { return mActive; }
    }

    public virtual void Start()
    {
        mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public virtual void Stop()
    {

    }

    public virtual void OnNoLoopPlayFinish()
    {

    }

    public void Initialization()
    {
        mSprite = gameObject.GetComponent<CSSpriteBase>();
    }

    public void Play(bool isReset = true)
    {
#if ClientDebug
        if (avater != null && avater.AvatarType == EAvatarType.MainPlayer&&mSprite!=null&&mSprite.getAtlas != null&&mSprite.getAtlas.name.Contains("Attack"))
        {
            if (Debug.developerConsoleVisible) Debug.Log("CSBaseFrame = " + mSprite.getAtlas.name);
        }
#endif
        mActive = true;
        curFrame = 0;
    }

    public void Pause()
    {
        mActive = false;
    }

    public bool Active(int montion)
    {
        string montionName = SFMisc.stringMotionDic.ContainsKey(montion) ? SFMisc.stringMotionDic[montion] : string.Empty;

        if (mActive == false) return false;

        if(curFrame >=0 && curFrame < mCurrentNames.Count)
        {
            string str = mCurrentNames[curFrame];

            return str.Contains(montionName);
        }
        
        return false;
    }

    public void setLoop(bool bl)
    {
        Loop = bl;
        mActive = true;
    }

    public bool getLoop()
    {
        return Loop;
    }

    public void setMeshRenderer(bool bl)
    {
        if (mMeshRenderer == null)
        {
            mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

        if (mMeshRenderer != null)
        {
            mMeshRenderer.enabled = bl;
        }
    }

    public void setFirstName()
    {
        if (mCurrentNames.Count > 0)
        {
            mSprite.SpriteName = mCurrentNames[0];

            //UISpriteData spriteData = mSprite.mAtlas.GetSprite(mSprite.SpriteName);
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

    public void Clear()
    {
        mCurrentNames.Clear();
    }

    public void RefreshNames()
    {
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

    int CompareSort(string f, string s)
    {
        return string.Compare(f, s);
    }
    /// <summary>
    /// 重置
    /// </summary>
    public void ResetToBeginning()
    {
        if (mSprite != null && mCurrentNames.Count > 0)
        {
            curFrame = 0;
        }
    }

    public void OnDestroy()
    {
        mSprite = null;
        mCurrentNames.Clear();
    }

    public virtual void UpdateFrame()
    {
    }

    public virtual void OnChangeOneFrame(int frame) { }
}
