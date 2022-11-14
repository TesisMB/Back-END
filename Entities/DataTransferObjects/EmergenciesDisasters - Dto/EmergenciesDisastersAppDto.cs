using Entities.DataTransferObjects.Alerts___Dto;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.LocationsEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Victims___Dto;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
    public class EmergenciesDisastersAppDto
    {
        public int EmergencyDisasterID { get; set; }

        public DateTime EmergencyDisasterStartDate { get; set; }

        public string? EmergencyDisasterEndDate { get; set; }

        public int Fk_EmplooyeeID { get; set; }
        public string EmployeeName { get; set; }

        public int ModifiedBy { get; set; }
        public string ModifiedByEmployee { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedByEmployee { get; set; }

        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }
        public string EmergencyDisasterInstruction { get; set; }

        public List<ResourcesRequestDto> Resources_Requests { get; set; }

        public LocationsEmergenciesDisastersDto LocationsEmergenciesDisasters { get; set; }

        public AlertsDto Alerts { get; set; }

       public ChatRoomsDto ChatRooms { get; set; }

        public VictimsDto Victims { get; set; }

        public ICollection<UsersChatRoomsDto> UsersChatRooms { get; set; }

        public string Type { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string alertName { get; set; }

        //public ICollection<ResourcesRequestMaterialsMedicinesVehiclesDto> Resources_Requests { get; set; }

        public Dictionary<string, int> Recursos { get; set; }
        //public List<ReportsDto> Reports { get; set; }


    }
}
