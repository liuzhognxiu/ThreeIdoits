//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Mounts.proto
namespace mounts
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MountsBagInfo")]
  public partial class MountsBagInfo : global::ProtoBuf.IExtensible
  {
    public MountsBagInfo() {}
    
    private readonly global::System.Collections.Generic.List<mounts.MountsInfo> _mountsInfo = new global::System.Collections.Generic.List<mounts.MountsInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"mountsInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<mounts.MountsInfo> mountsInfo
    {
      get { return _mountsInfo; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MountsInfo")]
  public partial class MountsInfo : global::ProtoBuf.IExtensible
  {
    public MountsInfo() {}
    

    private int? _mountsId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mountsId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mountsId
    {
      get { return _mountsId?? default(int); }
      set { _mountsId = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool mountsIdSpecified
    {
      get { return _mountsId != null; }
      set { if (value == (_mountsId== null)) _mountsId = value ? mountsId : (int?)null; }
    }
    private bool ShouldSerializemountsId() { return mountsIdSpecified; }
    private void ResetmountsId() { mountsIdSpecified = false; }
    

    private int? _starLv;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"starLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int starLv
    {
      get { return _starLv?? default(int); }
      set { _starLv = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool starLvSpecified
    {
      get { return _starLv != null; }
      set { if (value == (_starLv== null)) _starLv = value ? starLv : (int?)null; }
    }
    private bool ShouldSerializestarLv() { return starLvSpecified; }
    private void ResetstarLv() { starLvSpecified = false; }
    

    private long? _getTime;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"getTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long getTime
    {
      get { return _getTime?? default(long); }
      set { _getTime = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool getTimeSpecified
    {
      get { return _getTime != null; }
      set { if (value == (_getTime== null)) _getTime = value ? getTime : (long?)null; }
    }
    private bool ShouldSerializegetTime() { return getTimeSpecified; }
    private void ResetgetTime() { getTimeSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UpgradeMountRequest")]
  public partial class UpgradeMountRequest : global::ProtoBuf.IExtensible
  {
    public UpgradeMountRequest() {}
    
    private int _mountsId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"mountsId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mountsId
    {
      get { return _mountsId; }
      set { _mountsId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChangeMountMsg")]
  public partial class ChangeMountMsg : global::ProtoBuf.IExtensible
  {
    public ChangeMountMsg() {}
    
    private int _mountsId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"mountsId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int mountsId
    {
      get { return _mountsId; }
      set { _mountsId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}