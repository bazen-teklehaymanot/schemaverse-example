using System.Collections.Specialized;
using Memphis.Client;
using Memphis.Client.Producer;
using System.Text;


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

    string graphqlMsg = @"query myQuery { greeting } mutation msg { updateUserEmail(email: ""http://github.com"", id: 1) { id name } }";

    await producer.ProduceAsync(Encoding.UTF8.GetBytes(graphqlMsg), commonHeaders);
    client.Dispose();
}
catch (Exception exception)
{
    Console.WriteLine($"Error occured: {exception.Message}");
}