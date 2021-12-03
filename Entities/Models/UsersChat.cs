using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("UsersChat", Schema="dbo")]
    public class UsersChat
    {
        public int FK_ChatID { get; set; }
        public int FK_UserID { get; set; }

        public Chat Chat { get; set; }
        public Users Users { get; set; }
    }
}
