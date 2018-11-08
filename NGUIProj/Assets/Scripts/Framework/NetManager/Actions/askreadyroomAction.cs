using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class askreadyroomAction : GameAction
{
    ActionResult m_result;
    public askreadyroomAction():base((int)GameCmd.GameCmd.AskReadyRoom)
    {
        Head.ActionRespId = (int)GameCmd.GameCmd.AskReadyRoomResp;
    }

    protected override void DecodePackage(NetReader reader)
    {

        LogicMsg.AskReadyRoomResp resp = LogicMsg.AskReadyRoomResp.Parser.ParseFrom(reader.Buffer);

        m_result = new ActionResult();
        m_result["State"] = resp.State;
        m_result["AccountId"] = resp.AccountId;
        Debug.Log("resp.State: " + resp.State + " resp.AccountId: " + resp.AccountId);
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
