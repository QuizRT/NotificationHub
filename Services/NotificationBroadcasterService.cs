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
			try 
			{
				Console.WriteLine("Broadcasting Notifications");
				var connectedClients = notification.Users.Select(userId => NotificationHub.ConnectedClients.FirstOrDefault(x => x.Value == userId).Key).ToList();
				foreach(var client in connectedClients)
				{
					Console.WriteLine(client);
				}
				Console.WriteLine(connectedClients.Count);
				_notificationHubContext.Clients.Clients(connectedClients).SendAsync("notification", notification);
			}
			catch (Exception e) 
			{
				Console.WriteLine("Error in Broadcaster");
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
