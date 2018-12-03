using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

using NotificationEngine.Models;
using NotificationEngine.Services;

namespace NotificationEngine.Hubs
{
    public class NotificationHub : Hub
    {
		public static Dictionary<string, string> ConnectedClients = new Dictionary<string, string>();

		private IReadNotificationService _notificationService;

		public NotificationHub(IReadNotificationService notificationService)
        {
			_notificationService = notificationService;
        }

		public void Init(string userId)
		{
			ConnectedClients.Add(userId, Context.ConnectionId);
		}

		public async Task GetNotifications()
		{
			var userId = NotificationHub.ConnectedClients.GetValueOrDefault(Context.ConnectionId);
			var notifications = await _notificationService.GetNotifications(userId);
			await Clients.Caller.SendAsync("Notifications", notifications);
		}
    }
}