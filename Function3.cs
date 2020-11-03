using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp6
{
    public static class Function3
    {
        //This is a test function to force messages to the DLQ
        [FunctionName("Function3")]
        public static void Run([ServiceBusTrigger("%myqueuereal%", Connection = "ServiceBusConnectionString")] string myQueueItem, ILogger log)
        {

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            throw new Exception();
        }
    }
}
