using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AssetCommon {

}

class InstanceItem
{
    public Object go;
    public Action<GameObject> callBack;
}

public class ResourceItem
{
    private AssetBundle m_assetBundle;
    private Object m_mainAsset;
    private string m_mainAssetName;
    private Object[] m_assets;
    private string[] m_assetsName;
    private string m_path;


}