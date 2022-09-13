using Back_End.Entities;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Hubs
{
    public class Mensaje : Hub
    {
        public async Task NewMessage(SendMessage msg)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            msg.CreatedDate = DateTime.Now;

            //msg.CreatedDate = date.ToString(@"hh\:mm");

            var person = cruzRojaContext.Persons
                   .Where(a => a.ID == msg.userID)
                   .AsNoTracking()
                   .FirstOrDefault();

            var user = cruzRojaContext.Users
                          .Where(a => a.UserID == msg.userID)
                          .AsNoTracking()
                          .FirstOrDefault();

            msg.Name = person.FirstName + " " + person.LastName;
            msg.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{user.Avatar}";

            var sala = Convert.ToString(msg.chatRoomID);

            await Clients.Group(sala).SendAsync("ReceiveMessage",  msg);
        }

        public async Task AddToGroup(string FK_ChatRoomID)
        {
            var sala = Convert.ToString(FK_ChatRoomID);

            await Groups.AddToGroupAsync(Context.ConnectionId, sala);

            await Clients.Group(sala).SendAsync("ShowWho", $"Alguien se conecto {Context.ConnectionId}");
        }
    }
}
