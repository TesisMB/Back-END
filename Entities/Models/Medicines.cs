using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Medicines", Schema = "dbo")]
    public class Medicines
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineName { get; set; }

        [Required]
        [MaxLength(50)]
        public int MedicineQuantity { get; set; }


        [Required]
        public DateTime MedicineExpirationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineLab { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineDrug { get; set; }

        [Required]
        public double MedicineWeight { get; set; }

        [Required]
        public string MedicineUnits { get; set; }

        public String MedicineUtility { get; set; }

        [Required]
        public Boolean MedicineAvailability { get; set; }

        [Required]
        public string MedicinePicture { get; set; }

        [Required]
        public int FK_EstateID { get; set; }

        [ForeignKey("FK_EstateID")]
        public Estates Estates { get; set; }

    }
}
