using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUsersChatRoomsRepository : IRepositoryBase<UsersChatRooms>
    {
        void JoinGroup(UsersChatRooms usersChat);
        void LeaveGroup(UsersChatRooms usersChat);

        Task<UsersChatRooms> GetUsersChatRooms(int userChat, int chatRoom);
    }
}
