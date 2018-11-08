using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;

public class ErrorMessage
{
    public int code { get; set; }
    public string msg { get; set; }
}

public class ErrorCodeHandler : MonoBehaviour {
    public Queue<ErrorMessage> errMessages = new Queue<ErrorMessage>();
    private bool bCanPop = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (bCanPop)
        {
            DealWithErrorCode();
        }
    }

    void DealWithErrorCode()
    {
        lock (errMessages)
        {
            if (errMessages.Count > 0)
            {
                bCanPop = false;
                ErrorMessage msg = errMessages.Dequeue();
                //todo popup tip or dialog

                Debug.Log("######### msg is: " + msg.msg + " code is: " + msg.code);

                bCanPop = true; // 之前的错误处理完之后重置
            }
            
        }
    }

    public void AddMessage(JsonObject data)
    {
        lock(errMessages)
        {
            ErrorMessage msg = new ErrorMessage();
            msg.msg = data["msg"].ToString();
            msg.code = Convert.ToInt32(data["code"]);
            errMessages.Enqueue(msg);
        }
    }
}
