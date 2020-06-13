using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.DbContexts
{
    public class CruzRojaContext2 : DbContext //Creo un Context2 para poder manipular los repositorios,
                                              //interfaces y poder realizar las conexiones a la base de datos de manera correcta
    {

        //Este metodo va a ser que devuelva la conexion que se establece en la clase Startup
        public CruzRojaContext2(DbContextOptions<CruzRojaContext2> options)
              : base(options)
        {
        }

        //Defino cada una de las Models, que se usan durante el proyecto, donde cada una de ellas representa una tabla de la base de datos
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Users> Users { get; set; }



    }
}
