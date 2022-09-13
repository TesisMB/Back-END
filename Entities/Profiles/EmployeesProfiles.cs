using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Entities.DataTransferObjects.Employees___Dto;
using Entities.DataTransferObjects.Models.Vehicles___Dto;
using Entities.Helpers;

namespace Back_End.Profiles
{
    public class EmployeesProfiles : Profile
    {
        public EmployeesProfiles()
        {
            //Creo Las clases a ser mapeadas
            CreateMap<Employees, EmployeesDto>()
                                .ForMember(i => i.UserDni, opt => opt.MapFrom(src => src.Users.UserDni))
                                .ForMember(i => i.UserAvailability, opt => opt.MapFrom(src => src.Users.UserAvailability))
                                .ForMember(i => i.RoleName, opt => opt.MapFrom(src => src.Users.Roles.RoleName))
                                .ForMember(i => i.Name, opt => opt.MapFrom(src => src.Users.Persons.FirstName + " " + src.Users.Persons.LastName))
                                .ForMember(i => i.Status, opt => opt.MapFrom(src => src.Users.Persons.Status));


            CreateMap<Employees, EmployeeDto>();
               // .ForPath(i => i.Users.Createdate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDateTime(src.Users.CreatedDate)));

            CreateMap<Employees, EmployeesVehiclesDto>();

            CreateMap<EmployeesForCreationDto, Employees>();

            CreateMap<EmployeeForUpdateDto, Employees>();
            CreateMap<Employees, EmployeeForUpdateDto>();

        }
    }
}
