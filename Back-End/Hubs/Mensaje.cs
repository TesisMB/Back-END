using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Repository;
using System;
using System.Threading.Tasks;

namespace Back_End.Hubs
{
    public class Mensaje : Hub
    {
        public async Task NewMessage(MessagesForCreationDto msg)
        {
            var sala = Convert.ToString(msg.FK_ChatRoomID);

            await Clients.Group(sala).SendAsync("ReceiveMessage",  msg);
        }

        public async Task AddToGroup(int FK_ChatRoomID)
        {
            var sala = Convert.ToString(FK_ChatRoomID);

            await Groups.AddToGroupAsync(Context.ConnectionId, sala);

            await Clients.Group(sala).SendAsync("ShowWho", $"Alguien se conecto {Context.ConnectionId}");
        }
    }
}
