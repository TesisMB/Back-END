using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("VolunteersSkills", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql

    public class VolunteersSkills
    {
        public int FK_VolunteerID { get; set; }

        public Volunteers Volunteers { get; set; }

        public int FK_SkillID { get; set; }

        public Skills Skills { get; set; }
    }
}
