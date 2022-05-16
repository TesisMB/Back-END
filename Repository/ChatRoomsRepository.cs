using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatRoomsRepository : RepositoryBase<ChatRooms>, IChatRoomsRepository
    {
        private readonly CruzRojaContext _cruzRojaContext;

        public ChatRoomsRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;

        }

        public async Task<IEnumerable<ChatRooms>> GetChatRooms()
        {
            var users = UsersRepository.authUser;
            CruzRojaContext _cruzRojaContext = new CruzRojaContext();
            var collection = _cruzRojaContext.ChatRooms as IQueryable<ChatRooms>;


            collection = collection.Where(x => x.UsersChatRooms.Any(a => a.FK_UserID == users.UserID
                                                                  && x.EmergenciesDisasters.EmergencyDisasterEndDate == null));

            //collection = (from x in collection where userChatRooms.Any(a => a.FK_ChatRoomID == x.ID && x.EmergenciesDisasters.EmergencyDisasterEndDate == null) select x).ToList();

            return await collection
                .Include(a => a.EmergenciesDisasters)
                .ThenInclude(a => a.LocationsEmergenciesDisasters)
                .Include(a => a.EmergenciesDisasters.TypesEmergenciesDisasters)
                .ToListAsync();
        }
    }
}
