using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Resources_Request___Dto
{
    public class Resources_RequestForCreationDto
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string Reason { get; set; }

        public bool Status { get; set; } = true;

        public int? FK_UserID { get; set; }

        public int FK_EmergencyDisasterID { get; set; }

        public ResourcesForCreationDto Resources { get; set; }
    }
}
