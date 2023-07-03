using Memphis.Client.Producer;
using System.Collections.Specialized;
using Memphis.Client;
using ProtoBuf;

var options = MemphisClientFactory.GetDefaultOptions();
options.Host = "<memphis-host>";
options.Username = "<application type username>";
options.ConnectionToken = "<broker-token>";

/**
* In case you are using Memphis.dev cloud
* options.AccountId = "<account-id>";
*/

try
{
    var client = await MemphisClientFactory.CreateClient(options);

    var producer = await client.CreateProducer(new MemphisProducerOptions
    {
        StationName = "<memphis-station-name>",
        ProducerName = "<memphis-prodcducer-name>",
        GenerateUniqueSuffix = true
    });

    NameValueCollection commonHeaders = new()
    {
        {
            "key-1", "value-1"
        }
    };

    Test test = new()
    {
        Field1 = "Hello",
        Field2 = "Amazing",
        Field3 = 32
    };
    using var memoryStream = new MemoryStream();
    Serializer.Serialize(memoryStream, test);
    var message = memoryStream.ToArray();

    await producer.ProduceAsync(message, commonHeaders);
    client.Dispose();
}
catch (Exception exception)
{
    Console.WriteLine($"Error occured: {exception.Message}");
}

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