using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LogicMsg;
using Google.Protobuf;

public class loginAction : GameAction {
    ActionResult m_result;
    public loginAction():base((int)GameCmd.GameCmd.Login)
    {
        Head.ActionRespId = (int)GameCmd.GameCmd.LoginResp;
    }

    protected override void DecodePackage(NetReader reader)
    {

        LogicMsg.LoginResp resp = LogicMsg.LoginResp.Parser.ParseFrom(reader.Buffer);

        m_result = new ActionResult();
        m_result["Result"] = resp.Result;
        m_result["AccountId"] = resp.AccountId;
        Debug.Log("resp.Result: " + resp.Result + " resp.AccountId: " + resp.AccountId);
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        
    }

    protected override void SendParameter(NetWriter writer, IMessage pbData)
    {
        
    }

    public override ActionResult GetResponseData()
    {
        return m_result;
    }
}
