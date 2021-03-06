//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: Mail.proto
namespace mail
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MailInfo")]
  public partial class MailInfo : global::ProtoBuf.IExtensible
  {
    public MailInfo() {}
    
    private long _mailId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"mailId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long mailId
    {
      get { return _mailId; }
      set { _mailId = value; }
    }

    private string _title;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"title", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string title
    {
      get { return _title?? ""; }
      set { _title = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool titleSpecified
    {
      get { return _title != null; }
      set { if (value == (_title== null)) _title = value ? title : (string)null; }
    }
    private bool ShouldSerializetitle() { return titleSpecified; }
    private void Resettitle() { titleSpecified = false; }
    

    private string _content;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"content", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string content
    {
      get { return _content?? ""; }
      set { _content = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool contentSpecified
    {
      get { return _content != null; }
      set { if (value == (_content== null)) _content = value ? content : (string)null; }
    }
    private bool ShouldSerializecontent() { return contentSpecified; }
    private void Resetcontent() { contentSpecified = false; }
    
    private string _from;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"from", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string from
    {
      get { return _from; }
      set { _from = value; }
    }
    private long _sendTime;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"sendTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long sendTime
    {
      get { return _sendTime; }
      set { _sendTime = value; }
    }

    private string _items;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string items
    {
      get { return _items?? ""; }
      set { _items = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool itemsSpecified
    {
      get { return _items != null; }
      set { if (value == (_items== null)) _items = value ? items : (string)null; }
    }
    private bool ShouldSerializeitems() { return itemsSpecified; }
    private void Resetitems() { itemsSpecified = false; }
    
    private int _state;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"state", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int state
    {
      get { return _state; }
      set { _state = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MailList")]
  public partial class MailList : global::ProtoBuf.IExtensible
  {
    public MailList() {}
    
    private readonly global::System.Collections.Generic.List<mail.MailInfo> _mails = new global::System.Collections.Generic.List<mail.MailInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"mails", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<mail.MailInfo> mails
    {
      get { return _mails; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MailIdMsg")]
  public partial class MailIdMsg : global::ProtoBuf.IExtensible
  {
    public MailIdMsg() {}
    
    private readonly global::System.Collections.Generic.List<long> _mailIds = new global::System.Collections.Generic.List<long>();
    [global::ProtoBuf.ProtoMember(1, Name=@"mailIds", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<long> mailIds
    {
      get { return _mailIds; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}