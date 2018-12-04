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
		public async Task<List<UserNotification>> GetNotifications(string userId)
		{

			var userNotifications = await _context.UserNotifications.Where(u => u.UserId == userId).Include("Notifications").ToListAsync();
			return userNotifications;
		}

        public async Task CreateNotification(Notification notification)
        {
			var userNotifications = notification.Users.Select((u) => new UserNotification() { UserId = u, HasRead = false });
			notification.UserNotifications.AddRange(userNotifications);
			await _context.Notifications.AddAsync(notification);
			await _context.SaveChangesAsync();
			Console.WriteLine("Notification Saved");
        }
    }
}