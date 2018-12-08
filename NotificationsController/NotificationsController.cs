using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotificationEngine.Services;
using NotificationEngine.Models;
using NotificationEngine.Hubs;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Notifications.Controllers
{
    [Route("api")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        IReadNotificationService notificationObject;
        public NotificationController(IReadNotificationService _notificationObject)
        {
            this.notificationObject = _notificationObject;
        }

        [HttpGet]
        [Route("notifications/{UserId}")]
        public async Task<IActionResult> GetNotifications(string UserId)
        {
            var notifications = await notificationObject.GetNotifications(UserId);
            if (notifications != null)
            {
                return Ok(notifications);
            }
            else
            {
                return NotFound();
            }
        }
    }
}