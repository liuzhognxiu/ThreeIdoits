using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//public enum MapCellStatus
//{
//    Normal,
//    Block,
//}

public class MyMapCell : ScriptableObject {
    public MapCellStatus Status;
    public int X;
    public int Y;
    public Texture2D CellSprite
    {
        get
        {
            Texture2D tex = null;

            switch(Status)
            {
                case MapCellStatus.Normal:
                    tex = AssetDatabase.LoadAssetAtPath("Assets/MapEditor/Textures/normal.png", typeof(Texture2D)) as Texture2D;
                    break;
                case MapCellStatus.Block:
                    tex = AssetDatabase.LoadAssetAtPath("Assets/MapEditor/Textures/block.png", typeof(Texture2D)) as Texture2D;
                    break;
            }

            return tex;
        }
    }

    public GameObject CellObj;

    public void UpdateMapCell()
    {
        MeshFilter filter = CellObj.GetComponent<MeshFilter>();
        MeshRenderer render = CellObj.GetComponent<MeshRenderer>();
        BoxCollider box = CellObj.GetComponent<BoxCollider>();
        render.sharedMaterial.mainTexture = CellSprite;
    }
}
