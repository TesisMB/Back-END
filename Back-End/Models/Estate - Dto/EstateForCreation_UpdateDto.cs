using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class EstateForCreation_UpdateDto
    {
        public string EstateAddress { get; set; }

        public int EstateNumber { get; set; }

        public string EstateResponsible { get; set; }

        public string EstatePhone { get; set; }

        public string EstateCity { get; set; }

        public string EstatePostalCode { get; set; }

    }
}
