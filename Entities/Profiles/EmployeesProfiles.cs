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
            CreateMap<Employees, EmployeesDto>();
            CreateMap<Employees, EmployeeDto>()

                .ForPath(i => i.Users.Persons.Birthdate, opt => opt.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.Users.Persons.Birthdate)));

            CreateMap<Employees, EmployeesVehiclesDto>();


            CreateMap<EmployeesForCreationDto, Employees>();

            CreateMap<EmployeeForUpdateDto, Employees>();
            CreateMap<Employees, EmployeeForUpdateDto>();

        }
    }
}
