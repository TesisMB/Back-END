using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EstatesRepository : RepositoryBase<Locations>, IEstatesRepository
    {
        private readonly CruzRojaContext _cruzRojaContext;
        public EstatesRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }

        public async Task<IEnumerable<Locations>> GetAllEstates()
        {
            return await FindAll()
                .Include(i => i.Estates)
                .ThenInclude(i => i.EstatesTimes)
                .ThenInclude(i => i.Times)
                .ThenInclude(i => i.Schedules)
                .Include(i => i.Estates)
                .ThenInclude(i => i.LocationAddress)
                .ToListAsync();
        }


        public IEnumerable<LocationVolunteers> GetAllLocations()
        {
            return _cruzRojaContext.locationVolunteers
                     .ToList();
        }

        public IEnumerable<Estates> GetAllEstatesByPdf()
        {

            var collection = _cruzRojaContext.Estates as IQueryable<Estates>;
          

            return collection
                           .Include(i => i.EstatesTimes)
                           .ThenInclude(i => i.Times)
                           .ThenInclude(i => i.Schedules)
                           .Include(i => i.Locations)
                           .Include(i => i.LocationAddress)
                           .Include(i => i.Materials)
                           .Include(i => i.Medicines)

                           .Include(i => i.Vehicles)
                           .ThenInclude(i => i.Brands)

                           .Include(i => i.Vehicles)
                           .ThenInclude(i => i.Model)

                           .Include(i => i.Vehicles)
                           .ThenInclude(i => i.TypeVehicles)
                           .ToList();
        }


        public Estates GetAllEstateByPdf(int estateId)
        {

            var collection = _cruzRojaContext.Estates as IQueryable<Estates>;

            collection = collection.Where(
                a => a.EstateID == estateId
                && a.Materials.Any(i => i.MaterialQuantity > 0)
                && a.Medicines.Any(i => i.MedicineAvailability != false
                && a.Vehicles.Any(i => i.VehicleAvailability != false))
                );


            return  collection
                           .Include(i => i.EstatesTimes)
                           .ThenInclude(i => i.Times)
                           .ThenInclude(i => i.Schedules)
                           .Include(i => i.Locations)
                           .Include(i => i.LocationAddress)
                           .Include(i => i.Materials)
                           .Include(i => i.Medicines)

                           .Include(i => i.Vehicles)
                           .ThenInclude(i => i.Brands)
                           
                           .Include(i => i.Vehicles)
                           .ThenInclude(i => i.Model)

                           .Include(i => i.Vehicles)
                           .ThenInclude(i => i.TypeVehicles)
                           .FirstOrDefault();
        }




            public async Task<IEnumerable<Locations>> GetAllEstatesType()
        {
            return await FindAll()
                          .ToListAsync();
        }
    }
}
