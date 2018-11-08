using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILuaTools : MonoBehaviour {

    public static GameObject AddCollider(GameObject go)
    {
        Collider col = go.GetComponent<Collider>();
        BoxCollider box = col as BoxCollider;
        if (box == null)
        {
            if (col != null)
            {
                if (Application.isPlaying) GameObject.Destroy(col);
                else GameObject.DestroyImmediate(col);
            }
            box = go.AddComponent<BoxCollider>();
        }
        box.center = new Vector3(0, 0, 10);
        box.size = new Vector3(2000, 1000);

        return box.gameObject;
    }

    public static UIPanel[] GetComponentsInChildren(GameObject go)
    {
        UIPanel[] value = go.GetComponentsInChildren<UIPanel>();
        return value;
    }

}
