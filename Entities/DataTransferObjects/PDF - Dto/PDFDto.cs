using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.PDF___Dto
{
    public  class PDFDto
    {
        public string CreateDate { get; set; }
        public string Location { get; set; }
        public string LocationFile { get; set; }

        public EmergenciesDisastersAppDto EmergenciesDisasters { get; set; }

    }
}
