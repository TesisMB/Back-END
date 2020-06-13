using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SICREYD.Models
{
    //Esta clase que extiende de Users define aquellos valores autorizados que pueden ser actualizados.
    public class UsersForUpdate
    {
        public string UserPassword { get; set; }

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }

        public string UserAddress { get; set; }

        public string UserBirthdate { get; set; }
        public string UserAvatar { get; set; }
        public int IdRole { get; set; }
    }
}
