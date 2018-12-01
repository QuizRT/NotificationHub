using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NotificationHub.Models
{
	public interface INotificationRepository
	{
		Task<IEnumerable<Notification>> GetNotifications(string UserId);
		Task<Notification> CreateNotification(Notification notification);
	}
}