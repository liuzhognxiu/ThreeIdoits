//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: RewardActivity.proto
// Note: requires additional types generated from: Bag.proto
namespace rewardActivity
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PrayInfo")]
  public partial class PrayInfo : global::ProtoBuf.IExtensible
  {
    public PrayInfo() {}
    
    private int _prayGoldCount;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"prayGoldCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int prayGoldCount
    {
      get { return _prayGoldCount; }
      set { _prayGoldCount = value; }
    }
    private int _prayLiquanCount;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"prayLiquanCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int prayLiquanCount
    {
      get { return _prayLiquanCount; }
      set { _prayLiquanCount = value; }
    }
    private int _todayGetGold;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"todayGetGold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int todayGetGold
    {
      get { return _todayGetGold; }
      set { _todayGetGold = value; }
    }
    private int _todayGetLiquan;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"todayGetLiquan", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int todayGetLiquan
    {
      get { return _todayGetLiquan; }
      set { _todayGetLiquan = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EverydaySignInfo")]
  public partial class EverydaySignInfo : global::ProtoBuf.IExtensible
  {
    public EverydaySignInfo() {}
    

    private int? _month;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"month", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int month
    {
      get { return _month?? default(int); }
      set { _month = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool monthSpecified
    {
      get { return _month != null; }
      set { if (value == (_month== null)) _month = value ? month : (int?)null; }
    }
    private bool ShouldSerializemonth() { return monthSpecified; }
    private void Resetmonth() { monthSpecified = false; }
    
    private int _signNum;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"signNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int signNum
    {
      get { return _signNum; }
      set { _signNum = value; }
    }

    private bag.BagItemInfo _item = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"item", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public bag.BagItemInfo item
    {
      get { return _item; }
      set { _item = value; }
    }
    private bool _isGet;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"isGet", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isGet
    {
      get { return _isGet; }
      set { _isGet = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PrayRequest")]
  public partial class PrayRequest : global::ProtoBuf.IExtensible
  {
    public PrayRequest() {}
    
    private int _type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private bool _useYuanbao;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"useYuanbao", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool useYuanbao
    {
      get { return _useYuanbao; }
      set { _useYuanbao = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PrayResponse")]
  public partial class PrayResponse : global::ProtoBuf.IExtensible
  {
    public PrayResponse() {}
    
    private rewardActivity.PrayInfo _pray;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"pray", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public rewardActivity.PrayInfo pray
    {
      get { return _pray; }
      set { _pray = value; }
    }
    private bool _double;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"double", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool @double
    {
      get { return _double; }
      set { _double = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ProstrateInfo")]
  public partial class ProstrateInfo : global::ProtoBuf.IExtensible
  {
    public ProstrateInfo() {}
    

    private long? _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
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
    

    private int? _disdain;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"disdain", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int disdain
    {
      get { return _disdain?? default(int); }
      set { _disdain = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool disdainSpecified
    {
      get { return _disdain != null; }
      set { if (value == (_disdain== null)) _disdain = value ? disdain : (int?)null; }
    }
    private bool ShouldSerializedisdain() { return disdainSpecified; }
    private void Resetdisdain() { disdainSpecified = false; }
    

    private int? _prostrate;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"prostrate", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int prostrate
    {
      get { return _prostrate?? default(int); }
      set { _prostrate = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool prostrateSpecified
    {
      get { return _prostrate != null; }
      set { if (value == (_prostrate== null)) _prostrate = value ? prostrate : (int?)null; }
    }
    private bool ShouldSerializeprostrate() { return prostrateSpecified; }
    private void Resetprostrate() { prostrateSpecified = false; }
    
    private int _everydayUsedNumber;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"everydayUsedNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int everydayUsedNumber
    {
      get { return _everydayUsedNumber; }
      set { _everydayUsedNumber = value; }
    }
    private int _times;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"times", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int times
    {
      get { return _times; }
      set { _times = value; }
    }

    private string _name;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name?? ""; }
      set { _name = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool nameSpecified
    {
      get { return _name != null; }
      set { if (value == (_name== null)) _name = value ? name : (string)null; }
    }
    private bool ShouldSerializename() { return nameSpecified; }
    private void Resetname() { nameSpecified = false; }
    

    private int? _cardType;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"cardType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int cardType
    {
      get { return _cardType?? default(int); }
      set { _cardType = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool cardTypeSpecified
    {
      get { return _cardType != null; }
      set { if (value == (_cardType== null)) _cardType = value ? cardType : (int?)null; }
    }
    private bool ShouldSerializecardType() { return cardTypeSpecified; }
    private void ResetcardType() { cardTypeSpecified = false; }
    

    private int? _yuanbao;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"yuanbao", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int yuanbao
    {
      get { return _yuanbao?? default(int); }
      set { _yuanbao = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool yuanbaoSpecified
    {
      get { return _yuanbao != null; }
      set { if (value == (_yuanbao== null)) _yuanbao = value ? yuanbao : (int?)null; }
    }
    private bool ShouldSerializeyuanbao() { return yuanbaoSpecified; }
    private void Resetyuanbao() { yuanbaoSpecified = false; }
    

    private bool? _dayReward;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"dayReward", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool dayReward
    {
      get { return _dayReward?? default(bool); }
      set { _dayReward = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool dayRewardSpecified
    {
      get { return _dayReward != null; }
      set { if (value == (_dayReward== null)) _dayReward = value ? dayReward : (bool?)null; }
    }
    private bool ShouldSerializedayReward() { return dayRewardSpecified; }
    private void ResetdayReward() { dayRewardSpecified = false; }
    

    private string _unionName;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"unionName", DataFormat = global::ProtoBuf.DataFormat.Default)]
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
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ProstrateRequest")]
  public partial class ProstrateRequest : global::ProtoBuf.IExtensible
  {
    public ProstrateRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UpdateProstrateRequest")]
  public partial class UpdateProstrateRequest : global::ProtoBuf.IExtensible
  {
    public UpdateProstrateRequest() {}
    
    private bool _useYuanbao;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"useYuanbao", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool useYuanbao
    {
      get { return _useYuanbao; }
      set { _useYuanbao = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UpdateProstrateResponse")]
  public partial class UpdateProstrateResponse : global::ProtoBuf.IExtensible
  {
    public UpdateProstrateResponse() {}
    
    private int _times;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"times", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int times
    {
      get { return _times; }
      set { _times = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnlineRewardMsg")]
  public partial class OnlineRewardMsg : global::ProtoBuf.IExtensible
  {
    public OnlineRewardMsg() {}
    

    private long? _time;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
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
    
    private readonly global::System.Collections.Generic.List<long> _reward = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(2, Name=@"reward", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> reward
    {
      get { return _reward; }
    }
  

    private int? _lastLiquan;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"lastLiquan", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int lastLiquan
    {
      get { return _lastLiquan?? default(int); }
      set { _lastLiquan = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool lastLiquanSpecified
    {
      get { return _lastLiquan != null; }
      set { if (value == (_lastLiquan== null)) _lastLiquan = value ? lastLiquan : (int?)null; }
    }
    private bool ShouldSerializelastLiquan() { return lastLiquanSpecified; }
    private void ResetlastLiquan() { lastLiquanSpecified = false; }
    

    private int? _theWeekLiquan;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"theWeekLiquan", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int theWeekLiquan
    {
      get { return _theWeekLiquan?? default(int); }
      set { _theWeekLiquan = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool theWeekLiquanSpecified
    {
      get { return _theWeekLiquan != null; }
      set { if (value == (_theWeekLiquan== null)) _theWeekLiquan = value ? theWeekLiquan : (int?)null; }
    }
    private bool ShouldSerializetheWeekLiquan() { return theWeekLiquanSpecified; }
    private void ResettheWeekLiquan() { theWeekLiquanSpecified = false; }
    

    private bool? _isDraw;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"isDraw", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isDraw
    {
      get { return _isDraw?? default(bool); }
      set { _isDraw = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool isDrawSpecified
    {
      get { return _isDraw != null; }
      set { if (value == (_isDraw== null)) _isDraw = value ? isDraw : (bool?)null; }
    }
    private bool ShouldSerializeisDraw() { return isDrawSpecified; }
    private void ResetisDraw() { isDrawSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnlineRewardRequest")]
  public partial class OnlineRewardRequest : global::ProtoBuf.IExtensible
  {
    public OnlineRewardRequest() {}
    
    private readonly global::System.Collections.Generic.List<int> _index = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(1, Name=@"index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<int> index
    {
      get { return _index; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EverydaySignRequest")]
  public partial class EverydaySignRequest : global::ProtoBuf.IExtensible
  {
    public EverydaySignRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ExpBeadMsg")]
  public partial class ExpBeadMsg : global::ProtoBuf.IExtensible
  {
    public ExpBeadMsg() {}
    

    private int? _drawNum;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"drawNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int drawNum
    {
      get { return _drawNum?? default(int); }
      set { _drawNum = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool drawNumSpecified
    {
      get { return _drawNum != null; }
      set { if (value == (_drawNum== null)) _drawNum = value ? drawNum : (int?)null; }
    }
    private bool ShouldSerializedrawNum() { return drawNumSpecified; }
    private void ResetdrawNum() { drawNumSpecified = false; }
    
    private long _exp;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"exp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long exp
    {
      get { return _exp; }
      set { _exp = value; }
    }

    private int? _doubleNum;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"doubleNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int doubleNum
    {
      get { return _doubleNum?? default(int); }
      set { _doubleNum = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool doubleNumSpecified
    {
      get { return _doubleNum != null; }
      set { if (value == (_doubleNum== null)) _doubleNum = value ? doubleNum : (int?)null; }
    }
    private bool ShouldSerializedoubleNum() { return doubleNumSpecified; }
    private void ResetdoubleNum() { doubleNumSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ExpBeadRequest")]
  public partial class ExpBeadRequest : global::ProtoBuf.IExtensible
  {
    public ExpBeadRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SevenDaysInfo")]
  public partial class SevenDaysInfo : global::ProtoBuf.IExtensible
  {
    public SevenDaysInfo() {}
    

    private int? _loginDay;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"loginDay", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int loginDay
    {
      get { return _loginDay?? default(int); }
      set { _loginDay = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool loginDaySpecified
    {
      get { return _loginDay != null; }
      set { if (value == (_loginDay== null)) _loginDay = value ? loginDay : (int?)null; }
    }
    private bool ShouldSerializeloginDay() { return loginDaySpecified; }
    private void ResetloginDay() { loginDaySpecified = false; }
    
    private readonly global::System.Collections.Generic.List<long> _sevenDay = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(2, Name=@"sevenDay", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> sevenDay
    {
      get { return _sevenDay; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SevenDaysRequest")]
  public partial class SevenDaysRequest : global::ProtoBuf.IExtensible
  {
    public SevenDaysRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResRetrieveMsg")]
  public partial class ResRetrieveMsg : global::ProtoBuf.IExtensible
  {
    public ResRetrieveMsg() {}
    
    private readonly global::System.Collections.Generic.List<long> _resData = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"resData", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> resData
    {
      get { return _resData; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResRetrieveRequest")]
  public partial class ResRetrieveRequest : global::ProtoBuf.IExtensible
  {
    public ResRetrieveRequest() {}
    
    private int _resId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"resId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int resId
    {
      get { return _resId; }
      set { _resId = value; }
    }
    private int _clickType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"clickType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int clickType
    {
      get { return _clickType; }
      set { _clickType = value; }
    }
    private int _count;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
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