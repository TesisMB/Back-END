using Entities.DataTransferObjects.FormationsDates___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.FormationsEstates___Dto
{
    public class FormationsEstatesDto
    {
        public int ID { get; set; }

        public string FormationEstateName { get; set; }

        public FormationsDatesDto FormationsDates { get; set; }
    }
}
