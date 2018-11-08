using UnityEngine;
using System.Collections;

public class CSGameMgrBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T mInstance = default(T);
    public static T Instance
    {
        get
        {
            return mInstance;
        }
        set { mInstance = value; }
    }

    private static Transform mCahcheTrans = null;
    public static UnityEngine.Transform CahcheTrans
    {
        get { return mCahcheTrans; }
        set { mCahcheTrans = value; }
    }

    public virtual bool IsDonotDestroy
    {
        get
        {
            return false;
        }
    }

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        if (IsDonotDestroy)
        {
            if (CahcheTrans == null)
            {
                if (Debug.developerConsoleVisible) Debug.LogError(name);
            }
            //DontDestroyOnLoad(CahcheTrans.gameObject);
        }
    }

    public static void CreateInstance(Transform parent)
    {
        if (mInstance != default(T)) return;
        GameObject go = new GameObject(typeof(T).ToString());
        if (parent != null)
        {
            go.transform.parent = parent;
        }
        mInstance = go.AddComponent<T>();
        CahcheTrans = go.transform;
    }

    public virtual void Destroy()
    {
        if (CahcheTrans != null)
        {
            if (!IsDonotDestroy)
            {
                UnityEngine.Object.Destroy(CahcheTrans.gameObject);
                CahcheTrans = null;
            }
        }
    }

    public virtual void OnDestroy()
    {
        mInstance = default(T);
        mCahcheTrans = null;
    }
}
