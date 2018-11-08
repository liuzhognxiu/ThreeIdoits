using UnityEngine;
using System.Collections.Generic;
using System;

//UITips
public class UITipsView : MonoBehaviour
{
    private UILabel mTipsLb;
    public UILabel TipsLb { get { return mTipsLb ?? (mTipsLb = this.transform.Find("GameObject/TipsLb").GetComponent<UILabel>()); } }

    private UILabel mTipsGame;
    public UILabel TipsGame { get { return mTipsGame ?? (mTipsGame = transform.Find("GameObject/TipsGame").GetComponent<UILabel>()); } }

    private GameObject mTipsHonor;
    public GameObject TipsHonor { get { return mTipsHonor ?? (mTipsHonor = transform.Find("GameObject/honor").gameObject); } }

    private UISprite mSprite;
    public UISprite Spirte { get { return mSprite ?? (mSprite = this.transform.Find("GameObject/Sprite").GetComponent<UISprite>()); } }

    private GameObject mChildGo;
    public GameObject ChildGo
    {
        get { return mChildGo ?? (mChildGo = transform.Find("GameObject").gameObject); }
    }

    private Transform mChildGoTrans;
    public Transform ChildGoTrans
    {
        get
        {
            if (mChildGoTrans == null) mChildGoTrans = ChildGo.transform;
            return mChildGoTrans;
        }
    }

    private UIPanel mPanel;
    public UIPanel Panel
    {
        get
        {
            if (mPanel == null) mPanel = GetComponent<UIPanel>();
            return mPanel;
        }
    }

    private Transform mCacheTrans;
    public UnityEngine.Transform CacheTrans
    {
        get
        {
            if (mCacheTrans == null) mCacheTrans = transform;
            return mCacheTrans;
        }
    }

    private TweenAlpha mTweenAlpha;
    public TweenAlpha TA
    {
        get
        {
            if (mTweenAlpha == null) mTweenAlpha = GetComponent<TweenAlpha>();
            return mTweenAlpha;
        }
    }

    private TweenPosition mTweenPosition;
    public TweenPosition TP
    {
        get
        {
            if (mTweenPosition == null)
            {
                mTweenPosition = GetComponent<TweenPosition>();
                if (mTweenPosition == null) mTweenPosition = gameObject.AddComponent<TweenPosition>();
            }
            return mTweenPosition;
        }
    }

    private TweenScale mTweenScale;
    public TweenScale TS
    {
        get
        {
            if (mTweenScale == null) mTweenScale = GetComponent<TweenScale>();
            return mTweenScale;
        }
    }

    private TweenPosition mChildTP;
    public TweenPosition ChildTP
    {
        get
        {
            if (mChildTP == null)
            {
                mChildTP = ChildGo.GetComponent<TweenPosition>();
                if (mChildTP == null) mChildTP = ChildGo.AddComponent<TweenPosition>();
            }
            return mChildTP;
        }
    }

    private TweenScale mChildTS;
    public TweenScale ChildTS
    {
        get
        {
            if (mChildTS == null)
            {
                mChildTS = ChildGo.GetComponent<TweenScale>();
                if (mChildTS == null) mChildTS = ChildGo.AddComponent<TweenScale>();
            }
            return mChildTS;
        }
    }

    public enum TipPosType { Center, LeftDown, Honor, Down, Right }

    public TipPosType mTipPos = TipPosType.LeftDown;
    public TipPosType TipPos
    {
        get
        {
            return mTipPos;
        }
    }

    Schedule mSchedule;
    public int DefaultDepth = 150;

    public void InitDepth(int depth)
    {
        DefaultDepth = depth;
    }

    public void ShowTips(string content, float timer, Color color, bool flag = false, TipPosType flagType = TipPosType.Center)
    {
        mTipPos = flagType;
        switch (flagType)
        {
            case TipPosType.Center:
                break;
            case TipPosType.LeftDown:
                if (Spirte.gameObject.activeSelf)
                    Spirte.gameObject.SetActive(false);
                CacheTrans.localPosition = new Vector3(-560, -85, 0);
                break;
            case TipPosType.Honor:
                break;
            case TipPosType.Down:
                CacheTrans.localPosition = Vector3.zero;
                if (!Spirte.gameObject.activeSelf)
                    Spirte.gameObject.SetActive(true);
                Spirte.width = 540;
                break;
            case TipPosType.Right:
                if (Spirte.gameObject.activeSelf)
                    Spirte.gameObject.SetActive(false);
                CacheTrans.localPosition = new Vector3(400, -85, 0);
                break;
            default:
                break;
        }



        ChildGoTrans.localPosition = new Vector3(-1, -205, 0);
        ChildGoTrans.localScale = Vector3.one;
        CacheTrans.localScale = Vector3.one;

        int count = !flag ? Utility.TipsList.Count : Utility.GameTipsList.Count;
        int moveIndex = 0;
        for (int i = 0; i < count; i++)
        {
            int index = i + 1;
            UITipsView view = !flag ? Utility.TipsList[count - index] : Utility.GameTipsList[count - index];
            if (view.TipPos == TipPosType.LeftDown || view.TipPos == TipPosType.Down)
            {
                moveIndex++;
                view.MoveUp(moveIndex, flag);
            }
            else if (view.TipPos == TipPosType.Right)
            {
                moveIndex++;
                view.RightMoveUp(moveIndex, flag);
            }
        }
        if (!flag)
        {
            Utility.TipsList.Add(this);
            TipsLb.text = content;
            TipsLb.color = color;
        }
        else
        {
            Utility.GameTipsList.Add(this);
            TipsGame.text = content;
            TipsGame.color = color;
        }

        mSchedule = Timer.Instance.SetSchedule(timer, (s) =>
        {
            TA.SetOnFinished(() =>
            {
                TP.enabled = false;
                UIPool.Instance.PushUITipPanel(this);
                if (!flag)
                    Utility.TipsList.Remove(this);
                else
                    Utility.GameTipsList.Remove(this);
            });
            TweenAlpha.Begin(gameObject, 1, 0);
        });
        setViewLayer();
    }

