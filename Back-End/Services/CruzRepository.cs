using Microsoft.EntityFrameworkCore;
using Back_End.Entities;
using Back_End.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public class CruzRepository
    {
    }
}
//Esta clase se va encaragar de implementar todos los metodos definidos en la interfaz ICruzRojaRepository
public class CruzRojaRepository : ICruzRojaRepository, IDisposable
{

    public readonly CruzRojaContext2 _context;

    public CruzRojaRepository(CruzRojaContext2 context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
    }


    //listo todos los usuarios
   public IEnumerable<Users> GetUsers()
    {
        //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
        return _context.Users
                .Include(i => i.Roles)
                .ToList();

        //_context.Users.ToList<Users>();
    }

    //listo los usuarios por id
    public Users GetUser(int UserID)
    {
        if (UserID.ToString() == "")
        {
            throw new ArgumentNullException(nameof(UserID));
        }

        return _context.Users

             .Include(i => i.Roles)
             .FirstOrDefault(a => a.UserID == UserID);

    }


    //Añadir un nuevo usuario
    public void AddUser(Users user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Users.Add(user);
    }



    //Metodo para verificar si un usuario existe
    public bool UserExists(int UserID)
    {
        if (UserID.ToString() == "") // si el usuario esta vacio
        {
            throw new ArgumentNullException(nameof(UserID));
        }

        return _context.Users.Any(a => a.UserID == UserID);
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
            // dispose resources when needed
        }
    }


    //Metodo para eliminar cada uno de los Usuarios en base a su Id.
    public void DeleteUser(Users user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Users.Remove(user);
    }

    public void UpdateUser(Users user)
    {
        /*      if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Update(user);*/
    }
}

