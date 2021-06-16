using Back_End.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public class UsersRepository : ICruzRojaRepository<Users>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public UsersRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));

        }

        public void Add(Users TEntity)
        {
            throw new NotImplementedException();
        }

        public Users GetListId(int UserID)
        {
            if (UserID.ToString() == "") // si el usuario esta vacio
            {
                throw new ArgumentNullException(nameof(UserID));
            }

            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
            return _context.Users
                   .Include(i => i.Persons)
                   .ThenInclude(i => i.Users.Estates)
                   .ThenInclude(i => i.LocationAddress)
                   .ThenInclude(i => i.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                   .Include(i => i.Roles)
                   .Include(i=>i.Employees)
                   .FirstOrDefault(a => a.ID == UserID);
        }
    

       public void Delete(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
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


        public IEnumerable<Users> GetList()
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }


        public void Update(Users TEntity)
        {
            throw new NotImplementedException();
        }

        public Users GetListVolunteerId(int volunteerID)
        {
            if (volunteerID.ToString() == "")
            {
                throw new ArgumentNullException(nameof(volunteerID));
            }

            return _context.Users
                  .Include(a => a.Roles)
                  .Include(a => a.Persons)
                  .ThenInclude(a => a.Users.Volunteers)
                  .ThenInclude(a => a.VolunteersSkills)
                  .ThenInclude(a => a.Skills)
                 .FirstOrDefault(a => a.ID == volunteerID);
        }
    }
}