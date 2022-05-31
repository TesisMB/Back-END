using AutoMapper;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.DataTransferObjects.ResourcesRequest___Dto;
using Entities.Helpers;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Profiles
{
    public class ResourcesRequestProfiles : Profile
    {

        public ResourcesRequestProfiles()
        {
            CreateMap<ResourcesRequest, ResourcesRequestDto>()


                 .ForMember(dest => dest.CreatedByEmployee, opts => opts.MapFrom(src => src.EmployeeCreated.Users.Persons.FirstName + " " + src.EmployeeCreated.Users.Persons.LastName))
                
                 .ForMember(dest => dest.ModifiedByEmployee, opts => opts.MapFrom(src => src.EmployeeModified.Users.Persons.FirstName + " " + src.EmployeeModified.Users.Persons.LastName))
                 
                 .ForMember(dest => dest.AnsweredByEmployee, opts => opts.MapFrom(src => src.EmployeeResponse.Users.Persons.FirstName + " " + src.EmployeeResponse.Users.Persons.LastName))

                 //Emergencia

                 .ForMember(dest => dest.EmergencyDisasterID, opts => opts.MapFrom(src => src.EmergenciesDisasters.EmergencyDisasterID))

                 .ForMember(dest => dest.EmergencyDisasterEndDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDate2(src.EmergenciesDisasters.EmergencyDisasterEndDate)))


                 //Locations
                 .ForMember(dest => dest.LocationCityName, opts => opts.MapFrom(src => src.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName))
             
                 
                 .ForMember(dest => dest.LocationDepartmentName, opts => opts.MapFrom(src => src.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationDepartmentName))
                
                 .ForMember(dest => dest.LocationMunicipalityName, opts => opts.MapFrom(src => src.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationMunicipalityName))
              
                
                 //Tipo de emergencia
                 .ForMember(dest => dest.TypeEmergencyDisasterID, opts => opts.MapFrom(src => src.EmergenciesDisasters.TypesEmergenciesDisasters.TypeEmergencyDisasterID))
                 
                 .ForMember(dest => dest.TypeEmergencyDisasterName, opts => opts.MapFrom(src => src.EmergenciesDisasters.TypesEmergenciesDisasters.TypeEmergencyDisasterName))






                 .ForPath(dest => dest.RequestDate, opts => opts.MapFrom(src => DateTimeOffsetExtensions.GetDate(src.RequestDate)));





            CreateMap<ResourcesRequestForCreationDto, ResourcesRequest>();

            CreateMap<AcceptRejectRequestDto, ResourcesRequest>();

            CreateMap<ResourcesRequest, ResourceRequestForUpdateDto>();


            CreateMap<ResourceRequestForUpdateDto, ResourcesRequest>()
                          //.ForMember(d => d.Resources_RequestResources_Materials_Medicines_Vehicles, opt => opt.Ignore());
                          .ForMember(a => a.Resources_RequestResources_Materials_Medicines_Vehicles, b => b.MapFrom(c => c.Resources_RequestResources_Materials_Medicines_Vehicles));


        }
    }
}