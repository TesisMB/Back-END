using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IChatRoomsRepository: IRepositoryBase<TypesChatRooms>
    {
        Task<IEnumerable<TypesChatRooms>> GetChatRooms();
    }
}
