using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("UsersNotifications", Schema="dbo")]
    public class UsersNotifications
    {
        public int FK_UserID { get; set; }

        public Users Users { get; set; }

        public int FK_NotificationID { get; set; }

        public Notifications Notifications { get; set; }
    }
}
