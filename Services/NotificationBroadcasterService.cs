using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

using NotificationEngine.Hubs;
using NotificationEngine.Models;
using RabbitMQ.Client;
using System.Text;

namespace NotificationEngine.Services
{
	public class NotificationBroadcaster
	{
		private IHubContext<NotificationHub> _notificationHubContext;
		public NotificationBroadcaster(IHubContext<NotificationHub> notificationHubContext)
		{
			_notificationHubContext = notificationHubContext;
		}

		public void BroadcastNotifications(Notification notification)
		{
			Console.WriteLine("Broadcasting Notifications");
			var connectedClients = notification.Users.Select(userId => NotificationHub.ConnectedClients.GetValueOrDefault(userId, "")).Where(u => u != "").ToList();
			_notificationHubContext.Clients.Clients(connectedClients).SendAsync("notification", notification);
		}
	}
}
