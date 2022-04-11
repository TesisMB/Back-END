using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ChatRoomsRepository : RepositoryBase<TypesChatRooms>, IChatRoomsRepository
    {
        private CruzRojaContext _cruzRojaContext;

        public ChatRoomsRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;

        }

        public async Task<IEnumerable<TypesChatRooms>> GetChatRooms()
        {

            var users = UsersRepository.authUser;


            var collection = _cruzRojaContext.TypesChatRooms as IQueryable<TypesChatRooms>;


            return await collection
                .Include(a => a.Chat)

                .ThenInclude(a => a.UsersChat)
                 .ThenInclude(a => a.Users)
                .ThenInclude(a => a.Persons)


                 .Include(a => a.Chat)
                .ThenInclude(a => a.UsersChat)
                 .ThenInclude(a => a.Users.Roles)
                

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
