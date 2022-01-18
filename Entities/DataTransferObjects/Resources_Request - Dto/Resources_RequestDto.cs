using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class Resources_RequestDto
    {
        public int ID { get; set; }

        public DateTime RequestDate { get; set; }

        public string Reason { get; set; }

        public Boolean Status { get; set; }

        public EmployeesUsersDto Users { get; set; }

        public EmergenciesDisastersDto EmergenciesDisasters { get; set; }

        public ICollection<Resources_RequestResources_Materials_Medicines_VehiclesDto> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }
    }
}
