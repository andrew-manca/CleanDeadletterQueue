using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;


namespace FunctionApp6
{

    public static class Function4
    {

        const string ServiceBusConnectionString = "Endpoint=sb://amancaservicebusdeadletter.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Xvf4TQe1xzD+rfctSOEuYLO6PbbEAXAz64Bw2VsyMTM=";
        const string QueueName = "amancaTestQueue";
        const string QueueNameDeadletter = "amancaTestQueue/$deadletterqueue";
        static IQueueClient queueClient;
        static IQueueClient deadLetterQueueClient;

        [FunctionName("Function4")]
        public static async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer, ILogger log)
        {
            try 
            { 
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            deadLetterQueueClient = new QueueClient(ServiceBusConnectionString, QueueNameDeadletter);
            log.LogInformation($"message");
            var receiver = new MessageReceiver(ServiceBusConnectionString, QueueNameDeadletter, ReceiveMode.ReceiveAndDelete);

            var messages = await receiver.ReceiveAsync(1);
            for (int i = 0; i < messages.Count; i++)
            {
                Console.WriteLine("sending message " + i);
                var message = Encoding.UTF8.GetString(messages[i].Body);
                var newMessage = new Message(Encoding.UTF8.GetBytes(message));
                await queueClient.SendAsync(newMessage);

            }
            await queueClient.CloseAsync();
            await deadLetterQueueClient.CloseAsync();
            Console.WriteLine("Finished");
            }
            catch (Exception ex)
            {
                log.LogInformation($"Exception: {ex}");
            }

        }

    }
}