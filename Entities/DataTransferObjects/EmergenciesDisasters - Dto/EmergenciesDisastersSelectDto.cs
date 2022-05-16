using Entities.DataTransferObjects.Locations___Dto;
using Entities.DataTransferObjects.LocationsEmergenciesDisasters___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.Models;
using System;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
    public class EmergenciesDisastersSelectDto
    {
        public int EmergencyDisasterID { get; set; }

        public DateTime EmergencyDisasterStartDate { get; set; }

        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }

        public LocationsEmergenciesDisastersDto LocationsEmergenciesDisasters { get; set; }

    }
}
