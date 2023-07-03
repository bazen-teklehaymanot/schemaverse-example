namespace ProtoBufExample;

[ProtoContract]
class Test
{
    [ProtoMember(1, Name = "field1")]
    public required string Field1 { get; set; }
    [ProtoMember(2, Name = "field2")]
    public required string Field2 { get; set; }
    [ProtoMember(3, Name = "field3")]
    public int Field3 { get; set; }
}