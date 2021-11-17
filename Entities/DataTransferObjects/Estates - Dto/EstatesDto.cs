using Entities.DataTransferObjects.Locations___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class EstatesDto
    {
        public int EstateID { get; set; }
        public string EstatePhone { get; set; }
        public string EstateTypes { get; set; }

        public int LocationAddressID { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int LocationsID { get; set; }

        public ICollection<EstatesTimesDto> EstatesTimes { get; set; }

        //public string NumberAddress { get; set; }
        //public LocationAddressDto LocationAddress { get; set; }
       // public LocationsDto Locations { get; set; }
}
    }
