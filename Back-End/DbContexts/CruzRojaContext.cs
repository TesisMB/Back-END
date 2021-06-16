using Microsoft.EntityFrameworkCore;
using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    public class CruzRojaContext : DbContext /*Creo un Context para poder obtenener apenas me autentifico el DNI y el password
                                             y devolver los resultados correspondientes */
    {    
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Estates> Estates { get; set; }
        public virtual DbSet<LocationAddress> LocationAddresses { get; set; }
        public virtual DbSet<Volunteers> Volunteers { get; set; }
        public virtual DbSet<Times> Times { get; set; }
        public virtual DbSet<EstatesTimes> EstatesTimes { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<VolunteersSkills> VolunteersSkills { get; set; }




        private const string Connection =
       @"Server=DESKTOP-4PRPJUH;
            Database=CruzRojaDB - Testing;
            Trusted_Connection=True;
            MultipleActiveResultSets=true";

        //Devuelvo la conexion establecida arriba 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Connection);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstatesTimes>()

            .HasKey(s => new { s.FK_EstateID, s.FK_TimeID });
          
            modelBuilder.Entity<VolunteersSkills>()

           .HasKey(s => new { s.FK_VolunteerID, s.FK_SkillID});

        }
    }
}
