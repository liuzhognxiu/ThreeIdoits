//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Social.proto
namespace social
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FriendInfo")]
  public partial class FriendInfo : global::ProtoBuf.IExtensible
  {
    public FriendInfo() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private int _sex;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sex
    {
      get { return _sex; }
      set { _sex = value; }
    }
    private int _career;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"career", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int career
    {
      get { return _career; }
      set { _career = value; }
    }
    private int _level;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }
    private bool _isOnline;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"isOnline", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isOnline
    {
      get { return _isOnline; }
      set { _isOnline = value; }
    }

    private int? _relation;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"relation", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int relation
    {
      get { return _relation?? default(int); }
      set { _relation = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool relationSpecified
    {
      get { return _relation != null; }
      set { if (value == (_relation== null)) _relation = value ? relation : (int?)null; }
    }
    private bool ShouldSerializerelation() { return relationSpecified; }
    private void Resetrelation() { relationSpecified = false; }
    

    private int? _curServerId;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"curServerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curServerId
    {
      get { return _curServerId?? default(int); }
      set { _curServerId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool curServerIdSpecified
    {
      get { return _curServerId != null; }
      set { if (value == (_curServerId== null)) _curServerId = value ? curServerId : (int?)null; }
    }
    private bool ShouldSerializecurServerId() { return curServerIdSpecified; }
    private void ResetcurServerId() { curServerIdSpecified = false; }
    

    private int? _curServerType;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"curServerType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curServerType
    {
      get { return _curServerType?? default(int); }
      set { _curServerType = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool curServerTypeSpecified
    {
      get { return _curServerType != null; }
      set { if (value == (_curServerType== null)) _curServerType = value ? curServerType : (int?)null; }
    }
    private bool ShouldSerializecurServerType() { return curServerTypeSpecified; }
    private void ResetcurServerType() { curServerTypeSpecified = false; }
    

    private int? _crossServerTimes;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"crossServerTimes", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int crossServerTimes
    {
      get { return _crossServerTimes?? default(int); }
      set { _crossServerTimes = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool crossServerTimesSpecified
    {
      get { return _crossServerTimes != null; }
      set { if (value == (_crossServerTimes== null)) _crossServerTimes = value ? crossServerTimes : (int?)null; }
    }
    private bool ShouldSerializecrossServerTimes() { return crossServerTimesSpecified; }
    private void ResetcrossServerTimes() { crossServerTimesSpecified = false; }
    

    private bool? _inCrossServer;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"inCrossServer", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool inCrossServer
    {
      get { return _inCrossServer?? default(bool); }
      set { _inCrossServer = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool inCrossServerSpecified
    {
      get { return _inCrossServer != null; }
      set { if (value == (_inCrossServer== null)) _inCrossServer = value ? inCrossServer : (bool?)null; }
    }
    private bool ShouldSerializeinCrossServer() { return inCrossServerSpecified; }
    private void ResetinCrossServer() { inCrossServerSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SocialInfo")]
  public partial class SocialInfo : global::ProtoBuf.IExtensible
  {
    public SocialInfo() {}
    
    private readonly global::System.Collections.Generic.List<social.FriendInfo> _friends = new global::System.Collections.Generic.List<social.FriendInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"friends", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<social.FriendInfo> friends
    {
      get { return _friends; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AddRelationRequest")]
  public partial class AddRelationRequest : global::ProtoBuf.IExtensible
  {
    public AddRelationRequest() {}
    
    private readonly global::System.Collections.Generic.List<long> _roleId = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> roleId
    {
      get { return _roleId; }
    }
  
    private int _relation;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"relation", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int relation
    {
      get { return _relation; }
      set { _relation = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AddFriendByNameRequest")]
  public partial class AddFriendByNameRequest : global::ProtoBuf.IExtensible
  {
    public AddFriendByNameRequest() {}
    
    private string _name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AddRelationResponse")]
  public partial class AddRelationResponse : global::ProtoBuf.IExtensible
  {
    public AddRelationResponse() {}
    
    private readonly global::System.Collections.Generic.List<social.FriendInfo> _added = new global::System.Collections.Generic.List<social.FriendInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"added", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<social.FriendInfo> added
    {
      get { return _added; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FindPlayerByNameRequest")]
  public partial class FindPlayerByNameRequest : global::ProtoBuf.IExtensible
  {
    public FindPlayerByNameRequest() {}
    
    private string _name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FindPlayerByNameResponse")]
  public partial class FindPlayerByNameResponse : global::ProtoBuf.IExtensible
  {
    public FindPlayerByNameResponse() {}
    
    private readonly global::System.Collections.Generic.List<social.FriendInfo> _players = new global::System.Collections.Generic.List<social.FriendInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"players", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<social.FriendInfo> players
    {
      get { return _players; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DeleteRelationRequest")]
  public partial class DeleteRelationRequest : global::ProtoBuf.IExtensible
  {
    public DeleteRelationRequest() {}
    
    private readonly global::System.Collections.Generic.List<long> _roleId = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> roleId
    {
      get { return _roleId; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SummonFriendRequest")]
  public partial class SummonFriendRequest : global::ProtoBuf.IExtensible
  {
    public SummonFriendRequest() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SummonByFriendMsg")]
  public partial class SummonByFriendMsg : global::ProtoBuf.IExtensible
  {
    public SummonByFriendMsg() {}
    
    private social.FriendInfo _friend;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"friend", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public social.FriendInfo friend
    {
      get { return _friend; }
      set { _friend = value; }
    }
    private int _mapId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"mapId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mapId
    {
      get { return _mapId; }
      set { _mapId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ConfirmSummonFriendRequest")]
  public partial class ConfirmSummonFriendRequest : global::ProtoBuf.IExtensible
  {
    public ConfirmSummonFriendRequest() {}
    
    private long _friendId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"friendId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long friendId
    {
      get { return _friendId; }
      set { _friendId = value; }
    }
    private bool _confirm;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"confirm", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool confirm
    {
      get { return _confirm; }
      set { _confirm = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TraceEnemyRequest")]
  public partial class TraceEnemyRequest : global::ProtoBuf.IExtensible
  {
    public TraceEnemyRequest() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TraceEnemyResponse")]
  public partial class TraceEnemyResponse : global::ProtoBuf.IExtensible
  {
    public TraceEnemyResponse() {}
    
    private social.FriendInfo _enemy;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"enemy", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public social.FriendInfo enemy
    {
      get { return _enemy; }
      set { _enemy = value; }
    }
    private int _mapId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"mapId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mapId
    {
      get { return _mapId; }
      set { _mapId = value; }
    }
    private int _line;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"line", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int line
    {
      get { return _line; }
      set { _line = value; }
    }
    private int _x;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RecommendFriendsResponse")]
  public partial class RecommendFriendsResponse : global::ProtoBuf.IExtensible
  {
    public RecommendFriendsResponse() {}
    
    private readonly global::System.Collections.Generic.List<social.FriendInfo> _roleAddedMe = new global::System.Collections.Generic.List<social.FriendInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"roleAddedMe", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<social.FriendInfo> roleAddedMe
    {
      get { return _roleAddedMe; }
    }
  
    private readonly global::System.Collections.Generic.List<social.FriendInfo> _recommend = new global::System.Collections.Generic.List<social.FriendInfo>();
    [global::ProtoBuf.ProtoMember(2, Name=@"recommend", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<social.FriendInfo> recommend
    {
      get { return _recommend; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AddedFriendByPlayerResponse")]
  public partial class AddedFriendByPlayerResponse : global::ProtoBuf.IExtensible
  {
    public AddedFriendByPlayerResponse() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RelationInfo")]
  public partial class RelationInfo : global::ProtoBuf.IExtensible
  {
    public RelationInfo() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private int _relationType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"relationType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int relationType
    {
      get { return _relationType; }
      set { _relationType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RelationAllResponse")]
  public partial class RelationAllResponse : global::ProtoBuf.IExtensible
  {
    public RelationAllResponse() {}
    
    private readonly global::System.Collections.Generic.List<social.RelationInfo> _relationInfo = new global::System.Collections.Generic.List<social.RelationInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"relationInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<social.RelationInfo> relationInfo
    {
      get { return _relationInfo; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}