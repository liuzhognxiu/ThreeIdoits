
//-------------------------------------------------------------------------
//Resource load
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------


using UnityEngine;
using System.Collections;

public class SFTerrainCell
{
    public GameObject go;
    protected Transform mCacheTrans;

    public readonly static Vector2 Size = new Vector2(512f, 256f);
    public readonly static Vector2 HalfSize = new Vector2(256f, 128f);
    public readonly static Vector3 HalfSize3 = new Vector3(256f, 128f, 0);
    //private Vector2 UV;
    protected Vector4 Bounds;
    //private Texture OldTex;
    protected Transform parent;


    protected bool mIsFinish = false;
    public bool IsFinish
    {
        get { return mIsFinish; }
        set { mIsFinish = value; }
    }
}
