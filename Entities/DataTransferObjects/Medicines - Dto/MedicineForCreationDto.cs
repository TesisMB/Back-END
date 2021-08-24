using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Medicines___Dto
{
    public class MedicineForCreationDto
    {
        public String MedicineName { get; set; }
        public int MedicineQuantity { get; set; }
        public DateTime MedicineExpirationDate { get; set; }
        public String MedicineLab { get; set; }
        public String MedicineDrug { get; set; }
        public float MedicineWeight { get; set; }
        public string MedicineUnits { get; set; }

        public String MedicineUtility { get; set; }
        public Boolean MedicineAvailability { get; set; } = true;
        public int FK_EstateID { get; set; }
    }
}
