using Google.Protobuf;
using System;
using UnityEngine;

/// <summary>
/// 游戏Action接口
/// </summary>
public abstract class GameAction
{
    private readonly int _actionId;

    protected GameAction(int actionId)
    {
        Head = new PackageHead() { ActionId = actionId };
    }

    public int ActionId
    {
        get { return Head.ActionId; }
    }

    /// <summary>
    /// 网络延时
    /// </summary>
    public int NetworkAverage { get; set; }

    /// <summary>
    /// 本机上运行消耗时间
    /// </summary>
    public int RuntimeAverage { get; set; }

    public event Action<ActionResult> Callback;
    public PackageHead Head { get; private set; }

    public byte[] Send(ActionParam actionParam)
    {
        NetWriter writer = NetWriter.Instance;
        //SetActionHead(writer);
        SendParameter(writer, actionParam);
        return writer.PostData();
    }

    public byte[] Send(Google.Protobuf.IMessage pbData)
    {
        NetWriter writer = NetWriter.Instance;
        SetActionHead(writer, pbData);

        writer.SetBodyData(PackCodec.Serialize(pbData));
        return writer.PostData();
    }

    /// <summary>
    /// 尝试解Body包
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public bool TryDecodePackage(NetReader reader)
    {
        try
        {
            DecodePackage(reader);
            return true;
        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("Action {0} decode package error:{1}", ActionId, ex));
            return false;
        }
    }

    public void OnCallback(ActionResult result)
    {
        try
        {
            if(Callback != null)
			{
				Callback(result);	
			}
        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("Action {0} callback process error:{1}", ActionId, ex));
        }
    }


    protected virtual void SetActionHead(NetWriter writer, Google.Protobuf.IMessage pbData)
    {
        //writer.writeInt32("actionId", ActionId);
        byte[] bodyBuffer = PackCodec.Serialize(pbData);
        ByteBuffer headBuffer = new ByteBuffer();
        headBuffer.WriteInt(this.ActionId);
        headBuffer.WriteInt(bodyBuffer.Length);
        headBuffer.WriteInt(Head.MsgId);
        headBuffer.WriteInt(0); //body_check;
        headBuffer.WriteInt(0); //head_check;

        writer.SetHeadBuffer(headBuffer.ToBytes());
    }

    protected virtual void SetActionHead(NetWriter writer)
    {
        writer.writeInt32("actionId", ActionId);
    }

    protected abstract void SendParameter(NetWriter writer, ActionParam actionParam);

    protected abstract void SendParameter(NetWriter writer, IMessage pbData);

    protected abstract void DecodePackage(NetReader reader);

    public abstract ActionResult GetResponseData();

}
