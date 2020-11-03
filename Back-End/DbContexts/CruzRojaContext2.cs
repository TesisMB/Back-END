using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
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
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Estate> Estate { get; set; }

        public virtual DbSet<Vehicles> Vehicles { get; set; }

        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }

    }
}
