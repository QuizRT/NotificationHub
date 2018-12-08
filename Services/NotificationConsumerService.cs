using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using NotificationEngine.Services;
using NotificationEngine.Models;

namespace NotificationEngine.Services
{
	public class NotificationConsumerService
	{
		private IServiceProvider _serviceProvider;
		private NotificationBroadcaster _broadcaster;

		public NotificationConsumerService(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Consumer Service");
			_serviceProvider = serviceProvider;
			this.Consume();
		}

		public void Consume()
		{
			var factory = new ConnectionFactory()
			{
				HostName = "rabbitmq",
				Port = 5672,
				UserName = "rabbitmq",
				Password = "rabbitmq",
				DispatchConsumersAsync = true
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
            consumer.Received += async (model, ea) =>
            {
				Console.WriteLine("Consumed");
                var body = ea.Body;
                var notificationMessageAsJson = Encoding.UTF8.GetString(body);
				using (var serviceScope = this._serviceProvider.CreateScope())
				{
					try 
					{
						Notification notification = Notification.ToObject(notificationMessageAsJson);
						Console.WriteLine(notificationMessageAsJson);
						// var notification = JsonConvert.DeserializeObject<Notification>(notificationMessageAsJson);
						Console.WriteLine(notificationMessageAsJson);
						Console.WriteLine(" [x] Received {0}", notificationMessageAsJson);
						Console.WriteLine("notification", notification);
						Console.WriteLine(notification.Message);			
						var notificationService = serviceScope.ServiceProvider.GetRequiredService<ICreateNotificationService>();
						var broadcasterService = serviceScope.ServiceProvider.GetRequiredService<NotificationBroadcaster>();
						await notificationService.CreateNotification(notification);
						await broadcasterService.BroadcastNotifications(notification);
					}
					catch(Exception e)
					{
						Console.WriteLine("Error in Consumer Service");
						Console.WriteLine(e.StackTrace);
						Console.WriteLine(e.Message);
					}
				}
            };

            channel.BasicConsume(queue: "Notification", autoAck: true, consumer: consumer);
		}
	}
}