using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class registerAction : GameAction
{
    ActionResult m_result;
    public registerAction():base((int)GameCmd.GameCmd.Register)
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
