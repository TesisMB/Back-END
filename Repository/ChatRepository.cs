using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatRepository : RepositoryBase<ChatRooms>, IChatRepository
    {
        public readonly CruzRojaContext _cruzRojaContext;
        public ChatRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;

        }

        public async Task<ChatRooms> GetChat(int chatID)
        {

            return await FindByCondition(i => i.ID.Equals(chatID))

                   .Include(i => i.TypesChatRooms)
                   .Include(i => i.UsersChatRooms)
                   .Include(i => i.EmergenciesDisasters)
                   .Include(i => i.EmergenciesDisasters.TypesEmergenciesDisasters)
                   .Include(i => i.EmergenciesDisasters)
                   .ThenInclude(i => i.LocationsEmergenciesDisasters)
                   .Include(a => a.DateMessage)
                   .ThenInclude(a => a.Messages)
                   .ThenInclude(a => a.Users)
                   .ThenInclude(a => a.Persons)
                   .FirstOrDefaultAsync();
        }
    }
}
