using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entities.DataTransferObjects.Medicines___Dto
{
    public class MedicineImg
    {
        public IFormFile ImageFile { get; set; }

    }
}
