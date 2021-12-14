using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IChatRepository: IRepositoryBase<ChatRooms>
    {
        Task<ChatRooms> GetChat(int chatID);
    }
}
