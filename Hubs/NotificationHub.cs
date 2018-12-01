using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Notifications.Hubs
{
    public class NotificationsHub : Hub
    {
        public async Task SendNotification(string user, string notification)
        {
            await Clients.All.SendAsync("ReceiveNotification", user, "signalR works");
        }

        public async Task NewNotification(string user, string notification)
        {
            await Clients.All.SendAsync("ReceiveNotification", user, notification);
        }
    }
}