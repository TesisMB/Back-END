using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class UsersForCreationDto // : UsersForManipulationDto
    {

        //Todas estas variables van a ser necesarias a la hora de crear un nuevo Usuario
        [MaxLength(8, ErrorMessage = ("El campo DNI deber tener como maximo 8 caracteres"))]
        public string UserDni { get; set; }

        [MinLength(8, ErrorMessage = ("El campo Contraseña deber tener minimo 8 caracteres"))]
        [MaxLength(16, ErrorMessage = "El campo Contraseña puede tener maximo 16 caracteres")]
        [Required]
        public string UserPassword { get; set; }

        public Boolean UserAvailability { get; set; }
        public int FK_RoleID { get; set; }
        //public int ID { get; set; }
        public virtual PersonForCreationDto Persons { get; set; }

    }
}
