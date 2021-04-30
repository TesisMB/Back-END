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


        public virtual DbSet<Estate> Estate { get; set; }

        public virtual DbSet<Vehicles> Vehicles { get; set; }

        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }

        private const string Connection =
       @"Server=DESKTOP-0SC8P3Q;
            Database=CruzRojaDB - Testing;
            Trusted_Connection=True;
            MultipleActiveResultSets=true";

        //Devuelvo la conexion establecida arriba 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Connection);


    }
}
