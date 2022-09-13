using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.PDF___Dto
{
    public  class PDFDto
    {
        public int ID { get; set; }
        public string CreateDate { get; set; }
        public string Location { get; set; }
        public string LocationFile { get; set; }
        public string PDFDateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string CreatedByEmployee { get; set; }
        public string ModifiedByEmployee { get; set; }
        public EmergenciesDisastersAppDto EmergenciesDisasters { get; set; }

    }
}
