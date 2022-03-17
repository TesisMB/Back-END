using AutoMapper;
using Entities.DataTransferObjects.Victims___Dto;
using Entities.Models;

namespace Entities.Profiles
{
    public class VictimsProfiles : Profile
    {
        public VictimsProfiles()
        {
            CreateMap<Victims, VictimsDto>();
        }
    }
}
