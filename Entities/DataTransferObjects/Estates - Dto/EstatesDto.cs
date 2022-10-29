using Entities.DataTransferObjects.Locations___Dto;
using System.Collections.Generic;

namespace Back_End.Models.Employees___Dto
{
    public class EstatesDto
    {
        public int EstateID { get; set; }

        public string EstatePhone { get; set; }
        public string EstateTypes { get; set; }
        public string LocationCityName { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }
        public LocationAddressDto LocationAddress { get; set; }
        public ICollection<EstatesTimesDto> EstatesTimes { get; set; }

    }

}