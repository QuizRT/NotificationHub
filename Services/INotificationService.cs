using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NotificationEngine.Models;

namespace NotificationEngine.Services
{
	public interface ICreateNotificationService
	{
		Task CreateNotification(Notification notification);
	}

	public interface IReadNotificationService
	{
		Task<List<UserNotification>> GetNotifications(string UserId);

	}
}