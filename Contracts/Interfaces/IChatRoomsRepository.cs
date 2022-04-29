using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IChatRoomsRepository : IRepositoryBase<ChatRooms>
    {
        Task<IEnumerable<ChatRooms>> GetChatRooms();
    }
}
