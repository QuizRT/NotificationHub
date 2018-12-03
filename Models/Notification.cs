using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NotificationEngine.Models
{
	public class Notification
	{
		[Key]
		public int NotificationId { get; set; }
		public string Message { get; set; }
		public string TargetUrl { get; set; }

		[NotMapped]
		public List<string> Users { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		public static Notification ToObject(string notification)
		{
			return JsonConvert.DeserializeObject<Notification>(notification);
		}
	}
}