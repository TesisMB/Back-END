
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


        public int CreatedBy { get; set; }
        public string CreatedByEmployee { get; set; }

        public int ModifiedBy { get; set; }
        public string ModifiedByEmployee { get; set; }

        public int AnsweredBy { get; set; }
        public string AnsweredByEmployee { get; set; }


        //Emergencias
        public int EmergencyDisasterID { get; set; }
        public string EmergencyDisasterEndDate{ get; set; }

        //Locations

        public string LocationDepartmentName { get; set; }

        public string LocationMunicipalityName { get; set; }

        public string LocationCityName { get; set; }


        //Tipo de emergencia

        public int TypeEmergencyDisasterID { get; set; }

        public string TypeEmergencyDisasterName { get; set; }



        //public EmployeesDto EmployeeCreated { get; set; }

        //public EmergenciesDisastersDto EmergenciesDisasters { get; set; }
        

        public ICollection<ResourcesRequestMaterialsMedicinesVehiclesDto> Resources_RequestResources_Materials_Medicines_Vehicles { get; set; }
    }
}
