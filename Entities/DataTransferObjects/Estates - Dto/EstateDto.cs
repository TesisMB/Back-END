using Back_End.Models;
using Back_End.Models.Employees___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Estates___Dto
{
    public class EstateDto
    {
        public int EstatePhone { get; set; }
        public string EstateTypes { get; set; }
        public string LocationCityName { get; set; }

        public LocationAddressDto LocationAddress { get; set; }
        public ICollection<EstatesTimesDto> EstatesTimes { get; set; }

    }
}
