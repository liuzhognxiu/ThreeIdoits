//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: OnHook.proto
// Note: requires additional types generated from: Bag.proto
namespace onHook
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnHookRequest")]
  public partial class OnHookRequest : global::ProtoBuf.IExtensible
  {
    public OnHookRequest() {}
    
    private int _mapId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"mapId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mapId
    {
      get { return _mapId; }
      set { _mapId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnHookMsg")]
  public partial class OnHookMsg : global::ProtoBuf.IExtensible
  {
    public OnHookMsg() {}
    
    private long _totalTime;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"totalTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long totalTime
    {
      get { return _totalTime; }
      set { _totalTime = value; }
    }
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _rewardItem = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(2, Name=@"rewardItem", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> rewardItem
    {
      get { return _rewardItem; }
    }
  
    private bool _isOnHook;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"isOnHook", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isOnHook
    {
      get { return _isOnHook; }
      set { _isOnHook = value; }
    }
    private int _mapId;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"mapId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mapId
    {
      get { return _mapId; }
      set { _mapId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}