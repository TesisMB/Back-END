using Microsoft.AspNetCore.Http;
using System;

namespace Entities.DataTransferObjects.Medicines___Dto
{
    public class MedicineForCreationDto
    {
        public DateTime MedicineExpirationDate { get; set; }

        public String MedicineLab { get; set; }

        public String MedicineDrug { get; set; }

        public double MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }
    }
}
