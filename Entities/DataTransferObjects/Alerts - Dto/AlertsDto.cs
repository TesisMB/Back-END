using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Alerts___Dto
{
   public class AlertsDto
    {
        public int AlertID { get; set; }

        public string AlertMessage { get; set; }

        public string AlertDegree { get; set; }

    }
}
