using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Notifications", Schema = "dbo")]
    public class Notifications
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public Boolean State { get; set; }

        [ForeignKey("FK_NotificationID")]
        public ICollection<UsersNotifications> UsersNotifications { get; set; }
    }
}
