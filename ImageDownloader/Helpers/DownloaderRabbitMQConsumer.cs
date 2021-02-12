using System;
using System.Net;
using System.Net.Http;
using System.Text;
using InstaGenerator.ImageFilter.Interfaces;
using InstaGenerator.RabbitMQClient;
using RabbitMQ.Client.Events;

namespace ImageDownloader.Helpers
{
    public class DownloaderRabbitMQConsumer : AbstractRabbitMQConsumer
    {

        private WebClient _webClient;
        
        protected override void Callback(BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received {message}");

            using (IRemoteImage remoteImage = new RemoteImage(message))
            {
                remoteImage.Download(_webClient);
                remoteImage.Resize();
            }

            Console.WriteLine("break");
            
        }

        public DownloaderRabbitMQConsumer(string rabbitMQHost, string consumerQueueName) : base(rabbitMQHost,
            consumerQueueName)
        {
            _webClient = new WebClient();
        }
    }
}