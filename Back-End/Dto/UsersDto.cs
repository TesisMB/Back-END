namespace Back_End.Dto
{

    public class UsersDto : RolesDto /*Me va a permitir heredar las variables tanto el IdRole 
                                       como el RoleName al momento de listar los Usuarios*/
    {
        public int UserId { get; set; }

        public string UserFullName { get; set; } /*Almaceno el Nombre y el Apellido de cada usuario y 
                                             devuelvo el nombre completo  concatenando ambos valores (Name+LastName)*/

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }

        public string UserAddress { get; set; }

        public int UserAge { get; set; } /*Almaceno en la base de datos las fechas de nacimientos
                                      de los usarios y devuelvo la edad correspondientes para cada uno de ellos */

    }
}
