using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using NotificationEngine.Hubs;
using NotificationEngine.Models;
using NotificationEngine.Services;

namespace NotificationEngine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "Server=localhost\\SQLEXPRESS;Database=NotificationDb;Trusted_Connection=True;";


			var dbContextOptions = new DbContextOptionsBuilder<NotificationContext>().UseSqlServer(connectionString).Options;
			var notificationContext = new NotificationContext(dbContextOptions);

			services.AddScoped<IReadNotificationService, NotificationService>();
			services.AddSingleton<ICreateNotificationService>(s => new NotificationService(notificationContext));
			services.AddSingleton<NotificationBroadcaster>();
			services.AddSingleton<NotificationConsumerService>();

			services.AddDbContext<NotificationContext>(option => option.UseSqlServer(connectionString));
			services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
               builder
                   .AllowAnyMethod ()
                   .AllowAnyHeader ()
                   .AllowCredentials ()
                   .AllowAnyOrigin();
           	}));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
		, NotificationConsumerService notificationService
		)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

			// using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			// {
			// 	NotificationContext dbContext = serviceScope.ServiceProvider.GetRequiredService<NotificationContext>();
			// 	dbContext.Database.Migrate();
			// }

            app.UseCors ("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notifications");
            });
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
