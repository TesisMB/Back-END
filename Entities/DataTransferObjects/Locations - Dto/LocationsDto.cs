using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Locations___Dto
{
    public class LocationsDto
    {
        public int LocationID { get; set; }

   
        public string LocationDepartmentName { get; set; }

        public string LocationMunicipalityName { get; set; }

        public string LocationCityName { get; set; }

        public int LocationLongitude { get; set; }
        public int LocationLatitude { get; set; }

    }
}
