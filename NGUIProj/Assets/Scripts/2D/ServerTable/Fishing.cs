//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Fishing.proto
namespace fishing
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FishingRoleMsg")]
  public partial class FishingRoleMsg : global::ProtoBuf.IExtensible
  {
    public FishingRoleMsg() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }

    private long? _time;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long time
    {
      get { return _time?? default(long); }
      set { _time = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool timeSpecified
    {
      get { return _time != null; }
      set { if (value == (_time== null)) _time = value ? time : (long?)null; }
    }
    private bool ShouldSerializetime() { return timeSpecified; }
    private void Resettime() { timeSpecified = false; }
    

    private int? _fishId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"fishId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int fishId
    {
      get { return _fishId?? default(int); }
      set { _fishId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool fishIdSpecified
    {
      get { return _fishId != null; }
      set { if (value == (_fishId== null)) _fishId = value ? fishId : (int?)null; }
    }
    private bool ShouldSerializefishId() { return fishIdSpecified; }
    private void ResetfishId() { fishIdSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StopFishingRequest")]
  public partial class StopFishingRequest : global::ProtoBuf.IExtensible
  {
    public StopFishingRequest() {}
    
    private long _time;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long time
    {
      get { return _time; }
      set { _time = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StartFishingRequest")]
  public partial class StartFishingRequest : global::ProtoBuf.IExtensible
  {
    public StartFishingRequest() {}
    
    private long _fishingPointId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"fishingPointId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long fishingPointId
    {
      get { return _fishingPointId; }
      set { _fishingPointId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ExchangeFishRequest")]
  public partial class ExchangeFishRequest : global::ProtoBuf.IExtensible
  {
    public ExchangeFishRequest() {}
    
    private int _fishId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"fishId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int fishId
    {
      get { return _fishId; }
      set { _fishId = value; }
    }
    private int _count;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int count
    {
      get { return _count; }
      set { _count = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}