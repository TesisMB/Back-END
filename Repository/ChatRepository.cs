using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<ChatRooms> GetChat(int chatID, bool status)
        {

            CruzRojaContext _cruzRojaContext = new CruzRojaContext();
            var collection = _cruzRojaContext.ChatRooms as IQueryable<ChatRooms>;

            if(status == true)
            {
                collection = collection.Where(x => x.ID.Equals(chatID));

            }
            else
            {
                collection = collection.Where(x => x.ID.Equals(chatID)
                                              && x.UsersChatRooms.Any(a => a.Status.Equals(false)));
            }


            return collection
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
                   .FirstOrDefault();
        }
    }
}
