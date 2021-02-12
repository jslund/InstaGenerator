using System;
using System.Text;
using InstaGenerator.RabbitMQClient;
using RabbitMQ.Client.Events;
using GoogleCloudClients;

namespace InstaGenerator.ImageFilter.Helpers
{
    public class FilterRabbitMQConsumer : AbstractRabbitMQConsumer
    {

        private RabbitMQPublisher _publisher;
        protected override void Callback(BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Starting {message}");

            var client = new GoogleVisionClient(message);
            Console.WriteLine("Created GoogleClient");
                client.FetchTags();
                if (client.Validate("Dog"))
                {
                    _publisher.Publish(message);
                    Console.WriteLine("Accepted");
                }
                else
                {
                    Console.WriteLine("Rejected");
                }
            


        }

        public FilterRabbitMQConsumer(string rabbitMQHost, string consumerQueueName) : base(rabbitMQHost,
            consumerQueueName)
        {
            _publisher = new RabbitMQPublisher(rabbitMQHost, "valid_images");
        }

    }
}