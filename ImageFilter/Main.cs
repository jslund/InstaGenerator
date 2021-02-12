using System;
using InstaGenerator.ImageFilter.Helpers;

namespace InstaGenerator.ImageFilter
{
    public class ImageFilterWorker
    {
        public static void Main()
        {
            var rabbitMQHost = "host.docker.internal";
            var consumerQueueName = "instagram_pictures";
            
            var consumer = new FilterRabbitMQConsumer(rabbitMQHost, consumerQueueName);
            consumer.Consume();

            Console.WriteLine("Beginning APp");
            Console.ReadLine();
        }
    }
}