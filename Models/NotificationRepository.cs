using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace NotificationHub.Models
{
	public class NotificationRepository : INotificationRepository
	{
		private NotificationContext _context;
		public NotificationRepository(NotificationContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Notification>> GetNotifications(string userId)
		{
			var notifications = await _context.Users.Where(u => u.UserId == userId).Include("Notifications").SelectMany(u => u.Notifications).ToListAsync();
			return notifications;
		}

		// public Notification CreateNotification()
		// {

		// }
	}
}