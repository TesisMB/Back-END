using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.PDF___Dto
{
    public class PDFForUpdateDto
    {
        public string Location { get; set; }
        public DateTime? PDFDateModified { get; set; }

        public int FK_EmergencyDisasterID { get; set; }
    }
}
