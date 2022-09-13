using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UsersChatRoomsRepository : RepositoryBase<UsersChatRooms>, IUsersChatRoomsRepository
    {
        private CruzRojaContext _cruzRojaContext;
        public UsersChatRoomsRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
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

        public void JoinGroup(UsersChatRooms usersChat, decimal longitude, decimal latitude)
        {
            //usersChat.FK_UserID = UsersRepository.authUser.UserID;
            //var rol = UsersRepository.authUser.Roles.RoleName;

            var user = EmployeesRepository.GetAllEmployeesById(usersChat.FK_UserID);

            usersChat.Users = null;

            if (user.Roles.RoleName == "Voluntario")
            {
                LocationVolunteers locations = new LocationVolunteers()
                {
                    LocationVolunteerLatitude = latitude,
                    LocationVolunteerLongitude = longitude
                    };

                _cruzRojaContext.Add(locations);
                SaveAsync();


               VolunteersLocationVolunteersEmergenciesDisasters volunteersLocationVolunteersEmergenciesDisasters = new VolunteersLocationVolunteersEmergenciesDisasters();

                volunteersLocationVolunteersEmergenciesDisasters.FK_VolunteerID = usersChat.FK_UserID;
                volunteersLocationVolunteersEmergenciesDisasters.FK_LocationVolunteerID = locations.ID;
                volunteersLocationVolunteersEmergenciesDisasters.FK_EmergencyDisasterID = usersChat.FK_ChatRoomID;


                _cruzRojaContext.Add(volunteersLocationVolunteersEmergenciesDisasters);
                SaveAsync();
            }


            Create(usersChat);
        }

        public void LeaveGroup(UsersChatRooms usersChat)
        {
            Delete(usersChat);
        }


        public void Coords(LocationVolunteers Locations)
        {
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Add(Locations);
            SaveAsync();
        }
    }
}
