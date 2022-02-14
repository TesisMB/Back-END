using AutoMapper;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class UsersChatProfiles : Profile
    {
        public UsersChatProfiles()
        {
            CreateMap<UsersChat, UsersChatDto>()

                  .ForPath(resp => resp.UserID, opt => opt.MapFrom(a => a.Users.UserID))

                   .ForPath(resp => resp.Name, opt => opt.MapFrom(a => $"{a.Users.Persons.FirstName} {a.Users.Persons.LastName}"));
        }
    }
}
