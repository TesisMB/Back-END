using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("Victims", Schema = "dbo")]
    public class Victims
    {
        [Key, ForeignKey("EmergenciesDisasters")]

        public int ID { get; set; }

        public int NumberDeaths { get; set; }

        public int NumberAffected { get; set; }

        public int NumberFamiliesAffected { get; set; }

        [Column(TypeName = "decimal(8, 4)")]
        public decimal MaterialsDamage { get; set; }

        public int AffectedLocalities { get; set; }

        public int EvacuatedPeople { get; set; }

        public int AffectedNeighborhoods { get; set; }

        public int AssistedPeople { get; set; }
        public int RecoveryPeople { get; set; }


        public EmergenciesDisasters EmergenciesDisasters { get; set; }


    }
}
