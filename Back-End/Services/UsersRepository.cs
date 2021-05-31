using Back_End.Entities;
using Back_End.Models;
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

        public IEnumerable<Users> GetList()
        {
            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
            return _context.Users
                    .Include(a=>a.Persons)
                    .ThenInclude(i => i.Users.Roles)
                    .ToList();
        }

        public Users GetListId(int UserID)
        {
            if (UserID.ToString() == "") // si el usuario esta vacio
            {
                throw new ArgumentNullException(nameof(UserID));
            }

            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
            return _context.Users
                   .Include(a=>a.Employees)
                  .Include(a => a.Persons)
                 .Include(i => i.Roles)
                 .FirstOrDefault(a => a.ID == UserID);
        }

        public void Add(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public void Update(Users user)
        {
        }


        public void Delete(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
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
    }
}
