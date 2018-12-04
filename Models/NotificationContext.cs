using System;
using Microsoft.EntityFrameworkCore;

namespace NotificationEngine.Models
{
    public class NotificationContext: DbContext
    {
		public NotificationContext(DbContextOptions<NotificationContext> options): base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Notification>()
				.HasMany(n => n.UserNotifications)
				.WithOne(n => n.Notification)
				.HasForeignKey(u => u.NotificationId);
        }

		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }
    }
}