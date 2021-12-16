using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatRepository : RepositoryBase<ChatRooms>, IChatRepository
    {
        public ChatRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
        {

        }

        public async Task<ChatRooms> GetChat(int chatID)
        {
            return await FindByCondition(i=> i.ID.Equals(chatID))
                   .Include(i => i.TypesChatRooms)
                   .Include(i => i.UsersChatRooms)
                   .ThenInclude(i=> i.Users)
                   .ThenInclude(i=> i.Persons)
                   .Include(i => i.EmergenciesDisasters)
                   .Include(i=> i.EmergenciesDisasters.TypesEmergenciesDisasters)
                   .Include(i => i.EmergenciesDisasters)
                   .ThenInclude(i=> i.Locations)
                   .FirstOrDefaultAsync();
        }
    }
}
