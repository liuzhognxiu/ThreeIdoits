using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using Pomelo.DotNetClient;


public class LoginGUI : MonoBehaviour {
	public static string userName = "";
    public static string password = string.Empty;
	public static string channel = "";
	public static JsonObject users = null;
	
	public static PomeloClient pc = null;
	
	public Texture2D pomelo;
	public GUISkin pomeloSkin; 
	public GUIStyle pomeloStyle;
	
 	void Start() 
    {	
		pomelo = (Texture2D)Resources.Load("pomelo");
		pomeloStyle.normal.textColor = Color.black;
    }
	
	//When quit, release resource
	void Update(){
		if(Input.GetKey(KeyCode.Escape)) {
			if (pc != null) {
				pc.disconnect();
			}
			Application.Quit();
		}
	}
	
	void OnGUI(){
		GUI.skin = pomeloSkin;
		GUI.color = Color.yellow;
		GUI.enabled = true;	
		GUI.Label(new Rect(160, 50, pomelo.width, pomelo.height), pomelo);
		
		GUI.Label(new Rect(75, 350, 50, 20), "name:", pomeloStyle);
		userName = GUI.TextField(new Rect(125, 350, 90, 20), userName);
        GUI.Label(new Rect(75, 450, 50, 20), "password:", pomeloStyle);
        password = GUI.TextField(new Rect(125, 450, 90, 20), password);

        GUI.Label(new Rect(225, 350, 55, 20), "channel:", pomeloStyle);
		channel = GUI.TextField(new Rect(280, 350, 100, 20), channel);
		
		if (GUI.Button(new Rect(410, 350, 70, 20), "Login")) {
			if (pc == null) {
                DontDestroyManager.Instance.NetworkManager.Login(userName, password, channel);
            }
		}

        if (GUI.Button(new Rect(520, 350, 70, 20), "Register"))
        {
            if (pc == null)
            {
                //GameManager.Instance.UserInfo.Action = ConstField.enumUserAction.Register;
                //Connect();
                DontDestroyManager.Instance.NetworkManager.Register(userName, password, channel);
            }
        }

        if (GUI.Button(new Rect(630, 350, 70, 20), "Logout"))
        {
            if (pc == null)
            {
                //GameManager.Instance.UserInfo.Action = ConstField.enumUserAction.Register;
                //Connect();
                DontDestroyManager.Instance.NetworkManager.Logout();
            }
        }

        if (GUI.Button(new Rect(100, 100, 70, 20), "test"))
        {
            PomeloGameManager.Instance.tModel.Count += 1;
        }
    }

 }