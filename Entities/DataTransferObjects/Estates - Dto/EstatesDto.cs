using Entities.DataTransferObjects.Locations___Dto;
using System.Collections.Generic;

namespace Back_End.Models.Employees___Dto
{
    public class EstatesDto
    {
        public int EstateID { get; set; }
        public string EstateTypes { get; set; }

        public ICollection<EstatesTimesDto> EstatesTimes { get; set; }

    }
}