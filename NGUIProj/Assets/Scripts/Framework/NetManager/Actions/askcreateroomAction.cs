using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class askcreateroomAction : GameAction
{
    ActionResult m_result;
    public askcreateroomAction():base((int)GameCmd.GameCmd.AskCreateRoom)
    {
        Head.ActionRespId = (int)GameCmd.GameCmd.AskCreateRoomResp;
    }

    protected override void DecodePackage(NetReader reader)
    {

        LogicMsg.AskCreateRoomResp resp = LogicMsg.AskCreateRoomResp.Parser.ParseFrom(reader.Buffer);

        m_result = new ActionResult();
        m_result["State"] = resp.State;
        m_result["Battleid"] = resp.Battleid;
        Debug.Log("resp.State: " + resp.State + " resp.Battleid: " + resp.Battleid);
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
