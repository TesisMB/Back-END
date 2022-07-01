using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.PDF___Dto
{
    public class PDFForCreationDto
    {
        public string Location { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Category { get; set; } = "Monitoreo";
        public int CreatedBy { get; set; }

        public IFormFile LocationFile { get; set; }

        public int FK_EmergencyDisasterID { get; set; }

    }
}
