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
			modelBuilder.Entity<User>().HasMany(n => n.Notifications).WithOne().HasForeignKey(n => n.UserId);
            modelBuilder.Entity<UserNotification>().HasOne(n => n.Notification).WithOne().HasForeignKey<UserNotification>(u => u.NotificationId);
        }

		public DbSet<Notification> Notifications { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }
    }
}