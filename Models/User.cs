using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotificationEngine.Models
{
	public class User
	{
		[Key]
		public string UserId { get; set; }
		public List<UserNotification> Notifications { get; set; }
	}
}