//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Collect.proto
// Note: requires additional types generated from: Bag.proto
namespace collect
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CabinetInfo")]
  public partial class CabinetInfo : global::ProtoBuf.IExtensible
  {
    public CabinetInfo() {}
    
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _collectionItems = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"collectionItems", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> collectionItems
    {
      get { return _collectionItems; }
    }
  
    private int _level;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }
    private int _exp;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"exp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int exp
    {
      get { return _exp; }
      set { _exp = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _linkEffects = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(4, Name=@"linkEffects", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<int> linkEffects
    {
      get { return _linkEffects; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PutCollectionItemMsg")]
  public partial class PutCollectionItemMsg : global::ProtoBuf.IExtensible
  {
    public PutCollectionItemMsg() {}
    
    private int _bagIndex;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"bagIndex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int bagIndex
    {
      get { return _bagIndex; }
      set { _bagIndex = value; }
    }
    private int _page;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"page", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int page
    {
      get { return _page; }
      set { _page = value; }
    }
    private int _x;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }

    private bag.BagItemInfo _item = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"item", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public bag.BagItemInfo item
    {
      get { return _item; }
      set { _item = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RemoveCollectionItemMsg")]
  public partial class RemoveCollectionItemMsg : global::ProtoBuf.IExtensible
  {
    public RemoveCollectionItemMsg() {}
    
    private long _itemId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long itemId
    {
      get { return _itemId; }
      set { _itemId = value; }
    }

    private int? _bagIndex;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"bagIndex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int bagIndex
    {
      get { return _bagIndex?? default(int); }
      set { _bagIndex = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool bagIndexSpecified
    {
      get { return _bagIndex != null; }
      set { if (value == (_bagIndex== null)) _bagIndex = value ? bagIndex : (int?)null; }
    }
    private bool ShouldSerializebagIndex() { return bagIndexSpecified; }
    private void ResetbagIndex() { bagIndexSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SwapCollectionItemMsg")]
  public partial class SwapCollectionItemMsg : global::ProtoBuf.IExtensible
  {
    public SwapCollectionItemMsg() {}
    
    private long _itemId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long itemId
    {
      get { return _itemId; }
      set { _itemId = value; }
    }
    private int _page;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"page", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int page
    {
      get { return _page; }
      set { _page = value; }
    }
    private int _x;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CallbackCollectionMsg")]
  public partial class CallbackCollectionMsg : global::ProtoBuf.IExtensible
  {
    public CallbackCollectionMsg() {}
    
    private readonly global::System.Collections.Generic.List<long> _itemIds = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"itemIds", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<long> itemIds
    {
      get { return _itemIds; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _bagIndices = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"bagIndices", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> bagIndices
    {
      get { return _bagIndices; }
    }
  

    private collect.CabinetInfo _cabinet = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"cabinet", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public collect.CabinetInfo cabinet
    {
      get { return _cabinet; }
      set { _cabinet = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Phase3Reward")]
  public partial class Phase3Reward : global::ProtoBuf.IExtensible
  {
    public Phase3Reward() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private long _itemId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"itemId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long itemId
    {
      get { return _itemId; }
      set { _itemId = value; }
    }
    private int _configId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"configId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int configId
    {
      get { return _configId; }
      set { _configId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CollectionGuideInfo")]
  public partial class CollectionGuideInfo : global::ProtoBuf.IExtensible
  {
    public CollectionGuideInfo() {}
    
    private bool _isActive;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"isActive", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isActive
    {
      get { return _isActive; }
      set { _isActive = value; }
    }

    private int? _phase;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"phase", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int phase
    {
      get { return _phase?? default(int); }
      set { _phase = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool phaseSpecified
    {
      get { return _phase != null; }
      set { if (value == (_phase== null)) _phase = value ? phase : (int?)null; }
    }
    private bool ShouldSerializephase() { return phaseSpecified; }
    private void Resetphase() { phaseSpecified = false; }
    

    private long? _phaseStartTime;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"phaseStartTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long phaseStartTime
    {
      get { return _phaseStartTime?? default(long); }
      set { _phaseStartTime = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool phaseStartTimeSpecified
    {
      get { return _phaseStartTime != null; }
      set { if (value == (_phaseStartTime== null)) _phaseStartTime = value ? phaseStartTime : (long?)null; }
    }
    private bool ShouldSerializephaseStartTime() { return phaseStartTimeSpecified; }
    private void ResetphaseStartTime() { phaseStartTimeSpecified = false; }
    

    private long? _lastCheckTime;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"lastCheckTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long lastCheckTime
    {
      get { return _lastCheckTime?? default(long); }
      set { _lastCheckTime = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool lastCheckTimeSpecified
    {
      get { return _lastCheckTime != null; }
      set { if (value == (_lastCheckTime== null)) _lastCheckTime = value ? lastCheckTime : (long?)null; }
    }
    private bool ShouldSerializelastCheckTime() { return lastCheckTimeSpecified; }
    private void ResetlastCheckTime() { lastCheckTimeSpecified = false; }
    
    private readonly global::System.Collections.Generic.List<long> _phase1Box = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(5, Name=@"phase1Box", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> phase1Box
    {
      get { return _phase1Box; }
    }
  
    private readonly global::System.Collections.Generic.List<long> _phase2Questions = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(6, Name=@"phase2Questions", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> phase2Questions
    {
      get { return _phase2Questions; }
    }
  
    private readonly global::System.Collections.Generic.List<long> _phase3Progress = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(7, Name=@"phase3Progress", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> phase3Progress
    {
      get { return _phase3Progress; }
    }
  
    private readonly global::System.Collections.Generic.List<collect.Phase3Reward> _phase3Reward = new global::System.Collections.Generic.List<collect.Phase3Reward>();
    [global::ProtoBuf.ProtoMember(8, Name=@"phase3Reward", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<collect.Phase3Reward> phase3Reward
    {
      get { return _phase3Reward; }
    }
  
    private readonly global::System.Collections.Generic.List<int> _phase1ConfigIds = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(9, Name=@"phase1ConfigIds", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> phase1ConfigIds
    {
      get { return _phase1ConfigIds; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CollectionGuideAnswerQuestionRequest")]
  public partial class CollectionGuideAnswerQuestionRequest : global::ProtoBuf.IExtensible
  {
    public CollectionGuideAnswerQuestionRequest() {}
    
    private int _answer;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"answer", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int answer
    {
      get { return _answer; }
      set { _answer = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CollectionGuideGetRewardRequest")]
  public partial class CollectionGuideGetRewardRequest : global::ProtoBuf.IExtensible
  {
    public CollectionGuideGetRewardRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StartDiggingRequest")]
  public partial class StartDiggingRequest : global::ProtoBuf.IExtensible
  {
    public StartDiggingRequest() {}
    
    private long _monsterId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"monsterId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long monsterId
    {
      get { return _monsterId; }
      set { _monsterId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DigCollectionResponse")]
  public partial class DigCollectionResponse : global::ProtoBuf.IExtensible
  {
    public DigCollectionResponse() {}
    
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _items = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> items
    {
      get { return _items; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CollectionGuideOpenBoxResponse")]
  public partial class CollectionGuideOpenBoxResponse : global::ProtoBuf.IExtensible
  {
    public CollectionGuideOpenBoxResponse() {}
    
    private long _itemId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long itemId
    {
      get { return _itemId; }
      set { _itemId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}