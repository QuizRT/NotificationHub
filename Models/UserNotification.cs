using System;
using System.ComponentModel.DataAnnotations;

namespace NotificationEngine.Models
{
	public class UserNotification
	{
		[Key]
		public int Id { get; set; }
		public bool HasRead { get; set; }
		public string UserId { get; set; }
		public int NotificationId { get; set; }


		public Notification Notification { get; set; }
		public User User { get; set; }
	}
}