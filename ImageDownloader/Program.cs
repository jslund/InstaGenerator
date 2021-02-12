using System;
using ImageDownloader.Helpers;


namespace ImageDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitMQHost = "host.docker.internal";
            var consumerQueueName = "valid_images";

            var consumer = new DownloaderRabbitMQConsumer(rabbitMQHost, consumerQueueName);
            
            consumer.Consume();

            Console.WriteLine("Beginning Downloader App");
            Console.ReadLine();


        }
    }
}