    private void RightMoveUp(int index, bool flag)
    {
        TweenPosition.Begin(gameObject, 0.2f, new Vector3(!flag ? 0 : 400, (!flag ? (31 * index) : (-85 + 25 * index)), 0));  
    }
    //显示tips
    public void ShowTips(string content, float timer, Color color, bool flag = false)
    {
        if (!flag)
        {
            mTipPos = TipPosType.Down;
            CacheTrans.localPosition = Vector3.zero;
            if (!Spirte.gameObject.activeSelf)
                Spirte.gameObject.SetActive(true);
            Spirte.width = 540;
        }
        else
        {
            mTipPos = TipPosType.LeftDown;
            if (Spirte.gameObject.activeSelf)
                Spirte.gameObject.SetActive(false);
            CacheTrans.localPosition = new Vector3(-560, -85, 0);
        }

        ChildGoTrans.localPosition = new Vector3(-1, -205, 0);
        ChildGoTrans.localScale = Vector3.one;
        CacheTrans.localScale = Vector3.one;

        int count = !flag ? Utility.TipsList.Count : Utility.GameTipsList.Count;
        int moveIndex = 0;
        for (int i = 0; i < count; i++)
        {
            int index = i + 1;
            UITipsView view = !flag ? Utility.TipsList[count - index] : Utility.GameTipsList[count - index];
            if (view.TipPos == TipPosType.LeftDown || view.TipPos == TipPosType.Down)
            {
                moveIndex++;
                view.MoveUp(moveIndex, flag);
            }
        }
        if (!flag)
        {
            Utility.TipsList.Add(this);
            TipsLb.text = content;
            TipsLb.color = color;
        }
        else
        {
            Utility.GameTipsList.Add(this);
            TipsGame.text = content;
            TipsGame.color = color;
        }

        mSchedule = Timer.Instance.SetSchedule(timer, (s) =>
        {
            TA.SetOnFinished(() =>
            {
                TP.enabled = false;
                UIPool.Instance.PushUITipPanel(this);
                if (!flag)
                    Utility.TipsList.Remove(this);
                else
                    Utility.GameTipsList.Remove(this);
            });
            TweenAlpha.Begin(gameObject, 1, 0);
        });
        setViewLayer();
    }



    public void ImmediatelyBeginAlpha(bool flag = false)
    {
        if (mSchedule != null)
            Timer.Instance.RemoveSchedule(mSchedule);
        TA.SetOnFinished(() =>
        {
            TP.enabled = false;
            UIPool.Instance.PushUITipPanel(this);
            if (!flag)
                Utility.TipsList.Remove(this);
            else
                Utility.GameTipsList.Remove(this);
        });
        TA.ResetToBeginning();
        TA.duration = 0;
        TA.PlayForward();
    }

    public void SizeTipSize(int fontsize)
    {
        TipsLb.fontSize = fontsize;
    }

    public void SizeTipSize(int fontsize, int spriteHeight)
    {
        TipsLb.fontSize = fontsize;
        Spirte.height = spriteHeight;
    }

    public void MoveUp(int index, bool flag)
    {
        TweenPosition.Begin(gameObject, 0.2f, new Vector3(!flag ? 0 : -560, (!flag ? (31 * index) : (-85 + 25 * index)), 0)); 
    }


