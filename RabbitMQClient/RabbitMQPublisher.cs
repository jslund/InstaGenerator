using System.Text;
using RabbitMQ.Client;

namespace InstaGenerator.RabbitMQClient
{
    public class RabbitMQPublisher : IAMQPPublisher
    {
        public string RabbitMQHost { get; set; }

        public string PublishQueueName { get; set; }

        private ConnectionFactory _factory;

        private IConnection _connection;

        private IModel _channel;

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange:"",
                    routingKey: PublishQueueName,
                    body: body);
                
        }

        public RabbitMQPublisher(string rabbitMQHost, string publishQueueName)
        {
            RabbitMQHost = rabbitMQHost;

            PublishQueueName = publishQueueName;

            _factory = new ConnectionFactory() {HostName = RabbitMQHost};
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: PublishQueueName, 
                 durable: false,
                 exclusive: false,
                 autoDelete: false, 
                 arguments: null);

        }
    }
}