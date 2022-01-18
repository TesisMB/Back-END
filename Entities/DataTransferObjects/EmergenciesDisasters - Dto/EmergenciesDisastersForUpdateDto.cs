using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
   public class EmergenciesDisastersForUpdateDto
    {
        public DateTime? EmergencyDisasterEndDate { get; set; }

        public string EmergencyDisasterInstruction { get; set; }

        public int? Fk_EmplooyeeID { get; set; }

        public int FK_LocationID { get; set; }

        public int FK_TypeEmergencyID { get; set; }

        public int FK_AlertID { get; set; }
    }
}
