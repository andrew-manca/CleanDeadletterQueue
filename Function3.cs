using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp6
{
    public static class Function3
    {
        // This is a test function to force messages to the DLQ
        // If you are using a Session Enabled Service Bus, please uncomment line of code for IsSessionEnabled
        [FunctionName("Function3")]
        public static void Run([ServiceBusTrigger("%myqueuereal%", Connection = "ServiceBusConnectionString"/*, IsSessionsEnabled =true*/)] string myQueueItem, ILogger log)
        {

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            throw new Exception();
        }
    }
}
