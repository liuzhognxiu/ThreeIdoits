
//author LiZongFu
//date 2016.5.11

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmeData
{
    public CSBetterList<string> Names = new CSBetterList<string>();
    public int LastFrame = 0;
}
public class ShadowData
{
    public Transform ShadowUvGoTrans;
    public CSSpriteBase ShadowSprite;
}

public class CSBaseAnimation 
{
    public CSSpriteBase Sprite;
    public GameObject Go;
    private Transform mCahcheTrans;
    public UnityEngine.Transform CahcheTrans
    {
        get {
            if (mCahcheTrans == null && Go != null)
                mCahcheTrans = Go.transform;
            return mCahcheTrans; }
    }
    public ISFAvater IAvater;

    //public delegate void OnValidate();
    //public event OnValidate onValidate;



    public Vector2 getSpriteUV() 
    {
        return Sprite == null ? Vector2.zero : Sprite.getUV; 
    }

    public void SetShaodwShader(ISFSprite sprite, EShareMatType t)
    {
        EShareMatType type = t;
        SFMisc.blackColor.a = type == EShareMatType.Transparent ? 0.3f : 0.5f;
        SFMisc.greyColor.r = SFMisc.greyColor.g = SFMisc.greyColor.b = SFMisc.greyColor.a = 1;
        EShareMatType blackType = type == EShareMatType.Transparent ? EShareMatType.Balck_Transparent : EShareMatType.Balck;
        sprite.SetShader(SFOut.IGame.getShareMaterial(sprite.getAtlas, blackType), SFMisc.blackColor, SFMisc.greyColor);
    }

}