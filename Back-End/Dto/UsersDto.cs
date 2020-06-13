namespace Back_End.Dto
{

    public class UsersDto : RolesDto /*Me va a permitir heredar las variables tanto el IdRole 
                                       como el RoleName al momento de listar los Usuarios*/
    {
        public int IdUser { get; set; }

        public string FullName { get; set; } /*Almaceno el Nombre y el Apellido de cada usuario y 
                                             devuelvo el nombre completo  concatenando ambos valores (Name+LastName)*/

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int Age { get; set; } /*Almaceno en la base de datos las fechas de nacimientos
                                      de los usarios y devuelvo la edad correspondientes para cada uno de ellos */

    }
}
