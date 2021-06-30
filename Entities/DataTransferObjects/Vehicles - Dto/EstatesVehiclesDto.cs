using Back_End.Models;
using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Models.Vehicles___Dto
{
    public class EstatesVehiclesDto
    {
        public string EstatePhone { get; set; }
        public string EstateTypes { get; set; }
        public LocationAddressVehiclesDto LocationAddress { get; set; }
        public ICollection<EstatesTimesDto> EstatesTimes { get; set; }
    }
}
