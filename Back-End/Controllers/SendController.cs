using Entities.DataTransferObjects;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
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

        public static async Task<Message> SendNotification(int userId, string body, int emergency)
        {
             var result = await NotificationService.SendNotification(userId,body, emergency);

            return result;
        }

        public static async Task<Message> SendNotificationAcceptRejectRequestChat(int userId, int chatRoomId, bool status) 
        {
            var result = await NotificationService.SendNotificationAcceptRejectRequestChat(userId, chatRoomId, status);

            return result;
        }
        public static async Task<Message> SendNotificationByChat(int userId, string messages, int chatRoomID)
        {
            var result = await NotificationService.SendNotificationChat(userId, messages, chatRoomID);

            return result;
        }


        public static Message SendNotificationJoinGroup(int userId, UsersChatRoomsJoin_LeaveGroupDto userChatRoom)
        {
            var result =  NotificationService.SendNotificationJoinGroup(userId, userChatRoom);

            return result;
        }
    }
}
