using Memphis.Client.Consumer;
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

    var consumer = await client.CreateConsumer(new MemphisConsumerOptions
    {
        StationName = "<station-name>",
        ConsumerName = "<consumer-name>",
        ConsumerGroup = "<consumer-group-name>",
    });

    consumer.MessageReceived += (sender, args) =>
    {
        if (args.Exception is not null)
        {
            Console.Error.WriteLine(args.Exception);
            return;
        }

        foreach (var msg in args.MessageList)
        {
            var data = msg.GetData();
            if (data is { Length: > 0 })
            {
                using var stream = new MemoryStream(data);
                var test = Serializer.Deserialize<Test>(stream);
                Console.WriteLine($"Field1: {test.Field1}");
                Console.WriteLine($"Field2: {test.Field2}");
                Console.WriteLine($"Field3: {test.Field3}");
            }
        }
    };

    consumer.ConsumeAsync();

    await Task.Delay(TimeSpan.FromMinutes(1));
    await consumer.DestroyAsync();
    client.Dispose();
}
catch (Exception exception)
{
    Console.WriteLine($"Error occured: {exception.Message}");
}