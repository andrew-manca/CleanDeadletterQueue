//using System;
//using System.IO;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Azure.ServiceBus;
//using Microsoft.Azure.ServiceBus.Core;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Host;
//using Microsoft.Extensions.Logging;


//namespace FunctionApp6
//{

//    public static class Function4
//    {

//        const string ServiceBusConnectionString = "ConnectionString";
//        const string QueueName = "QueueName";
//        const string QueueNameDeadletter = "QueueName/$deadletterqueue";
//        static IQueueClient queueClient;
//        static IQueueClient deadLetterQueueClient;
//        const int MaximumDeliveryCount = 11;

//        [FunctionName("Function4")]
//        public static async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer, ILogger log)
//        {
//            try 
//            { 
//            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
//            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
//            deadLetterQueueClient = new QueueClient(ServiceBusConnectionString, QueueNameDeadletter);
//            log.LogInformation($"message");
//            var receiver = new MessageReceiver(ServiceBusConnectionString, QueueNameDeadletter, ReceiveMode.ReceiveAndDelete);

//            var messages = await receiver.ReceiveAsync(30);
//            for (int i = 0; i < messages.Count; i++)
//            {
//                log.LogInformation("sending message " + i);
//                //Only send messages that have a delivery count less than the count set
//                if (messages[i].SystemProperties.DeliveryCount < MaximumDeliveryCount)
//                {
//                    var message = Encoding.UTF8.GetString(messages[i].Body);
//                    var newMessage = new Message(Encoding.UTF8.GetBytes(message));
//                    await queueClient.SendAsync(newMessage);
//                }

//            }
//            await queueClient.CloseAsync();
//            await deadLetterQueueClient.CloseAsync();
//            log.LogInformation("Finished");
//            }
//            catch (Exception ex)
//            {
//                log.LogInformation($"Exception: {ex}");
//            }

//        }

//    }
//}