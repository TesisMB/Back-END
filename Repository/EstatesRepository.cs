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
            var user = EmployeesRepository.GetAllEmployeesById(estateId);

            var collection = _cruzRojaContext.Estates as IQueryable<Estates>;

            collection = collection.Where(a => a.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

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

        public string Date(string month)
        {
            if(month == "Jan")
            {
                    month = "01";
            }
            else if(month == "Feb") {
                    month = "02";
            }
            else if (month == "Mar")
            {
                month = "03";
            }
            else if (month == "Apr")
            {
                month = "04";
            }
            else if (month == "May")
            {
                month = "05";
            }
            else if (month == "Jun")
            {
                month = "06";
            }
            else if (month == "Jul")
            {
                month = "07";
            }
            else if (month == "Aug")
            {
                month = "08";
            }
            else if (month == "Sep")
            {
                month = "09";
            }
            else if (month == "Oct")
            {
                month = "10";
            }
            else if (month == "Nov")
            {
                month = "11";
            }
            else
            {
                month = "12";
            }

            return month;
        }
    }
}
