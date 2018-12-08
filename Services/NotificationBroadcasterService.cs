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

		public async Task BroadcastNotifications(Notification notification)
		{
			try 
			{
				Console.WriteLine("Broadcasting Notifications");
				var connectedClients = notification.Users.SelectMany(userId => NotificationHub.ConnectedClients.Where(x => x.Value == userId).Select(x => x.Key)).ToList();
				foreach(var client in connectedClients)
				{
					Console.WriteLine(client);
				}
				Console.WriteLine(connectedClients.Count);
				await _notificationHubContext.Clients.All.SendAsync("notification", new { Message = notification.Message, TargetUrl = notification.TargetUrl });
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
