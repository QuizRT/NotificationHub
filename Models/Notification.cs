using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotificationHub.Models
{
	public class Notification
	{
		[Key]
		public int NotificationId { get; set; }
		public string UserId { get; set; }
		public string Message { get; set; }
		public string TargetUrl { get; set; }
		public bool IsRead { get; set; }
	}
}