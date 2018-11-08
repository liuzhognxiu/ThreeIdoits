using UnityEngine;
using System.Collections;

public class CSObjectPoolItem
{
    public CSObjectPoolBase owner;
    public bool isUse = true;
    public GameObject go;
    public object objParam;
}
