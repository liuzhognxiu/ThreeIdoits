using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class askaddroomAction : GameAction
{
    ActionResult m_result;
    public askaddroomAction():base((int)GameCmd.GameCmd.AskAddRoom)
    {
        Head.ActionRespId = (int)GameCmd.GameCmd.AskAddRoomResp;
    }

    protected override void DecodePackage(NetReader reader)
    {

        LogicMsg.AskAddRoomResp resp = LogicMsg.AskAddRoomResp.Parser.ParseFrom(reader.Buffer);

        m_result = new ActionResult();
        m_result["State"] = resp.State;
        Debug.Log("resp.State: " + resp.State);
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
