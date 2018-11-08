
//-------------------------------------------
//Terrain Data
//author LiZongFu
//time 2015.12.28
//-------------------------------------------

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SFTerrain
{
    protected string mName;                       // 地图名字
    protected Transform mEntity;                 // 地图
    protected Transform mTextureCellsParent;     // 纹理格子父类 
    protected Vector2 mSize;                      // 地图尺寸
    
    protected SFMisc.Dot2 mDot2 = new SFMisc.Dot2();
    protected Vector3 vt3 = new Vector3();

    //显示区域
    protected int VisionX = 2;
    protected int VisionY = 2;

    public int Columns = 10;     // 竖排
    public int Rows = 10;        // 横排
    protected Transform mCameraTrans;
    public Transform CameraTrans
    {
        get
        {
            if (mCameraTrans == null && Camera.main != null)
                mCameraTrans = Camera.main.transform;
            return mCameraTrans;
        }
    }

    protected int viewMaxx, viewMinx, viewMaxy, viewMiny;

    protected CSBetterList<int> HideCoord = new CSBetterList<int>();
    protected CSBetterList<Vector2> UpdateCoord = new CSBetterList<Vector2>();
    protected SFMisc.Dot2 coord = new SFMisc.Dot2();
    protected SFMisc.Dot2 temCoord = new SFMisc.Dot2();
    protected Vector2 minLocalPosition = new Vector2(float.MaxValue, float.MaxValue);
    protected Texture tex; 
    protected System.Text.StringBuilder str = new System.Text.StringBuilder();
    protected bool mIsCheckRefreshTerrain = false;
    public bool IsCheckRefreshTerrain
    {
        get { return mIsCheckRefreshTerrain; }
        set { mIsCheckRefreshTerrain = value; }
    }

    protected bool mIsWaitLoad = true;
    public bool IsWaitLoad
    {
        get { return mIsWaitLoad; }
        set { mIsWaitLoad = value; }
    }

    public string Name
    {
        get { return mName; }
    }

    protected Vector2 mFillSize = new Vector2();

    public Transform Entity
    {
        get { return mEntity; }
        set { mEntity = value; }
    }

    public Vector2 OldSize
    {
        get { return mSize; }
    }

    public Vector2 NewSize
    {
        get { return new Vector2(Columns * SFTerrainCell.Size.x, Rows * SFTerrainCell.Size.y); }
    }

    public Vector2 FillSize
    {
        get { return mFillSize; }
    }

    public void CB_Build_1()
    {
        mFillSize.x = mSize.x % SFTerrainCell.Size.x;
        mFillSize.y = mSize.y % SFTerrainCell.Size.y;

        Columns = (int)(mSize.x / SFTerrainCell.Size.x) + (mFillSize.x > 0 ? 1 : 0);
        Rows = (int)(mSize.y / SFTerrainCell.Size.y) + (mFillSize.y > 0 ? 1 : 0);
    }

    public void CB_Build_2()
    {
        float x = SFTerrainCell.Size.x / 2;
        float y = (Rows * SFTerrainCell.Size.y - SFTerrainCell.Size.y / 2);

        mEntity.position = new Vector3(x, y, 1f);
    }

    public float CB_CreateCellData_1(int x,float ratio)
    {
         return x * ratio * SFTerrainCell.Size.y;
    }

    public float CB_CreateCellData_2(int y, float ratio)
    {
        return y * ratio * SFTerrainCell.Size.x;
    }

    public int CB_CreateCellData_3(int x,int y)
    {
        return (x << 16) | y;
    }

    public int GetKey(int cellX, int cellY)
    {
        return cellY * 100000 + cellX;
    }

    public int CB_dichotomyFind_1(Vector3 dot, int x, int y)
    {
        //int cellX = Mathf.CeilToInt(dot.x / x) - 1;
        //int cellY = Mathf.CeilToInt((NewSize.y - dot.y) / y) - 1;
        //CSCell cell = CSPreLoadingMgr.Singleton.CurScene.Mesh.getCell(cellX, cellY);
        //if (cell == null)
        //{
        //    if (CSScene.IsLanuchMainPlayer)
        //    {
        //        return CSScene.Sington.MainPlayer.OldCell.Coord;
        //    }
        //    else
        //    {
        //        return CSScene.MainPlayerInfo.Coord;
        //    }
        //}

        //return cell.Coord;
        int cellX = Mathf.CeilToInt((dot.x - minLocalPosition.x) / x) - 1;
        int cellY = Rows - Mathf.CeilToInt((dot.y - minLocalPosition.y) / y) - 1;
        int key = GetKey(cellX, cellY);
        return key;
    }

}
