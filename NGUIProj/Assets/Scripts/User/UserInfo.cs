using UnityEngine;
using System.Collections;
using System;
using SimpleJson;
using System.Collections.Generic;
using System.Linq;

public class NetUserInfo
{
    public int id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public int follows { get; set; }
}

public class UserInfo {
    private ConstField.enumUserAction action = ConstField.enumUserAction.None;
    public ConstField.enumUserAction Action
    {
        get
        {
            return action;
        }
        set
        {
            action = value;
        }
    }

    private string userName;
    public string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName = value;
        }
    }
    private int userId;
    public int UserId
    {
        get
        {
            return userId;
        }
    }
    private int follows; 
    public int Follows
    {
        get
        {
            return follows;
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateUserInfo(JsonObject data)
    {
        List<NetUserInfo> userData = JsonHelper.DeSerialize<List<NetUserInfo>>(data["msg"].ToString());
        if (userData.Count > 0)
        {
            this.userName = userData[0].name;
            this.userId = userData[0].id;
        }
        List<string> userNames = JsonHelper.DeSerialize<List<string>>(data["users"].ToString());
        
        foreach(string username in userNames)
        {
            PomeloGameManager.Instance.AddOnlineUser(new UserInfo() { UserName = username });
        }
    }
}
