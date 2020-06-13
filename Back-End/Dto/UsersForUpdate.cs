namespace Back_End.Dto
{
    //Esta clase que extiende de Users define aquellos valores autorizados que pueden ser actualizados.
    public class UsersForUpdate
    {
        public string Password { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int IdRole { get; set; }
    }
}
