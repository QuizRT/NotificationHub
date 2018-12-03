using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using NotificationEngine.Services;
using NotificationEngine.Models;

namespace NotificationEngine.Services
{
	public class NotificationConsumerService
	{
		private NotificationBroadcaster _broadcaster;
		public NotificationConsumerService(NotificationBroadcaster broadcaster, ICreateNotificationService notificationService)
		{
			Console.WriteLine("Consumer Service");
			_broadcaster = broadcaster;
			this.Consume();
		}

		public void Consume()
		{
			var factory = new ConnectionFactory()
			{
				HostName = "rabbitmq",
				Port = 5672,
				UserName = "rabbitmq",
				Password = "rabbitmq"
			};
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
				queue: "Notification",
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null
			);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var notificationMessageAsJson = Encoding.UTF8.GetString(body);
				var notification = Notification.ToObject(notificationMessageAsJson);
                Console.WriteLine(notificationMessageAsJson);
                Console.WriteLine(" [x] Received {0}", notificationMessageAsJson);

				_broadcaster.BroadcastNotifications(notification);
            };

            channel.BasicConsume(queue: "Notification", autoAck: true, consumer: consumer);
		}
	}
}