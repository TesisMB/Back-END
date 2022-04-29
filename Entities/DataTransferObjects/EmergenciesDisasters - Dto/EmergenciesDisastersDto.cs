using Back_End.Models;
using Entities.DataTransferObjects.Alerts___Dto;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.DataTransferObjects.LocationsEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Victims___Dto;
using Entities.Models;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
    public class EmergenciesDisastersDto
    {
        public int EmergencyDisasterID { get; set; }

        public DateTime EmergencyDisasterStartDate { get; set; }

        public string? EmergencyDisasterEndDate { get; set; }

        public string EmergencyDisasterInstruction { get; set; }

        public EmployeesDto Employees { get; set; }

        public LocationsEmergenciesDisastersDto LocationsEmergenciesDisasters { get; set; }

        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }

        public AlertsDto Alerts { get; set; }

        public ChatRoomsDto ChatRooms { get; set; }

        public VictimsDto Victims { get; set; }

        public ICollection<ResourcesRequestDto> Resources_Requests { get; set; }


    }
}
