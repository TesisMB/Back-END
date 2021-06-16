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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstatesTimes>()

            .HasKey(s => new { s.FK_EstateID, s.FK_TimeID });

            modelBuilder.Entity<VolunteersSkills>()

           .HasKey(s => new { s.FK_VolunteerID, s.FK_SkillID });

        }

        //Defino cada una de las Models, que se usan durante el proyecto, donde cada una de ellas representa una tabla de la base de datos
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Estates> Estates { get; set; }
        public virtual DbSet<LocationAddress> LocationAddresses { get; set; }
        public virtual DbSet<Times> Times { get; set; }
        public virtual DbSet<EstatesTimes> EstatesTimes { get; set; }
        public virtual DbSet<Volunteers> Volunteers { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<VolunteersSkills> VolunteersSkills { get; set; }
    }
}
