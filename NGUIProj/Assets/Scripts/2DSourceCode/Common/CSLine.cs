using UnityEngine;
using System.Collections;

public class CSLine {

    private LineRenderer mLine;
    public UnityEngine.LineRenderer Line
    {
        get { return mLine; }
        set { mLine = value; }
    }

    private bool mIsShow = false;
    long avatarID1;
    Vector3 delta1;
    long avatarID2;
    Vector3 delta2;
    int showDist;
    public void Init(long avatarID1, Vector3 delta1, long avatarID2, Vector3 delta2,int showDist = 6)
    {
        mIsShow = true;
        this.avatarID1 = avatarID1;
        this.avatarID2 = avatarID2;
        this.delta1 = delta1;
        this.delta2 = delta2;
        this.showDist = showDist;
        if (mLine == null)
        {
            CSResource res = SFOut.IResourceManager.AddQueue("Line", ResourceType.Effect, OnLoaded, ResourceAssistType.Terrain);
        }
        else
        {
            Show(true);
            UpdateLinePos();
        }
    }

    void OnLoaded(CSResource res)
    {
        if (!mIsShow) return;
        if (SFOut.IScene == null) return;
        if (mLine == null)
        {
            ISFAvater avatar = SFOut.IScene.getAvatarByISFAvatar(avatarID1);
            if (avatar == null) return;
            GameObject go = res.GetObjInst() as GameObject;
            if (go == null) return;
            SFMisc.SetLayer(go, 0);
            Transform trans = go.transform;
            trans.parent = avatar.CacheTransform;
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            mLine = go.GetComponent<LineRenderer>();
        }
       
        if (mLine == null)
        {
            Destroy();
            return;
        }
        UpdateLinePos();
    }

    public void Update()
    {
        UpdateLinePos();
    }

    public void UpdateLinePos()
    {
        if (!mIsShow) return;
        if (mLine == null || !SFOut.IGame.IsLanuchMainPlayer || SFOut.IScene.CahceWorldTrans == null) return;
        ISFAvater avatar1 = SFOut.IScene.getAvatarByISFAvatar(avatarID1);
        if (avatar1 == null||avatar1.isDead)
        {
            Show(false);
            mIsShow = false;
            return;
        }
        ISFAvater avatar2 = SFOut.IScene.getAvatarByISFAvatar(avatarID2);
        if (avatar2 == null || avatar2.isDead)
        {
            Show(false);
            mIsShow = false;
            return;
        }

        SFMisc.Dot2 dot = avatar1.getOldCellPos - avatar2.getOldCellPos;
        int dist = dot.Pow2();
        bool b = dist <= (showDist * showDist);
        if (mLine.enabled != b) mLine.enabled = b;
        if (!b)
        {
            Show(false);
            mIsShow = false;
            return;
        }
        float z = avatar1.CacheTransform.localPosition.z > avatar2.CacheTransform.localPosition.z ? avatar2.CacheTransform.localPosition.z : avatar1.CacheTransform.localPosition.z;
        Vector3 vec1 = avatar1.getRealPosition2() + delta1+SFOut.IScene.CahceWorldTrans.TransformPoint(new Vector3(0, 0, z-1000));
        Vector3 vec2 = avatar2.getRealPosition2() + delta2 + SFOut.IScene.CahceWorldTrans.TransformPoint(new Vector3(0, 0, z - 1000));
        mLine.SetPosition(0, vec1);
        mLine.SetPosition(1, vec2);
    }

    void Show(bool b)
    {
        if (mLine != null)
        {
            if (mLine.gameObject.activeSelf != b)
                mLine.gameObject.SetActive(b);
        }
    }

    public void Destroy()
    {
        if (mLine != null)
        {
            GameObject.Destroy(mLine.gameObject);
        }
        mLine = null;
        mIsShow = false;
    }
}
