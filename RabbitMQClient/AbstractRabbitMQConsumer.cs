using System.Runtime.Loader;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InstaGenerator.RabbitMQClient
{
    public abstract class AbstractRabbitMQConsumer : IAMQPConsumer
    {

        protected abstract void Callback(BasicDeliverEventArgs ea);

        private IConnection _connection;
        
        public string RabbitMQHost { get; set; }

        public string ConsumerQueueName { get; set; }

        private ConnectionFactory _factory;

        private IModel _channel;

        
        public void Consume()
        {
            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();
            
                _channel.QueueDeclare(queue: ConsumerQueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                
                _channel.BasicQos(prefetchSize:0, prefetchCount:1, global: false);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (sender, ea) =>
                {
                    Callback(ea);
                    _channel.BasicAck(deliveryTag:ea.DeliveryTag, multiple: false);
                };

                _channel.BasicConsume(queue: ConsumerQueueName,
                    autoAck: false,
                    consumer: consumer);
            

        }
        
        

        public AbstractRabbitMQConsumer(string rabbitMQHost, string consumerQueueName)
        {
            RabbitMQHost = rabbitMQHost;
            ConsumerQueueName = consumerQueueName;

            _factory = new ConnectionFactory() {HostName = rabbitMQHost};

        }
    }
}