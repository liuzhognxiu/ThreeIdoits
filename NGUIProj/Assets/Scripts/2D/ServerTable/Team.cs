//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Team.proto
namespace team
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TeamMember")]
  public partial class TeamMember : global::ProtoBuf.IExtensible
  {
    public TeamMember() {}
    
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
    private int _career;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"career", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int career
    {
      get { return _career; }
      set { _career = value; }
    }
    private int _level;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }
    private bool _isOnline;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"isOnline", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isOnline
    {
      get { return _isOnline; }
      set { _isOnline = value; }
    }
    private int _sex;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int sex
    {
      get { return _sex; }
      set { _sex = value; }
    }

    private string _unionName;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"unionName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string unionName
    {
      get { return _unionName?? ""; }
      set { _unionName = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool unionNameSpecified
    {
      get { return _unionName != null; }
      set { if (value == (_unionName== null)) _unionName = value ? unionName : (string)null; }
    }
    private bool ShouldSerializeunionName() { return unionNameSpecified; }
    private void ResetunionName() { unionNameSpecified = false; }
    

    private long? _inviteTeamId;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"inviteTeamId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long inviteTeamId
    {
      get { return _inviteTeamId?? default(long); }
      set { _inviteTeamId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool inviteTeamIdSpecified
    {
      get { return _inviteTeamId != null; }
      set { if (value == (_inviteTeamId== null)) _inviteTeamId = value ? inviteTeamId : (long?)null; }
    }
    private bool ShouldSerializeinviteTeamId() { return inviteTeamIdSpecified; }
    private void ResetinviteTeamId() { inviteTeamIdSpecified = false; }
    

    private int? _nbValue;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"nbValue", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int nbValue
    {
      get { return _nbValue?? default(int); }
      set { _nbValue = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool nbValueSpecified
    {
      get { return _nbValue != null; }
      set { if (value == (_nbValue== null)) _nbValue = value ? nbValue : (int?)null; }
    }
    private bool ShouldSerializenbValue() { return nbValueSpecified; }
    private void ResetnbValue() { nbValueSpecified = false; }
    

    private long? _teamId;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"teamId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long teamId
    {
      get { return _teamId?? default(long); }
      set { _teamId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool teamIdSpecified
    {
      get { return _teamId != null; }
      set { if (value == (_teamId== null)) _teamId = value ? teamId : (long?)null; }
    }
    private bool ShouldSerializeteamId() { return teamIdSpecified; }
    private void ResetteamId() { teamIdSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TeamInfo")]
  public partial class TeamInfo : global::ProtoBuf.IExtensible
  {
    public TeamInfo() {}
    
    private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private long _leaderId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"leaderId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long leaderId
    {
      get { return _leaderId; }
      set { _leaderId = value; }
    }
    private readonly global::System.Collections.Generic.List<team.TeamMember> _members = new global::System.Collections.Generic.List<team.TeamMember>();
    [global::ProtoBuf.ProtoMember(3, Name=@"members", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<team.TeamMember> members
    {
      get { return _members; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TeamBrief")]
  public partial class TeamBrief : global::ProtoBuf.IExtensible
  {
    public TeamBrief() {}
    
    private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }

    private team.TeamMember _leader = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"leader", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public team.TeamMember leader
    {
      get { return _leader; }
      set { _leader = value; }
    }
    private int _size;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"size", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int size
    {
      get { return _size; }
      set { _size = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetTeamTabRequest")]
  public partial class GetTeamTabRequest : global::ProtoBuf.IExtensible
  {
    public GetTeamTabRequest() {}
    
    private team.TeamTab _tab;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"tab", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public team.TeamTab tab
    {
      get { return _tab; }
      set { _tab = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TeamTabInfo")]
  public partial class TeamTabInfo : global::ProtoBuf.IExtensible
  {
    public TeamTabInfo() {}
    
    private team.TeamTab _tab;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"tab", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public team.TeamTab tab
    {
      get { return _tab; }
      set { _tab = value; }
    }
    private readonly global::System.Collections.Generic.List<team.TeamMember> _players = new global::System.Collections.Generic.List<team.TeamMember>();
    [global::ProtoBuf.ProtoMember(2, Name=@"players", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<team.TeamMember> players
    {
      get { return _players; }
    }
  
    private readonly global::System.Collections.Generic.List<team.TeamBrief> _teams = new global::System.Collections.Generic.List<team.TeamBrief>();
    [global::ProtoBuf.ProtoMember(3, Name=@"teams", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<team.TeamBrief> teams
    {
      get { return _teams; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TeamList")]
  public partial class TeamList : global::ProtoBuf.IExtensible
  {
    public TeamList() {}
    
    private readonly global::System.Collections.Generic.List<team.TeamBrief> _teams = new global::System.Collections.Generic.List<team.TeamBrief>();
    [global::ProtoBuf.ProtoMember(1, Name=@"teams", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<team.TeamBrief> teams
    {
      get { return _teams; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetTeamInfoResponse")]
  public partial class GetTeamInfoResponse : global::ProtoBuf.IExtensible
  {
    public GetTeamInfoResponse() {}
    

    private team.TeamInfo _myTeam = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"myTeam", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public team.TeamInfo myTeam
    {
      get { return _myTeam; }
      set { _myTeam = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"JoinTeamResponse")]
  public partial class JoinTeamResponse : global::ProtoBuf.IExtensible
  {
    public JoinTeamResponse() {}
    
    private long _teamId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"teamId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long teamId
    {
      get { return _teamId; }
      set { _teamId = value; }
    }
    private team.TeamMember _joiner;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"joiner", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public team.TeamMember joiner
    {
      get { return _joiner; }
      set { _joiner = value; }
    }

    private long? _leaderId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"leaderId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long leaderId
    {
      get { return _leaderId?? default(long); }
      set { _leaderId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool leaderIdSpecified
    {
      get { return _leaderId != null; }
      set { if (value == (_leaderId== null)) _leaderId = value ? leaderId : (long?)null; }
    }
    private bool ShouldSerializeleaderId() { return leaderIdSpecified; }
    private void ResetleaderId() { leaderIdSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LeaveTeamResponse")]
  public partial class LeaveTeamResponse : global::ProtoBuf.IExtensible
  {
    public LeaveTeamResponse() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private long _leaderId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"leaderId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long leaderId
    {
      get { return _leaderId; }
      set { _leaderId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ApplyTeamRequest")]
  public partial class ApplyTeamRequest : global::ProtoBuf.IExtensible
  {
    public ApplyTeamRequest() {}
    
    private long _teamId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"teamId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long teamId
    {
      get { return _teamId; }
      set { _teamId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ConfirmTeamApplyRequest")]
  public partial class ConfirmTeamApplyRequest : global::ProtoBuf.IExtensible
  {
    public ConfirmTeamApplyRequest() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private team.ConfirmTeamApplyType _confirm;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"confirm", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public team.ConfirmTeamApplyType confirm
    {
      get { return _confirm; }
      set { _confirm = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"InviteTeamRequest")]
  public partial class InviteTeamRequest : global::ProtoBuf.IExtensible
  {
    public InviteTeamRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"InviteTeamMsg")]
  public partial class InviteTeamMsg : global::ProtoBuf.IExtensible
  {
    public InviteTeamMsg() {}
    
    private long _teamId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"teamId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long teamId
    {
      get { return _teamId; }
      set { _teamId = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private long _inviter;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"inviter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long inviter
    {
      get { return _inviter; }
      set { _inviter = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ConfirmTeamInviteRequest")]
  public partial class ConfirmTeamInviteRequest : global::ProtoBuf.IExtensible
  {
    public ConfirmTeamInviteRequest() {}
    
    private long _teamId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"teamId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long teamId
    {
      get { return _teamId; }
      set { _teamId = value; }
    }
    private team.ConfirmTeamApplyType _confirm;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"confirm", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public team.ConfirmTeamApplyType confirm
    {
      get { return _confirm; }
      set { _confirm = value; }
    }
    private long _inviter;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"inviter", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long inviter
    {
      get { return _inviter; }
      set { _inviter = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChangeTeamLeaderRequest")]
  public partial class ChangeTeamLeaderRequest : global::ProtoBuf.IExtensible
  {
    public ChangeTeamLeaderRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TeamLeaderChanged")]
  public partial class TeamLeaderChanged : global::ProtoBuf.IExtensible
  {
    public TeamLeaderChanged() {}
    
    private long _newLeaderId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"newLeaderId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long newLeaderId
    {
      get { return _newLeaderId; }
      set { _newLeaderId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LeaveTeamRequest")]
  public partial class LeaveTeamRequest : global::ProtoBuf.IExtensible
  {
    public LeaveTeamRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SetTeamModeRequest")]
  public partial class SetTeamModeRequest : global::ProtoBuf.IExtensible
  {
    public SetTeamModeRequest() {}
    
    private int _teamMode;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"teamMode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int teamMode
    {
      get { return _teamMode; }
      set { _teamMode = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CallBack")]
  public partial class CallBack : global::ProtoBuf.IExtensible
  {
    public CallBack() {}
    
    private long _caller;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"caller", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long caller
    {
      get { return _caller; }
      set { _caller = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private string _mapName;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"mapName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string mapName
    {
      get { return _mapName; }
      set { _mapName = value; }
    }
    private long _time;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long time
    {
      get { return _time; }
      set { _time = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CallBackInfo")]
  public partial class CallBackInfo : global::ProtoBuf.IExtensible
  {
    public CallBackInfo() {}
    
    private long _caller;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"caller", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long caller
    {
      get { return _caller; }
      set { _caller = value; }
    }
    private bool _agree;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"agree", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool agree
    {
      get { return _agree; }
      set { _agree = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"TeamTab")]
    public enum TeamTab
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"RoundPlayers", Value=1)]
      RoundPlayers = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Friends", Value=2)]
      Friends = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Union", Value=3)]
      Union = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Applies", Value=4)]
      Applies = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RoundTeams", Value=5)]
      RoundTeams = 5
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ConfirmTeamApplyType")]
    public enum ConfirmTeamApplyType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"Accept", Value=1)]
      Accept = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Refuse", Value=2)]
      Refuse = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Shield", Value=3)]
      Shield = 3
    }
  
}