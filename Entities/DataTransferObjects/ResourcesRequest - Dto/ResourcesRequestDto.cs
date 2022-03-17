using Back_End.Models.Users___Dto.Users___Persons;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class ResourcesRequestDto
    {
        public int ID { get; set; }

        public string RequestDate { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public Boolean Status { get; set; }

        public string Condition { get; set; }

        public EmployeesUsersDto Users { get; set; }
        public EmergenciesDisastersDto EmergenciesDisasters { get; set; }



        public ICollection<ResourcesRequestMaterialsMedicinesVehiclesDto> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }
    }
}
