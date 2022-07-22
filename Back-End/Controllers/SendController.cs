using Entities.DataTransferObjects;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public  class SendController : ControllerBase
    {
        private static  INotificationService _notificationService;

        public  SendController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public static async Task<Message> SendNotification(string body)
        {
             var result = await NotificationService.SendNotification(body);

            return result;
        }
    }
}
