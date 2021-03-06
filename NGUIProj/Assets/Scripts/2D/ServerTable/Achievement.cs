//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Achievement.proto
// Note: requires additional types generated from: Bag.proto
namespace achievement
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AchievementInfo")]
  public partial class AchievementInfo : global::ProtoBuf.IExtensible
  {
    public AchievementInfo() {}
    
    private readonly global::System.Collections.Generic.List<long> _achievementData = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"achievementData", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> achievementData
    {
      get { return _achievementData; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetAchievementRewardRequest")]
  public partial class GetAchievementRewardRequest : global::ProtoBuf.IExtensible
  {
    public GetAchievementRewardRequest() {}
    
    private int _achievementId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"achievementId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int achievementId
    {
      get { return _achievementId; }
      set { _achievementId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetAutoAchievementRewardRequest")]
  public partial class GetAutoAchievementRewardRequest : global::ProtoBuf.IExtensible
  {
    public GetAutoAchievementRewardRequest() {}
    
    private readonly global::System.Collections.Generic.List<int> _achievementIds = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"achievementIds", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<int> achievementIds
    {
      get { return _achievementIds; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetAchievementRewardResponse")]
  public partial class GetAchievementRewardResponse : global::ProtoBuf.IExtensible
  {
    public GetAchievementRewardResponse() {}
    
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _rewardItem = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"rewardItem", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> rewardItem
    {
      get { return _rewardItem; }
    }
  
    private int _achievementNumber;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"achievementNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int achievementNumber
    {
      get { return _achievementNumber; }
      set { _achievementNumber = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _achievementId = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(3, Name=@"achievementId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<int> achievementId
    {
      get { return _achievementId; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MedalUpgradeRequest")]
  public partial class MedalUpgradeRequest : global::ProtoBuf.IExtensible
  {
    public MedalUpgradeRequest() {}
    
    private int _currentId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"currentId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int currentId
    {
      get { return _currentId; }
      set { _currentId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MedalUpgradeResponse")]
  public partial class MedalUpgradeResponse : global::ProtoBuf.IExtensible
  {
    public MedalUpgradeResponse() {}
    
    private readonly global::System.Collections.Generic.List<bag.BagItemInfo> _changedItems = new global::System.Collections.Generic.List<bag.BagItemInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"changedItems", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<bag.BagItemInfo> changedItems
    {
      get { return _changedItems; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}