
//-------------------------------------------------------------------------
//CSTerrainMeshData
//Author LiZongFu
//Time 2016.1.11
//-------------------------------------------------------------------------


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public interface ISFMesh
{
    ISfCell getCellByISfCell(SFMisc.Dot2 dot);
    ISfCell getCellByISfCell(int x, int y);
    void GetNeighbours(Node node, CSBetterList<Node> neighbors);
}
[Serializable]
public class SFMesh
{
    [HideInInspector]
    [SerializeField]
    protected string mName;                  // 地图名字
    [HideInInspector]
    [SerializeField]
    protected Transform mEntity;            // 地图
    [HideInInspector]
    [SerializeField]
    protected Transform mEditMeshParent;    // 数据格子父类
    [HideInInspector]
    [SerializeField]
    protected Vector2 mPixelSize;            // 地图尺寸
    [HideInInspector]
    [SerializeField]
    protected Vector2 mMeshSize;             // 地图尺寸
    [HideInInspector]
    [SerializeField]
    protected int mVisionCount = 9;          // 格子显示半径
    [HideInInspector]
    [SerializeField]
    protected bool mLineRendered;            // 线已经渲染
    [HideInInspector]
    [SerializeField]
    protected static int mVerticalCount = 10;       // 竖排
    [HideInInspector]
    [SerializeField]
    protected static int mHorizontalCount = 10;     // 横排

    protected int viewMaxx, viewMinx, viewMaxy, viewMiny;
    protected Dictionary<int, GameObject> AllDisplayMesh = new Dictionary<int, GameObject>();
    protected CSBetterList<int> HideCoord = new CSBetterList<int>();
    protected CSBetterList<Vector2> UpdateCoord = new CSBetterList<Vector2>();
    //private CSBetterList<CSCell> SafeEffectCell = new CSBetterList<CSCell>();

    //bool mDisplay = false;

    protected SFMisc.Dot2 mDot2 = new SFMisc.Dot2();
    protected Vector2 mCellParentOffset;
    public void CB_Init_1(string name, Transform parent, Vector2 terrainSize)
    {
        mName = name;
        mEntity = parent;

        mPixelSize = terrainSize;
        mMeshSize.x = mPixelSize.x;
        mMeshSize.y = mPixelSize.y;

        float exactX = mPixelSize.x % SFCell.Size.x;
        float exactY = mPixelSize.y % SFCell.Size.y;

        mHorizontalCount = (int)(mPixelSize.y / SFCell.Size.y) + (exactY > 0 ? 1 : 0);
        mVerticalCount = (int)(mPixelSize.x / SFCell.Size.x) + (exactX > 0 ? 1 : 0);
        float x = -(SFTerrainCell.HalfSize.x - SFCell.HalfSize.x);
        float y = SFTerrainCell.HalfSize.y - SFCell.HalfSize.y;

        mCellParentOffset = new Vector2(x, y);
    }

    public int CB_GetKey_1(int x, int y)
    {
        return x * 100000 + y;
    }
}
