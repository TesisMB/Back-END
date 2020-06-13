using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SICREYD.Models
{
    public class JwtSettings
    {
        //estos valores van a permitir almacenar la informacion del token
        public string Key { get; set; }
        public string Issuer { get; set; } //emisor
        public string Audience { get; set; }
        public int MinutesToExpiration { get; set; } 
    }
}