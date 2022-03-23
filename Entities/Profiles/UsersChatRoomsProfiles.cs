using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class UsersChatRoomsProfiles : Profile
    {
        public UsersChatRoomsProfiles()
        {
            CreateMap<UsersChatRooms, UsersChatRoomsDto>()
                     .ForPath(resp => resp.UserID, opt => opt.MapFrom(a => a.FK_UserID))

                    .ForPath(resp => resp.Name, opt => opt.MapFrom(a => a.Users.Persons.FirstName + " " + a.Users.Persons.LastName));

            CreateMap<UsersChatRoomsJoin_LeaveGroupDto, UsersChatRooms>();




        }
    }
}
