namespace Back_End.Dto
{
    //Esta clase que extiende de Users define aquellos valores autorizados que pueden ser actualizados.
    public class UsersForUpdate
    {
        public string UserPassword { get; set; }

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }

        public string UserAddress { get; set; }

        public int IdRole { get; set; }
    }
}
