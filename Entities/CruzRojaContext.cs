using Back_End.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Back_End.Entities
{
    public partial class CruzRojaContext : DbContext //Creo un Context2 para poder manipular los repositorios,
                                                     //interfaces y poder realizar las conexiones a la base de datos de manera correcta
    {
        public CruzRojaContext()
        {
        }



        //Este metodo va a ser que devuelva la conexion que se establece en la clase Startup
        public CruzRojaContext(DbContextOptions<CruzRojaContext> options)
              : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstatesTimes>()

           .HasKey(s => new { s.FK_EstateID, s.FK_TimeID });

            modelBuilder.Entity<VolunteersSkills>()

           .HasKey(s => new { s.FK_VolunteerID, s.FK_SkillID });



            modelBuilder.Entity<TypeVehiclesModels>()

           .HasKey(s => new { s.FK_TypeVehicleID, s.FK_ModelID });

        }

        //Defino cada una de las Models, que se usan durante el proyecto, donde cada una de ellas representa una tabla de la base de datos
        public DbSet<Users> Users { get; set; }
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Schedules> Schedules { get; set; }
        public DbSet<Estates> Estates { get; set; }
        public DbSet<LocationAddress> LocationAddresses { get; set; }
        public DbSet<Times> Times { get; set; }
        public DbSet<EstatesTimes> EstatesTimes { get; set; }
        public DbSet<Volunteers> Volunteers { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<VolunteersSkills> VolunteersSkills { get; set; }
        public DbSet<TypeVehicles> TypeVehicles { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Medicines> Medicines { get; set; }

        private const string Connection =
      @"Server=DESKTOP-0SC8P3Q;
            Database=CruzRojaDB - Testing;
            Trusted_Connection=True;
            MultipleActiveResultSets=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection);
            }
        }
    }
}
