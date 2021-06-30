
using AutoMapper;
using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using Entities.DataTransferObjects.Vehicles___Dto.Update;

namespace Entities.Profiles
{
    public class VehiclesProfiles : Profile
    {
        public VehiclesProfiles()
        {
            CreateMap<Vehicles, VehiclesDto>();

            CreateMap<VehiclesForCreationDto, Vehicles>();

            CreateMap<VehiclesForUpdateDto, Vehicles>();
            CreateMap<Vehicles, VehiclesForUpdateDto>();
        }
    }
}
