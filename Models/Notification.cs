using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NotificationEngine.Services;

namespace NotificationEngine.Models
{
	public class Notification
	{
		[Key]
		public int NotificationId { get; set; }
		public string Message { get; set; }
		public string TargetUrl { get; set; }

		[NotMapped]
		public IList<string> Users { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		public static Notification ToObject(string notification)
		{
			try 
			{
				Console.WriteLine("noexception");
				Notification output = JsonConvert.DeserializeObject<Notification>(notification);
				return output;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return new Notification();
			} 
		}
	}
}