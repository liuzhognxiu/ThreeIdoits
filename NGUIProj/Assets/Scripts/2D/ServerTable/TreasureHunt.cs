//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: TreasureHunt.proto
// Note: requires additional types generated from: Bag.proto
namespace treasureHunt
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TreasureHuntInfo")]
  public partial class TreasureHuntInfo : global::ProtoBuf.IExtensible
  {
    public TreasureHuntInfo() {}
    
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _itemInfo = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"itemInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> itemInfo
    {
      get { return _itemInfo; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TreasureRequest")]
  public partial class TreasureRequest : global::ProtoBuf.IExtensible
  {
    public TreasureRequest() {}
    
    private int _type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TreasureItemChangeList")]
  public partial class TreasureItemChangeList : global::ProtoBuf.IExtensible
  {
    public TreasureItemChangeList() {}
    
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _changeList = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"changeList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> changeList
    {
      get { return _changeList; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetTreasureItemRequest")]
  public partial class GetTreasureItemRequest : global::ProtoBuf.IExtensible
  {
    public GetTreasureItemRequest() {}
    
    private int _index;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int index
    {
      get { return _index; }
      set { _index = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ServerHistory")]
  public partial class ServerHistory : global::ProtoBuf.IExtensible
  {
    public ServerHistory() {}
    
    private readonly global::System.Collections.Generic.List<string> _serverHistory = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(1, Name=@"serverHistory", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> serverHistory
    {
      get { return _serverHistory; }
    }
  
    private int _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _history = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(3, Name=@"history", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> history
    {
      get { return _history; }
    }
  

    private int? _limitTreasure;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"limitTreasure", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int limitTreasure
    {
      get { return _limitTreasure?? default(int); }
      set { _limitTreasure = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool limitTreasureSpecified
    {
      get { return _limitTreasure != null; }
      set { if (value == (_limitTreasure== null)) _limitTreasure = value ? limitTreasure : (int?)null; }
    }
    private bool ShouldSerializelimitTreasure() { return limitTreasureSpecified; }
    private void ResetlimitTreasure() { limitTreasureSpecified = false; }
    

    private long? _openLimitTime;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"openLimitTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long openLimitTime
    {
      get { return _openLimitTime?? default(long); }
      set { _openLimitTime = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool openLimitTimeSpecified
    {
      get { return _openLimitTime != null; }
      set { if (value == (_openLimitTime== null)) _openLimitTime = value ? openLimitTime : (long?)null; }
    }
    private bool ShouldSerializeopenLimitTime() { return openLimitTimeSpecified; }
    private void ResetopenLimitTime() { openLimitTimeSpecified = false; }
    

    private int? _highEquipNumber;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"highEquipNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int highEquipNumber
    {
      get { return _highEquipNumber?? default(int); }
      set { _highEquipNumber = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool highEquipNumberSpecified
    {
      get { return _highEquipNumber != null; }
      set { if (value == (_highEquipNumber== null)) _highEquipNumber = value ? highEquipNumber : (int?)null; }
    }
    private bool ShouldSerializehighEquipNumber() { return highEquipNumberSpecified; }
    private void ResethighEquipNumber() { highEquipNumberSpecified = false; }
    

    private int? _rewardNumber;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"rewardNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int rewardNumber
    {
      get { return _rewardNumber?? default(int); }
      set { _rewardNumber = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool rewardNumberSpecified
    {
      get { return _rewardNumber != null; }
      set { if (value == (_rewardNumber== null)) _rewardNumber = value ? rewardNumber : (int?)null; }
    }
    private bool ShouldSerializerewardNumber() { return rewardNumberSpecified; }
    private void ResetrewardNumber() { rewardNumberSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ExchangePointRequest")]
  public partial class ExchangePointRequest : global::ProtoBuf.IExtensible
  {
    public ExchangePointRequest() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ExpUseRequest")]
  public partial class ExpUseRequest : global::ProtoBuf.IExtensible
  {
    public ExpUseRequest() {}
    
    private readonly global::System.Collections.Generic.List<int> _indexList = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(1, Name=@"indexList", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> indexList
    {
      get { return _indexList; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TreasureIdResponse")]
  public partial class TreasureIdResponse : global::ProtoBuf.IExtensible
  {
    public TreasureIdResponse() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}