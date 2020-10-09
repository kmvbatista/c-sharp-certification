using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

[DataContract]
class Artist 
{
  [DataMember]
  public string Name { get; set; }

  public Artist(string name)
  {
    Name = name;
  }
  protected Artist(SerializationInfo info, StreamingContext context)
  {
    Name = info.GetString("name");
  }

  protected Artist()
  {
  }

  [OnDeserialized()]
  internal void OnDeserializedMethod(StreamingContext context)
  {
    Name = "unknown";
  }

  [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("name", Name);
  }
}