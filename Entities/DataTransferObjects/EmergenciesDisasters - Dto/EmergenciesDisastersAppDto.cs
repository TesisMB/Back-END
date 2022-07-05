using Entities.DataTransferObjects.Alerts___Dto;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.LocationsEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.Victims___Dto;
using System;

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


        public LocationsEmergenciesDisastersDto LocationsEmergenciesDisasters { get; set; }

        public AlertsDto Alerts { get; set; }

        public ChatRoomsDto ChatRooms { get; set; }

        public VictimsDto Victims { get; set; }

    }
}
