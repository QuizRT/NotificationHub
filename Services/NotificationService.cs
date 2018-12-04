using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using NotificationEngine.Models;

namespace NotificationEngine.Services
{
	public class NotificationService : ICreateNotificationService, IReadNotificationService
	{
		private NotificationContext _context;
		public NotificationService(NotificationContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<UserNotification>> GetNotifications(string userId)
		{
			var notifications = await _context.Users.Where(u => u.UserId == userId).Include("Notifications").SelectMany(u => u.Notifications).ToListAsync();
			return notifications;
		}

        public async Task CreateNotification(Notification notification)
        {
			Console.WriteLine("Notification Saved");
            foreach (var user in notification.Users)
			{
				var userNotification = new UserNotification()
				{
					Notification = notification,
					UserId = user,
					HasRead = false,
				};
				if (!await _context.Users.ContainsAsync(new User() { UserId = user }))
				{
					_context.Users.Add(new User() { UserId = user, Notifications = new List<UserNotification>() { userNotification } });
				}
				else
				{
					_context.UserNotifications.Add(userNotification);
				}
			}
			await _context.SaveChangesAsync();
			Console.WriteLine("Notification Saved");
        }
    }
}