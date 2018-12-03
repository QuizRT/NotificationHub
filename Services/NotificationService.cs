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

        public Task<Notification> CreateNotification(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}