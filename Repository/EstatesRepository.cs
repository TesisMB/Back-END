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

        public IEnumerable<Estates> GetAllEstatesByPdf(string LocationDepartmentName)
        {

            var collection = _cruzRojaContext.Estates as IQueryable<Estates>;

            if (string.IsNullOrEmpty(LocationDepartmentName))
            {
                return GetAllEstatesByPdf();
            }
            else
            {
            collection = collection.Where(
                a => a.Locations.LocationDepartmentName == LocationDepartmentName);
            }


            return collection
                           .Include(i => i.EstatesTimes)
                           .ThenInclude(i => i.Times)
                           .ThenInclude(i => i.Schedules)
                           .Include(i => i.Locations)
                           .ToList();
        }

        public IEnumerable<Estates> GetAllEstatesByPdf()
        {
            return _cruzRojaContext.Estates
                          .Include(i => i.EstatesTimes)
                          .ThenInclude(i => i.Times)
                          .ThenInclude(i => i.Schedules)
                          .Include(i => i.Locations)
                          .ToList();
        }

            public async Task<IEnumerable<Locations>> GetAllEstatesType()
        {
            return await FindAll()
                          .ToListAsync();
        }
    }
}
