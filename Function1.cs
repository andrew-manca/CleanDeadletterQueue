//using System;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
//using Microsoft.Extensions.Logging;

//namespace FunctionApp6
//{
//    public static class Function1
//    {

//        //This will re-add the message to the queue using a Service Bus trigger on the deadletter queue
//     [FunctionName("Function1")]
//     [return: ServiceBus("%myqueuereal%", Connection = "serviceBusConnectionString")]
//        public static string Run([ServiceBusTrigger("%myqueue%", Connection = "serviceBusConnectionString")] string myQueueItem, ILogger log)
//    {
//        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

//        return myQueueItem;
//    }
//}
//}
