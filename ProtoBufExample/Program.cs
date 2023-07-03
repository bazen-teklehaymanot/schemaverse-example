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