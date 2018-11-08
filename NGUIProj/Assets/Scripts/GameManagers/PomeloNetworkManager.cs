using UnityEngine;
using System.Collections;
using Pomelo.DotNetClient;
using SimpleJson;
using System;
using LuaFramework;
using System.Collections.Generic;

public class PomeloNetworkManager : MonoBehaviour {
    private string host = "127.0.0.1";
    private int port = 3014;
    private PomeloClient pc = null;
    private bool isConnected = false;
    private string deviceId = string.Empty;
    private Queue<PomeloPackage> m_pomeloBackPackage = new Queue<PomeloPackage>();
    private Queue<PomeloPackage> m_pomeloSendPackage = new Queue<PomeloPackage>();

    private Action<string, string, string> m_connectCallback = null;

    private static PomeloNetworkManager instance = null;
    public static PomeloNetworkManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }
    
    // Use this for initialization
    void Start () {
        deviceId = "accd";// SystemInfo.deviceUniqueIdentifier.ToString();
        //if(!AppConst.LuaMode)
            CheckConnect(null);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        ProcessRespond();
    }

    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
    }

    void ProcessRespond()
    {
        lock(m_pomeloBackPackage)
        {
            while(m_pomeloBackPackage.Count > 0)
            {
                PomeloPackage pkg = m_pomeloBackPackage.Dequeue();
                if (pkg.luaFunc == null)
                    Debug.Log("fuck1");


                pkg.luaFunc.Call(pkg.ReturnData);
            }
        }
    }

    void EventsListen()
    {
        pc.on("onChannelUserOnline", (data) => {
            Debug.Log("onChannelUserOnline " + data);
        });

        pc.on("onLeave", (data) => {
            
        });

        pc.on("onChat", (data) => {
            
        });
    }

    public void LuaTableTest(LuaInterface.LuaTable table)
    {
        Debug.Log("LuaTableTest values is: " + table.Length);
        IEnumerator<DictionaryEntry> tmp = table.ToDictTable().GetEnumerator();
        while(tmp.MoveNext())
        {
            DictionaryEntry curr = tmp.Current;
            Debug.Log("key " + curr.Key + " value: " + curr.Value );
        }
    }

    public void Request(string route, LuaInterface.LuaTable paramsTable, LuaInterface.LuaFunction func)
    {
        if (pc != null)
        {
            JsonObject msg = new JsonObject();
            IEnumerator<DictionaryEntry> paramList = paramsTable.ToDictTable().GetEnumerator();
            while (paramList.MoveNext())
            {
                DictionaryEntry curr = paramList.Current;
                Debug.Log("key " + curr.Key + " value: " + curr.Value);
                msg[curr.Key.ToString()] = curr.Value;
            }

            pc.request(route, msg, (result) =>
            {
                PomeloPackage pkg = new PomeloPackage();
                if (func == null)
                    Debug.LogError("callback function is null!");

                pkg.luaFunc = func;
                pkg.ReturnData = result.ToString();
                AddResultPackage(pkg);
            });
        }
        else
        {
            Debug.LogError("Pomelo Client is null");
        }
    }

    void AddResultPackage(PomeloPackage pkg)
    {
        lock(m_pomeloBackPackage)
        {
            m_pomeloBackPackage.Enqueue(pkg);
        }
    }

    public void Connect(string host, int port, LuaInterface.LuaFunction func)
    {
        pc = new PomeloClient();
        pc.initClient(host, port);
        pc.connect(null, (data) => {
            JsonObject msg = new JsonObject();
            msg["deviceId"] = deviceId;
            msg["id"] = 1;
            pc.request("gate.gateHandler.queryEntry", msg, (result) => {
                Debug.Log("--------------------------------------");
                Debug.Log(result.ToString());
                Debug.Log("--------------------------------------");
                if (Convert.ToInt32(result["code"]) == 200)
                {
                    pc.disconnect();

                    string h = (string)result["host"];
                    int p = Convert.ToInt32(result["port"]);
                    pc = new PomeloClient();
                    pc.initClient(h, p);
                    pc.connect(null, (dd) =>
                    {
                        JsonObject conectorMessage = new JsonObject();
                        conectorMessage.Add("deviceId", deviceId);
                        pc.request("connector.entryHandler.entry", conectorMessage, (ret) =>
                        {
                            Debug.Log("--------------------------------------");
                            Debug.Log(ret.ToString());
                            Debug.Log("--------------------------------------");
                            if (Convert.ToInt32(ret["code"]) == 200)
                            {
                                isConnected = true;
                                EventsListen();
                            }

                            if (func != null)
                            {
                                PomeloPackage pkg = new PomeloPackage();
                                pkg.luaFunc = func;
                                pkg.ReturnData = ret.ToString();
                                Debug.Log("!!!!!!!!!!!!!!!!!!!! " + pkg.ReturnData);
                                AddResultPackage(pkg);
                            }
                        });
                    });
                }
            });
        });
    }

    public void Connect()
    {
        pc = new PomeloClient();
        pc.initClient(host, port);
        pc.connect(null, (data) => {
            JsonObject msg = new JsonObject();
            msg["deviceId"] = deviceId;
            msg["id"] = 1;
            pc.request("gate.gateHandler.queryEntry", msg, OnQuery);

        });
    }

    bool CheckConnect(Action<string, string, string> callback)
    {
        bool ret = false;
        if (!isConnected)
            Connect();
        else
            ret = true;
        if (callback != null)
        {
            m_connectCallback = callback;
        }

        return ret;
    }

    private void onLogin(string username, string psw, string channel)
    {
        if (pc != null)
        {
            JsonObject userMessage = new JsonObject();
            userMessage.Add("username", username);
            userMessage.Add("password", psw);
            userMessage.Add("channel", channel);
            
            pc.request("user.userHandler.login", userMessage, (result) =>
            {
                if (Convert.ToInt32(result["code"]) == 200)
                {
                    PomeloGameManager.Instance.UserInfo.UpdateUserInfo(result);
                    DontDestroyManager.Instance.SceneStatusManager.AddMessage(new SceneMessage() { Status = SceneStatus.Chat });
                }
                else
                {
                    DontDestroyManager.Instance.ErrorCodeHandler.AddMessage(result);
                }
            });
        }

        m_connectCallback = null;
    }

    public void Login(string username, string psw, string channel)
    {
        if (CheckConnect(onLogin))
        {
            m_connectCallback(username, psw, channel);
        }
    }

    void OnQuery(JsonObject result)
    {
        if (Convert.ToInt32(result["code"]) == 200)
        {
            pc.disconnect();

            string host = (string)result["host"];
            int port = Convert.ToInt32(result["port"]);
            pc = new PomeloClient();
            pc.initClient(host, port);
            pc.connect(null, (data) =>
            {
                JsonObject conectorMessage = new JsonObject();
                conectorMessage.Add("deviceId", deviceId);
                pc.request("connector.entryHandler.entry", conectorMessage, (ret) =>
                {
                    if (Convert.ToInt32(ret["code"]) == 200)
                    {
                        isConnected = true;
                        EventsListen();
                        Debug.Log("...... " + data);
                    }
                });
            });
        }
    }

    private void onRegister(string userName, string password, string channel)
    {
        if (pc != null)
        {
            JsonObject userMessage = new JsonObject();
            userMessage.Add("username", userName);
            userMessage.Add("password", password);
            userMessage.Add("channel", channel);

            pc.request("user.userHandler.register", userMessage, (result) =>
            {
                if (Convert.ToInt32(result["code"]) == 200)
                {
                    PomeloGameManager.Instance.UserInfo.UpdateUserInfo(result);
                    DontDestroyManager.Instance.SceneStatusManager.AddMessage(new SceneMessage() { Status = SceneStatus.Chat });
                }
                else
                {
                    DontDestroyManager.Instance.ErrorCodeHandler.AddMessage(result);
                }
            });
        }

        m_connectCallback = null;
    }

    public void Register(string userName, string password, string channel)
    {
        if (CheckConnect(onRegister))
            m_connectCallback(userName, password, channel);

    }

    public void Logout()
    {
        pc.disconnect();
        isConnected = false;
        m_connectCallback = null;
    }

    void OnApplicationQuit()
    {
        if (pc != null)
        {
            pc.disconnect();
        }
    }

}
