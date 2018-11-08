using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PomeloGameManager : MonoBehaviour {
    private static PomeloGameManager s_instance = null;

    public static PomeloGameManager Instance
    {
        get
        {
            return s_instance;
        }
    }

    private UserInfo m_userInfo = new UserInfo();
    public UserInfo UserInfo
    {
        get
        {
            return m_userInfo;
        }
    }

    private List<UserInfo> m_onlineUsers = new List<UserInfo>();
    public List<UserInfo> OnlineUsers
    {
        get
        {
            return m_onlineUsers;
        }
    }

    private testModel m_testModel = null;
    public testModel tModel
    {
        get
        {
            if (m_testModel == null)
                m_testModel = new testModel();

            return m_testModel;
        }
    }

    void Awake()
    {
        s_instance = this;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool AddOnlineUser(UserInfo info)
    {
        if (PomeloGameManager.Instance.OnlineUsers.Where(u => u.UserName == info.UserName).FirstOrDefault() != null)
        {
            Debug.Log("had existed ! " + info.UserName);
            return false;
        }
        else
        {
            PomeloGameManager.Instance.OnlineUsers.Add(new UserInfo() { UserName = info.UserName });
            return true;
        }
    }
}
