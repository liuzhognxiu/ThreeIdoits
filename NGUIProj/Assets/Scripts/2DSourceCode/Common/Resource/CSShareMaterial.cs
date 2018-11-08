using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EShareMatType
{
    Normal,
    Transparent,
    Balck,
    Balck_Transparent,
    ColorBright,
    ColorBright_Transparent,
    ColorSet_Grey,
    ColorSet_Grey_Transparent,
    ColorSet_Green,
    ColorSet_Green_Transparent,
    ColorSet_Red,
    ColorSet_Red_Transparent,
    ColorSet_Blue,
    ColorSet_Blue_Transparent,
    ColorAdd,
    ColorAdd_Transparent,
    DeadTransparent,
}

 [System.Serializable]
public class CSShareMaterial 
{
    public Dictionary<int, List<Material>> InstanceIDToShareMat = new Dictionary<int, List<Material>>();
    public Dictionary<int, Material> InstanceIDToYeManDic = new Dictionary<int, Material>();
    public Dictionary<int, string> TypeToShaderName = new Dictionary<int, string>()
    {
        //{EShareMatType.Normal,"Mobile/LZF/Alpha Blended"},
        //{EShareMatType.Transparent,"Particles/Additive (Soft)"},
        {(int)EShareMatType.Normal,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.Transparent,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.Balck,"Mobile/LZF/Black"},
        {(int)EShareMatType.Balck_Transparent,"Mobile/LZF/Black"},
        {(int)EShareMatType.ColorBright,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorBright_Transparent,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Grey,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Grey_Transparent,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Green,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Green_Transparent,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Red,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Red_Transparent,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Blue,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorSet_Blue_Transparent,"Mobile/LZF/ColorSet"},
        {(int)EShareMatType.ColorAdd,"Mobile/LZF/ColorAdd"},
        {(int)EShareMatType.ColorAdd_Transparent,"Mobile/LZF/ColorAdd"},
    };

     public void Clear()
    {
        InstanceIDToShareMat.Clear();
        InstanceIDToYeManDic.Clear();
    }

     public string GetShader(EShareMatType type)
    {
        if (TypeToShaderName.ContainsKey((int)type))
            return TypeToShaderName[(int)type];
        return "";
    }

     //Material mBlackMat = null;
     public Material GetShaderMatByYeMan(UnityEngine.Object obj,string shaderName)
     {
         if (obj == null) return null;
         int id = obj.GetInstanceID();
         if (!InstanceIDToYeManDic.ContainsKey(id))
         {
             //Debug.LogError(shaderName);
             Shader shader = Shader.Find(shaderName);
             Material mat = new Material(shader);
             InstanceIDToYeManDic.Add(id, mat);
         }
         return InstanceIDToYeManDic[id];
     }

    public Material GetShareMat(UnityEngine.Object obj, EShareMatType type = EShareMatType.Normal, string shaderName = "")
    {
        //return null;
        //if (type == EShareMatType.Balck)
        //{
        //    if (mBlackMat != null) return mBlackMat;
        //    shaderName = !string.IsNullOrEmpty(shaderName) ? shaderName : TypeToShaderName[type];
        //    //Debug.LogError(shaderName);
        //    Shader shader = Shader.Find(shaderName);
        //    mBlackMat = new Material(shader);
        //    mBlackMat.name = "Black";
        //    return mBlackMat;
        //}
        if(type == EShareMatType.Normal)
        {
            SFAtlas atlas = obj as SFAtlas;
            if (atlas != null)
            {
                return atlas.spriteMaterial;
            }
        }
        if(obj == null)return null;
        int id = obj.GetInstanceID();
        if (!InstanceIDToShareMat.ContainsKey(id))
        {
            InstanceIDToShareMat.Add(id, new List<Material>());
        }
        if ((int)type >= InstanceIDToShareMat[id].Count)
        {
            int count = InstanceIDToShareMat[id].Count;
            for (int i = 0; i <= (int)type - count; i++)
            {
                InstanceIDToShareMat[id].Add(null);
            }
        }
        if (InstanceIDToShareMat[id][(int)type] == null)
        {
            shaderName = !string.IsNullOrEmpty(shaderName) ? shaderName : TypeToShaderName[(int)type];
            //Debug.LogError(shaderName);
            Shader shader = Shader.Find(shaderName);
            Material mat = new Material(shader);
            mat.name = TypeToShaderName[(int)type] /*+ "_" + id*/;
            InstanceIDToShareMat[id][(int)type] = mat;

        }
        return InstanceIDToShareMat[id][(int)type];
    }

    public Material GetNewMaterial(EShareMatType type = EShareMatType.Normal, string shaderName = "")
    {
        shaderName = string.IsNullOrEmpty(shaderName) ? TypeToShaderName[(int)type] : shaderName;
        Shader shader = Shader.Find(shaderName);
        return new Material(shader);
    }
}
