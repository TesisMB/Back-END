using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Medicines___Dto
{
   public class MedicineForUpdateDto
    {
        public String MedicineName { get; set; }
        public String MedicineQuantity { get; set; }
        public DateTimeOffset MedicineExpirationDate { get; set; }
        public String MedicineLab { get; set; }
        public String MedicineDrug { get; set; }
        public String MedicineWeight { get; set; }
        public String MedicineUtility { get; set; }
        public Boolean MedicineAvailability { get; set; }
        public int FK_EstateID { get; set; }
    }
}
