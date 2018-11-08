using UnityEngine;
using System.Collections;

public class NetAdapter : MonoBehaviour {
    public string mUrl;
    public string mPort;

    private static NetAdapter instance;
    public static NetAdapter Instance
    {
        get
        {
            return instance;
        }
    }
	// Use this for initialization
	void Start () {
        instance = this;
        //DontDestoryUnload(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string SocketServerPath
    {
        get
        {
            return mUrl + ":" + mPort;
        }
    }
}
