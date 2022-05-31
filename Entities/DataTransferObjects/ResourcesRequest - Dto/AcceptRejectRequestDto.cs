using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.ResourcesRequest___Dto
{
   public class AcceptRejectRequestDto
    {
        public string Reason { get; set; }
        public int FK_EmergencyDisasterID { get; set; }
        public Boolean Status { get; set; }

        public int UserRequest { get; set; }

        public int FK_EmployeeID { get; set; }

        public int AnsweredBy { get; set; }

    }
}