    /// <summary>显示在中间的提示框 </summary>
    public void ShowCenterTips(string content, float timer, Color color)
    {
        mTipPos = TipPosType.Center;
        CacheTrans.localPosition = new Vector3(0, 200, 0);
        ChildGoTrans.localPosition = new Vector3(-1, -205, 0);
        ChildGoTrans.localScale = Vector3.one;
        if (!Spirte.gameObject.activeSelf)
            Spirte.gameObject.SetActive(true);
        TipsLb.text = content;
        TipsLb.color = color;

        TA.duration = timer;

        Timer.Instance.SetSchedule(timer, (s) =>
        {
            TweenAlpha.Begin(gameObject, 1, 0);
            TA.SetOnFinished(() =>
            {
                TP.enabled = false;
                UIPool.Instance.PushUITipPanel(this);
                // DestroyImmediate(gameObject);
            });
            //if (TA != null)
            //    TA.PlayForward();
        });
        setViewLayer();
    }

    /// <summary>显示在中间向上飘的提示框 </summary>
    public void ShowCenterMoveUpTips(string content, float timer, Color color)
    {
        mTipPos = TipPosType.Center;
        Utility.TipsList.Add(this);

        ChildGoTrans.localPosition = new Vector3(0, 80, 0);
        if (!Spirte.gameObject.activeSelf)
            Spirte.gameObject.SetActive(true);
        TipsLb.text = content;
        TipsLb.color = color;

        Spirte.width = 230;

        TweenAlpha ta = this.GetComponent<TweenAlpha>();
        ta.duration = 1f;
        ChildGoTrans.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Vector3 childPos = ChildGoTrans.localPosition;
        TweenScale.Begin(ChildGo, 0.1f, new Vector3(1, 1, 1));
        TweenPosition.Begin(ChildGo, 0.1f, new Vector3(0, childPos.y + 20f, 0f));

        Timer.Instance.SetSchedule(1f, (s) =>
        {
            TweenPosition.Begin(ChildGo, 1f, new Vector3(0, 120, 0));
            TweenAlpha.Begin(gameObject, 1, 0);
            TA.SetOnFinished(() =>
            {
                ChildTP.enabled = false;
                ChildTS.enabled = false;
                TP.enabled = false;
                Utility.TipsList.Remove(this);
                UIPool.Instance.PushUITipPanel(this);
            });
        });
        setViewLayer();
    }

    public void ShowCenterMoveUpTips(string content, List<UITipsView> list, float delay, float duration, Color color)
    {
        mTipPos = TipPosType.Center;
        //避免由于上次使用时有重叠偏移动画，所以要重置位置
        ChildTP.enabled = false;
        ChildGoTrans.localPosition = new Vector3(-1f, -190f, 0f);
        //由于有延迟所以必须先手动重置
        CacheTrans.localPosition = new Vector3(0, 290, 0);
        Panel.alpha = 1f;

        if (!Spirte.gameObject.activeSelf)
            Spirte.gameObject.SetActive(true);
        Spirte.width = 230;
        TipsLb.text = content;
        TipsLb.color = color;

        Utility.PlayTweenScale(ChildTS, new Vector3(0.5f, 0.5f, 0.5f), Vector3.one, 0.1f);
        Utility.PlayTweenPosition(TP, CacheTrans.localPosition, new Vector3(0, 330, 0), duration, delay);
        Utility.PlayTweenAlpha(TA, 1f, 0f, duration, delay);

        if (list.Count > 0)
        {
            Transform trans = null;
            UITipsView tipsView = null;
            int index;
            Vector3 offset;
            for (int i = 0; i < list.Count; i++)
            {
                tipsView = list[i];
                trans = list[i].transform;
                index = list.Count - i;
                offset = Vector3.up * index * 30;
                if ((tipsView.CacheTrans.localPosition - CacheTrans.localPosition).y + (tipsView.ChildGoTrans.localPosition - ChildGoTrans.localPosition).y < offset.y)
                {
                    Utility.PlayTweenPosition(tipsView.ChildTP, tipsView.ChildGoTrans.localPosition, ChildGoTrans.localPosition + offset, 0.2f);
                }
            }
        }

        if (list.Contains(this))
            list.Remove(this);
        list.Add(this);

        TA.SetOnFinished(() =>
        {
            TP.enabled = false;
            TP.delay = 0f;
            TA.delay = 0f;
            if (list != null)
            {
                list.Remove(this);
            }
            UIPool.Instance.PushUITipPanel(this);
        });
        setViewLayer();
    }

    public void ShowHonorTip(int _honor)
    {
        mTipPos = TipPosType.Honor;
        TipsHonor.SetActive(true);
        TipsHonor.GetComponentInChildren<UILabel>().text = _honor.ToString();

        TweenPosition _tp = TipsHonor.GetComponent<TweenPosition>();
        if (_tp != null)
        {
            _tp.ResetToBeginning();
            _tp.PlayForward();
            _tp.SetOnFinished(() =>
            {
                this.gameObject.SetActive(false);
                Destroy(gameObject);
            });
        }
        setViewLayer();
    }

    void setViewLayer()
    {
        if (mTipPos == TipPosType.LeftDown)
            gameObject.GetComponent<UIPanel>().depth = 1;
        else
            gameObject.GetComponent<UIPanel>().depth = DefaultDepth;
    }
}
