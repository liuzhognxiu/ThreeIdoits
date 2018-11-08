using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogicMsg;
using Google.Protobuf;
using System;

public class loginrespAction : GameAction
{

    public loginrespAction():base((int)GameCmd.GameCmd.LoginResp)
    {
        Head.ActionRespId = (int)GameCmd.GameCmd.LoginResp;
    }

    protected override void DecodePackage(NetReader reader)
    {

        LogicMsg.LoginResp resp = LogicMsg.LoginResp.Parser.ParseFrom(reader.Buffer);

        foreach (byte b in reader.Buffer)
        {
            Debug.Log(b);
        }

        Debug.Log("resp.AccountId: " + resp.AccountId + " resp.result: " + resp.Result);
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {

    }

    protected override void SendParameter(NetWriter writer, IMessage pbData)
    {

    }

    public override ActionResult GetResponseData()
    {
        return null;
    }
}
