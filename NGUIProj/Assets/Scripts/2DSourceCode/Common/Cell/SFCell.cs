
//-------------------------------------------------------------------------
// Cell
// Author LiZongFu
// Time 2016.1.4
//-------------------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface ISfCell
{
    Vector2 LocalPosition2 { get; }
    bool isAttributesByISFCell(int type);
    SFMisc.Dot2 Coord { get; }
}
[Serializable]
public class SFCell
{
    static public SFMisc.Dot2 Size = new SFMisc.Dot2(60, 40); // 1.5倍 
    static public SFMisc.Dot2 HalfSize = new SFMisc.Dot2(30, 20); // 1.5倍 
    public static float width { get { return HalfSize.x; } }
    public static float height { get { return HalfSize.y; } }
    public static float mworldWidth = 0;
    public static float worldWidth = 0;
    public static float mworldHeight = 0;
    public static float worldHeight = 0;

    protected int getIntx(int num)
    {
        return num >> 16;
    }

    protected int getInty(int num)
    {

        return num & 0xffff;
    }
    protected bool IsHave(int num, int n)
    {
        return (num & (1 << n)) != 0;
    }

    public int CB_getKey_1(int x,int y)
    {
        return x * 100000 + y;
    }

    public Vector3 CB_LocalPosition2_1(Vector2 LocalPosition, Vector2 NewSize)
    {
        Vector3 vec = Vector3.zero;
        vec.x = LocalPosition.x + SFCell.HalfSize.x;
        vec.y = NewSize.y + LocalPosition.y - SFCell.HalfSize.y;
        return vec;
    }
}
