using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class JwtSettings
    {
        //Estos valores van a permitir almacenar la informacion del token
        public string Key { get; set; }
        public string Issuer { get; set; } //Emisor
        public string Audience { get; set; }
        public int MinutesToExpiration { get; set; } 
    }
}