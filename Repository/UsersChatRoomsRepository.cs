using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersChatRoomsRepository: RepositoryBase<UsersChatRooms>, IUsersChatRoomsRepository
    {
        private CruzRojaContext _cruzRojaContext;
        public UsersChatRoomsRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }

        public async Task<UsersChatRooms> GetUsersChatRooms(int userChat, int ChatRoom)
        {

            var collection = _cruzRojaContext.UsersChatRooms as IQueryable<UsersChatRooms>;

            collection = collection.Where(a => a.FK_UserID.Equals(userChat)
                                        && a.FK_ChatRoomID.Equals(ChatRoom));


            return await collection
                    .Include(i => i.Chat)
                    .Include(i => i.Users)
                   .FirstOrDefaultAsync();
        }

        public void JoinGroup(UsersChatRooms usersChat)
        {
            Create(usersChat);
        }

        public void LeaveGroup(UsersChatRooms usersChat)
        {
            Delete(usersChat);
        }
    }
}
