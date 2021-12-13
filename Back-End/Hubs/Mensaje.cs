using Microsoft.AspNetCore.SignalR;
using Repository;
using System.Threading.Tasks;

namespace Back_End.Hubs
{
    public class Mensaje : Hub
    {
        /*public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task NotificaTodos(string mensaje)
        {
            return Clients.All.SendAsync("prepararventa", mensaje);
        }*/

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

    }
}
