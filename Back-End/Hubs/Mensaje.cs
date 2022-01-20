using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Repository;
using System.Threading.Tasks;

namespace Back_End.Hubs
{
    public class Mensaje : Hub
    {
        public async Task NewMessage(Messages msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }

        /*public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task NewMessage(string mensaje)
        {
            return Clients.All.SendAsync("ReceiveMessage", mensaje);
        }

        public async Task SendMessage(string room, string user, string message)
        {
            var username = UsersRepository.authUser;

            user = username.Persons.FirstName + "" + username.Persons.LastName;

            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);

            await Clients.Group(room).SendAsync("ShowWho", $"Alguien se conecto {Context.ConnectionId}");
        }
        */
    }
}
