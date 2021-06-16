using Back_End.Entities;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public class EmployeesRepository : ICruzRojaRepository<Employees>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public EmployeesRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public IEnumerable<Employees> GetList()
        {
            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
            return _context.Employees
                    .Include(i => i.Users)
                    .ThenInclude(i => i.Roles)
                    .Include(i => i.Users.Persons)
                    .ThenInclude(i => i.Users.Estates)
                    .ThenInclude(i => i.LocationAddress)
                    .ThenInclude(i => i.Estates.EstatesTimes)
                    .ThenInclude(a => a.Times)
                    .ThenInclude(a => a.Schedules)
                    .ToList();
        }

        public Employees GetListId(int EmployeeID)
        {
            if (EmployeeID.ToString() == "") // si el usuario esta vacio
            {
                throw new ArgumentNullException(nameof(EmployeeID));
            }

            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
            return _context.Employees
                   .Include(i => i.Users)
                   .ThenInclude(i => i.Roles)
                   .Include(i => i.Users.Persons)
                   .ThenInclude(i => i.Users.Estates)
                   .ThenInclude(i => i.LocationAddress)
                   .ThenInclude(i => i.Estates.EstatesTimes)
                   .ThenInclude(a => a.Times)
                   .ThenInclude(a => a.Schedules)
                .FirstOrDefault(a => a.ID == EmployeeID);
        }


        public void Add(Employees user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Employees.Add(user);
        }


        public void Delete(Employees user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Employees.Remove(user);
        }


        //metodo para verificar que todos los datos  a almacenar esten, caso contrario marco un Error.
        public bool save()
        {
            return (_context.SaveChanges() >= 0);
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

        public void Update(Employees employee)
        {
            /*      if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                _context.Users.Update(user);*/
        }

        public Employees GetListVolunteerId(int TEntity)
        {
            throw new NotImplementedException();
        }
    }
    
}
