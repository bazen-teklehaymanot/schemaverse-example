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
        ProducerName = "<memphis-producer-name>",
        GenerateUniqueSuffix = true
    });

    NameValueCollection commonHeaders = new()
    {
        {
            "key-1", "value-1"
        }
    };

    ContactDetail contactDetail = new()
    {
        FirstName = "Bob",
        LastName = "Marley"
    };

    string message = JsonConvert.SerializeObject(contactDetail);

    await producer.ProduceAsync(Encoding.UTF8.GetBytes(message), commonHeaders);
    client.Dispose();
}
catch (Exception exception)
{
    Console.WriteLine($"Error occured: {exception.Message}");
}



public class ContactDetail
{
    [JsonProperty("fname")]
    public required string FirstName { get; set; }
    [JsonProperty("lname")]
    public required string LastName { get; set; }
}
