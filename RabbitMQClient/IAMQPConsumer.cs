namespace InstaGenerator.RabbitMQClient
{
    public interface IAMQPConsumer
    {
        void Consume();

        public string RabbitMQHost { get; set; }

        public string ConsumerQueueName { get; set; }
        
        
    }
}