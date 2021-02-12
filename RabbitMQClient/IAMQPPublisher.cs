namespace InstaGenerator.RabbitMQClient
{
    public interface IAMQPPublisher
    {
        public void Publish(string message);

        public string RabbitMQHost { get; set; }

        public string PublishQueueName { get; set; }
        
    }
}