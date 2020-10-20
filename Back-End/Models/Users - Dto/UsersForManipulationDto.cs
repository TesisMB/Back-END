using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Users___Dto
{

    [UserDniMustBeDifferentFromUserDniDto(
        ErrorMessage = "El Dni que ingreso ya existe")]
    public abstract class UsersForManipulationDto
    {
        [Required(ErrorMessage = "Debe completar el campo - Nombre")]
        [MaxLength(100, ErrorMessage =  "El campo Nombre puede tener hasta 100 caracteres")]
        public string UserFirstName { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Apellido")]
        [MaxLength(100, ErrorMessage = "El campo Apellido puede tener hasta 100 caracteres")]

        public string UserLastname { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Dni")]
        [MaxLength(8, ErrorMessage = "El campo Dni debe tener  8 caracteres")]
        public string UserDni { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Contraseña")]
        [MinLength(8, ErrorMessage = ("El campo Contraseña deber tener minimo 8 caracteres"))]
        [MaxLength(16, ErrorMessage = "El campo Contraseña puede tener maximo 16 caracteres")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Telefono")]
        [MaxLength(12, ErrorMessage = "El campo Telefono puede tener hasta 12 caracteres")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Email")]
        [MaxLength(50, ErrorMessage = "El campo Email puede tener hasta 50 caracteres")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Genero")]
        [MaxLength(1, ErrorMessage = "El campo Genero debe tener  1 caracter")]
        public string UserGender { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Direccion")]
        [MaxLength(12, ErrorMessage = "El campo Direccion puede tener hasta 50 caracteres")]
        public string UserAddress { get; set; }

        [Required(ErrorMessage = "Debe completar el campo - Fecha de Nacimiento")]
        public DateTimeOffset UserBirthdate { get; set; }

        public DateTimeOffset UserCreatedate { get; set; } = DateTime.Now;

        
        [Required(ErrorMessage = "Debe completar el campo - Avatar")]
        public string UserAvatar { get; set; }


        [Required(ErrorMessage = "Debe completar el campo - Role")]

        public int RoleID { get; set; } /*Una vez que el Usuario Ingresa el Id automaticamente 
                                        se le va colocar el nombre del rol al cual pertence ese nuevo usuario */

    }
}
