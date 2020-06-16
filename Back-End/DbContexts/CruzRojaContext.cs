using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.DbContexts
{
    public class CruzRojaContext : DbContext /*Creo un Context para poder obtenener apenas me autentifico el usernmae y el password
                                             y devolver los resultados correspondientes */


    {

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        private const string Connection =
       @"Server=DESKTOP-4PRPJUH;
            Database=CruzRojaDB;
            Trusted_Connection=True;
            MultipleActiveResultSets=true";


        //Devuelvo la conexion establecida arriba 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Connection);


    }
}