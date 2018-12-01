using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotificationHub.Models
{
	public class User
	{
		[Key]
		public string UserId { get; set; }
		public List<Notification> Notifications { get; set; }
	}
}