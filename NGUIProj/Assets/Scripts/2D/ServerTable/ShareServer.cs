//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: ShareServer.proto
namespace shareserver
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChangeLine")]
  public partial class ChangeLine : global::ProtoBuf.IExtensible
  {
    public ChangeLine() {}
    
    private long _userId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"userId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long userId
    {
      get { return _userId; }
      set { _userId = value; }
    }
    private long _roleId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private int _sourceServerId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"sourceServerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sourceServerId
    {
      get { return _sourceServerId; }
      set { _sourceServerId = value; }
    }
    private int _targetServerId;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"targetServerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int targetServerId
    {
      get { return _targetServerId; }
      set { _targetServerId = value; }
    }
    private int _targetServerType;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"targetServerType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int targetServerType
    {
      get { return _targetServerType; }
      set { _targetServerType = value; }
    }
    private string _sign;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"sign", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string sign
    {
      get { return _sign; }
      set { _sign = value; }
    }

    private bool? _reconnect;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"reconnect", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool reconnect
    {
      get { return _reconnect?? default(bool); }
      set { _reconnect = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool reconnectSpecified
    {
      get { return _reconnect != null; }
      set { if (value == (_reconnect== null)) _reconnect = value ? reconnect : (bool?)null; }
    }
    private bool ShouldSerializereconnect() { return reconnectSpecified; }
    private void Resetreconnect() { reconnectSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MsgToClient")]
  public partial class MsgToClient : global::ProtoBuf.IExtensible
  {
    public MsgToClient() {}
    
    private int _messageId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"messageId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int messageId
    {
      get { return _messageId; }
      set { _messageId = value; }
    }
    private bool _sendToClient;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"sendToClient", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool sendToClient
    {
      get { return _sendToClient; }
      set { _sendToClient = value; }
    }
    private int _type;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private byte[] _bytes;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"bytes", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] bytes
    {
      get { return _bytes; }
      set { _bytes = value; }
    }

    private long? _id;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id?? default(long); }
      set { _id = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool idSpecified
    {
      get { return _id != null; }
      set { if (value == (_id== null)) _id = value ? id : (long?)null; }
    }
    private bool ShouldSerializeid() { return idSpecified; }
    private void Resetid() { idSpecified = false; }
    

    private int? _sendServerType;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"sendServerType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sendServerType
    {
      get { return _sendServerType?? default(int); }
      set { _sendServerType = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool sendServerTypeSpecified
    {
      get { return _sendServerType != null; }
      set { if (value == (_sendServerType== null)) _sendServerType = value ? sendServerType : (int?)null; }
    }
    private bool ShouldSerializesendServerType() { return sendServerTypeSpecified; }
    private void ResetsendServerType() { sendServerTypeSpecified = false; }
    

    private int? _sendToServerType;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"sendToServerType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sendToServerType
    {
      get { return _sendToServerType?? default(int); }
      set { _sendToServerType = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool sendToServerTypeSpecified
    {
      get { return _sendToServerType != null; }
      set { if (value == (_sendToServerType== null)) _sendToServerType = value ? sendToServerType : (int?)null; }
    }
    private bool ShouldSerializesendToServerType() { return sendToServerTypeSpecified; }
    private void ResetsendToServerType() { sendToServerTypeSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GameLoginShare")]
  public partial class GameLoginShare : global::ProtoBuf.IExtensible
  {
    public GameLoginShare() {}
    
    private int _serverId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"serverId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int serverId
    {
      get { return _serverId; }
      set { _serverId = value; }
    }
    private int _serverType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"serverType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int serverType
    {
      get { return _serverType; }
      set { _serverType = value; }
    }
    private string _sign;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"sign", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string sign
    {
      get { return _sign; }
      set { _sign = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PlayerLoginSyn")]
  public partial class PlayerLoginSyn : global::ProtoBuf.IExtensible
  {
    public PlayerLoginSyn() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private bool _onLine;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"onLine", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool onLine
    {
      get { return _onLine; }
      set { _onLine = value; }
    }
    private int _curServerId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"curServerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curServerId
    {
      get { return _curServerId; }
      set { _curServerId = value; }
    }
    private int _curServerType;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"curServerType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curServerType
    {
      get { return _curServerType; }
      set { _curServerType = value; }
    }
    private long _unionId;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"unionId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long unionId
    {
      get { return _unionId; }
      set { _unionId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PlayerOnlineInfo")]
  public partial class PlayerOnlineInfo : global::ProtoBuf.IExtensible
  {
    public PlayerOnlineInfo() {}
    
    private int _curServerId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"curServerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curServerId
    {
      get { return _curServerId; }
      set { _curServerId = value; }
    }
    private int _curServerType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"curServerType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curServerType
    {
      get { return _curServerType; }
      set { _curServerType = value; }
    }
    private readonly global::System.Collections.Generic.List<long> _roleId = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(3, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<long> roleId
    {
      get { return _roleId; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChangeTitleSync")]
  public partial class ChangeTitleSync : global::ProtoBuf.IExtensible
  {
    public ChangeTitleSync() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private int _titleId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"titleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int titleId
    {
      get { return _titleId; }
      set { _titleId = value; }
    }
    private int _type;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ShareRechargeMsg")]
  public partial class ShareRechargeMsg : global::ProtoBuf.IExtensible
  {
    public ShareRechargeMsg() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private int _yuanbao;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"yuanbao", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int yuanbao
    {
      get { return _yuanbao; }
      set { _yuanbao = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ShareActivityRankMsg")]
  public partial class ShareActivityRankMsg : global::ProtoBuf.IExtensible
  {
    public ShareActivityRankMsg() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private int _eventType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    private int _eventId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"eventId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int eventId
    {
      get { return _eventId; }
      set { _eventId = value; }
    }
    private int _number;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"number", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int number
    {
      get { return _number; }
      set { _number = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UnionSynInfo")]
  public partial class UnionSynInfo : global::ProtoBuf.IExtensible
  {
    public UnionSynInfo() {}
    
    private long _unionId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"unionId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long unionId
    {
      get { return _unionId; }
      set { _unionId = value; }
    }
    private int _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private long _roleId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }

    private int? _data;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"data", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int data
    {
      get { return _data?? default(int); }
      set { _data = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool dataSpecified
    {
      get { return _data != null; }
      set { if (value == (_data== null)) _data = value ? data : (int?)null; }
    }
    private bool ShouldSerializedata() { return dataSpecified; }
    private void Resetdata() { dataSpecified = false; }
    

    private long? _data64;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"data64", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long data64
    {
      get { return _data64?? default(long); }
      set { _data64 = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool data64Specified
    {
      get { return _data64 != null; }
      set { if (value == (_data64== null)) _data64 = value ? data64 : (long?)null; }
    }
    private bool ShouldSerializedata64() { return data64Specified; }
    private void Resetdata64() { data64Specified = false; }
    

    private string _str;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"str", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string str
    {
      get { return _str?? ""; }
      set { _str = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool strSpecified
    {
      get { return _str != null; }
      set { if (value == (_str== null)) _str = value ? str : (string)null; }
    }
    private bool ShouldSerializestr() { return strSpecified; }
    private void Resetstr() { strSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ShareTreasureMsg")]
  public partial class ShareTreasureMsg : global::ProtoBuf.IExtensible
  {
    public ShareTreasureMsg() {}
    
    private int _serverId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"serverId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int serverId
    {
      get { return _serverId; }
      set { _serverId = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _history = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(2, Name=@"history", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> history
    {
      get { return _history; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RepeatLoginMsg")]
  public partial class RepeatLoginMsg : global::ProtoBuf.IExtensible
  {
    public RepeatLoginMsg() {}
    
    private long _playerId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"playerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long playerId
    {
      get { return _playerId; }
      set { _playerId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UnionChangeBossNum")]
  public partial class UnionChangeBossNum : global::ProtoBuf.IExtensible
  {
    public UnionChangeBossNum() {}
    

    private long? _unionId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"unionId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long unionId
    {
      get { return _unionId?? default(long); }
      set { _unionId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool unionIdSpecified
    {
      get { return _unionId != null; }
      set { if (value == (_unionId== null)) _unionId = value ? unionId : (long?)null; }
    }
    private bool ShouldSerializeunionId() { return unionIdSpecified; }
    private void ResetunionId() { unionIdSpecified = false; }
    

    private long? _roleId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId?? default(long); }
      set { _roleId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool roleIdSpecified
    {
      get { return _roleId != null; }
      set { if (value == (_roleId== null)) _roleId = value ? roleId : (long?)null; }
    }
    private bool ShouldSerializeroleId() { return roleIdSpecified; }
    private void ResetroleId() { roleIdSpecified = false; }
    

    private int? _type;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type?? default(int); }
      set { _type = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool typeSpecified
    {
      get { return _type != null; }
      set { if (value == (_type== null)) _type = value ? type : (int?)null; }
    }
    private bool ShouldSerializetype() { return typeSpecified; }
    private void Resettype() { typeSpecified = false; }
    

    private int? _killBossNum;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"killBossNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int killBossNum
    {
      get { return _killBossNum?? default(int); }
      set { _killBossNum = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool killBossNumSpecified
    {
      get { return _killBossNum != null; }
      set { if (value == (_killBossNum== null)) _killBossNum = value ? killBossNum : (int?)null; }
    }
    private bool ShouldSerializekillBossNum() { return killBossNumSpecified; }
    private void ResetkillBossNum() { killBossNumSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SahreServerOnlineTime")]
  public partial class SahreServerOnlineTime : global::ProtoBuf.IExtensible
  {
    public SahreServerOnlineTime() {}
    

    private long? _unionId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"unionId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long unionId
    {
      get { return _unionId?? default(long); }
      set { _unionId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool unionIdSpecified
    {
      get { return _unionId != null; }
      set { if (value == (_unionId== null)) _unionId = value ? unionId : (long?)null; }
    }
    private bool ShouldSerializeunionId() { return unionIdSpecified; }
    private void ResetunionId() { unionIdSpecified = false; }
    

    private long? _roleId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId?? default(long); }
      set { _roleId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool roleIdSpecified
    {
      get { return _roleId != null; }
      set { if (value == (_roleId== null)) _roleId = value ? roleId : (long?)null; }
    }
    private bool ShouldSerializeroleId() { return roleIdSpecified; }
    private void ResetroleId() { roleIdSpecified = false; }
    

    private long? _time;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
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
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}