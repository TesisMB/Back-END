using Back_End.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Back_End.Services
{
    public class VolunteersRepository : ICruzRojaRepository<Volunteers>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public VolunteersRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public IEnumerable<Volunteers> GetList()
        {
            return _context.Volunteers
                 .Include(a => a.Users)
                 .ThenInclude(a => a.Persons)
                 .Include(a => a.Users.Roles)
                .Include(a => a.VolunteersSkills)
                 .ThenInclude(a => a.Skills)
                 .Include(a=>a.Users.Estates)
                 .ThenInclude(a=>a.LocationAddress)
                 .ThenInclude(a=>a.Estates.EstatesTimes)
                 .ThenInclude(a=>a.Times)
                 .ThenInclude(a=>a.Schedules)
                 .ToList();
        }
        public Volunteers GetListId(int volunteerId)
        {
            if (volunteerId.ToString() == "")
            {
                throw new ArgumentNullException(nameof(volunteerId));
            }

            return _context.Volunteers
                  .Include(a => a.Users)
                 .ThenInclude(a => a.Persons)
                 .Include(a => a.Users.Roles)
                .Include(a => a.VolunteersSkills)
                 .ThenInclude(a => a.Skills)
                 .Include(a => a.Users.Estates)
                 .ThenInclude(a => a.LocationAddress)
                 .ThenInclude(a => a.Estates.EstatesTimes)
                 .ThenInclude(a => a.Times)
                 .ThenInclude(a => a.Schedules)
                 .FirstOrDefault(a => a.ID == volunteerId);
        }

        public void Add(Volunteers volunteer)
        {
            if (volunteer == null)
            {
                throw new ArgumentNullException(nameof(volunteer));
            }

            _context.Volunteers.Add(volunteer);
        }

        public void Update(Volunteers volunteer)
        {
            /*      if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                _context.Users.Update(user);*/
        }
        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Delete(Volunteers volunteer)
        {
            if (volunteer == null)
            {
                throw new ArgumentNullException(nameof(volunteer));
            }

            _context.Volunteers.Remove(volunteer);
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Disponer de recurso cuando sea necesario
            }
        }

        public Volunteers GetListVolunteerId(int TEntity)
        {
            throw new NotImplementedException();
        }
    }
}
