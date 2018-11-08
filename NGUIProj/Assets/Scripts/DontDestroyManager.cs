using UnityEngine;
using System.Collections;

public class DontDestroyManager : MonoBehaviour {
    private static DontDestroyManager instance = null;
    public static DontDestroyManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    private SceneStatusManager sceneStatusManager;
    public SceneStatusManager SceneStatusManager
    {
        get
        {
            return sceneStatusManager;
        }
    }

    private PomeloGameManager gameManager;
    public PomeloGameManager GameManager
    {
        get
        {
            return gameManager;
        }
    }

    private ErrorCodeHandler errorCodeHandler;
    public ErrorCodeHandler ErrorCodeHandler
    {
        get
        {
            return errorCodeHandler;
        }
    }

    private PomeloNetworkManager networkManager;
    public PomeloNetworkManager NetworkManager
    {
        get
        {
            return networkManager;
        }
    }

    // Use this for initialization
    void Start () {
        instance = this;
        DontDestroyOnLoad(gameObject);
        sceneStatusManager = gameObject.AddComponent<SceneStatusManager>();
        gameManager = gameObject.AddComponent<PomeloGameManager>();
        errorCodeHandler = gameObject.AddComponent<ErrorCodeHandler>();
        networkManager = gameObject.AddComponent<PomeloNetworkManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
