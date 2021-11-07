using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Back_End.Hubs
{
    public class Mensaje : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task NotificaTodos(string mensaje)
        {
            return Clients.All.SendAsync("prepararventa", mensaje);
        }

    }
}
