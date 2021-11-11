using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Hubs
{
   public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
