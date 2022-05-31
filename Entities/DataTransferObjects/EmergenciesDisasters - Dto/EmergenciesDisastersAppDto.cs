using Entities.DataTransferObjects.Alerts___Dto;
using Entities.DataTransferObjects.LocationsEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using System;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
    public class EmergenciesDisastersAppDto
    {
        public int EmergencyDisasterID { get; set; }

        public DateTime EmergencyDisasterStartDate { get; set; }

        public string? EmergencyDisasterEndDate { get; set; }


        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }


        public LocationsEmergenciesDisastersDto LocationsEmergenciesDisasters { get; set; }

        public AlertsDto Alerts { get; set; }

    }
}
