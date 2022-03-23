using Back_End.Models.Employees___Dto;
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Estates___Dto
{
    public class EstatesTypeDto
    {
        public int LocationID { get; set; }

        public string LocationDepartmentName { get; set; }
        public string LocationCityName { get; set; }

        public string LocationMunicipalityName { get; set; }

        public ICollection<EstatesDto> Estates { get; set; }

    }
}

