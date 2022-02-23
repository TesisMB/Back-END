using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using System;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
    public class EmergenciesDisasters2Dto
    {
        public int EmergencyDisasterID { get; set; }

        public DateTime EmergencyDisasterStartDate { get; set; }

        public string? EmergencyDisasterEndDate { get; set; }

        public string EmergencyDisasterInstruction { get; set; }

        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }

    }
}
