using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class DateMessage
    {
        [Key]
        public int ID { get; set; }

        public int FK_ChatRoomID { get; set; }
        public DateTime CreatedDate { get; set; }

        //public int FK_DateID { get; set; }

        //[ForeignKey("FK_DateID")]

        //public Date Date { get; set; }


        public ICollection<Messages> Messages { get; set; }


        [ForeignKey("FK_ChatRoomID")]

        public ChatRooms ChatRooms { get; set; }


    }

}
