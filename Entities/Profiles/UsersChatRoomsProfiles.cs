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
                     .ForMember(resp => resp.UserID, opt => opt.MapFrom(a => a.FK_UserID))

                     .ForMember(resp => resp.UserDni, opt => opt.MapFrom(a => a.Users.UserDni))

                     .ForMember(resp => resp.RoleName, opt => opt.MapFrom(a => a.Users.Roles.RoleName))

                     .ForMember(resp => resp.Picture, opt => opt.MapFrom(a => a.Users.Volunteers.VolunteerAvatar))

                    .ForMember(resp => resp.Name, opt => opt.MapFrom(a => a.Users.Persons.FirstName + " " + a.Users.Persons.LastName));


            CreateMap<UsersChatRoomsJoin_LeaveGroupDto, UsersChatRooms>();



            CreateMap<UsersChatRooms, UsersChatRoomsEmergenciesDisastersDto>()
                                     .ForPath(resp => resp.UserID, opt => opt.MapFrom(a => a.FK_UserID));

        }
    }
}
