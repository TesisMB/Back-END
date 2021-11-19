using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatRoomsRepository: RepositoryBase<TypesChatRooms>, IChatRoomsRepository
    {
        public ChatRoomsRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
        {

        }

        public async Task<IEnumerable<TypesChatRooms>> GetChatRooms()
        {
            return await FindAll()
                .Include(a => a.Chat)
                .ThenInclude(a => a.UsersChat)
                 .ThenInclude(a => a.Users)
                .ThenInclude(a => a.Persons)
                .Include(a => a.ChatRooms)
                .ThenInclude(a => a.UsersChatRooms)
                .ThenInclude(a => a.Users)
                .ThenInclude(a => a.Persons)
                .Include(a => a.ChatRooms)
                .ThenInclude(a => a.EmergenciesDisasters)
                .ThenInclude(a => a.Locations)
                .Include(a => a.ChatRooms)
                .ThenInclude(a => a.EmergenciesDisasters)
                .ThenInclude(a => a.TypesEmergenciesDisasters)
                .ToListAsync();
        }
    }
}
