using System;
using Microsoft.EntityFrameworkCore;

namespace NotificationHub.Models
{
    public class NotificationContext: DbContext
    {
		public NotificationContext(DbContextOptions<NotificationContext> options): base(options)
		{
			this.Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(n => n.Notifications).WithOne().HasForeignKey(c => c.UserId);
        }

		public DbSet<Notification> Notifications { get; set; }
		public DbSet<User> Users { get; set; }
    }
}