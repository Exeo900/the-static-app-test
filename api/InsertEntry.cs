using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class InsertEntry
    {
        // Visit https://aka.ms/sqlbindingsoutput to learn how to use this output binding
        [FunctionName("InsertEntry")]
        public static async Task<CreatedResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Sql("[dbo].[Entry]", "SqlConnectionString")] IAsyncCollector<Entry> output,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger with SQL Output Binding function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation("requestBody: " + requestBody);

            Entry entry = JsonConvert.DeserializeObject<Entry>(requestBody);

            await output.AddAsync(entry);

            return new CreatedResult(req.Path, entry);
        }
    }

    public class Entry
    {
        public string Message { get; set; }
    }
}
