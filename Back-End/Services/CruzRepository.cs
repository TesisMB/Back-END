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
    //_context me va a permitir poder conectarme a la Base de datos y poder hacer implementar los metodos  
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
    }

    //listo los usuarios por id
    public Users GetUser(int UserID)
    {
        if (UserID.ToString() == "") // si el usuario esta vacio
        {
            throw new ArgumentNullException(nameof(UserID));
        }

        //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
        return _context.Users
             .Include(i => i.Roles)
             .FirstOrDefault(a => a.UserID == UserID);
    }

    //Añadir un nuevo usuario
    public void AddUser(Users user)
    {
        //Verifico que el Usuario no sea null
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        //Despues tambien verifico que no existan dos Dni iguales en la Base de datos
        if (_context.Users.Any(a => a.UserDni == user.UserDni))
        {
            throw new ArgumentException();
        }

        //Se retorna al Controller que no hay errores
        _context.Users.Add(user);
    }


    //Metodo para verificar si un usuario existe

    //NO ES USADO POR AHORA PERO EN EL FUTURO PUEDE LLEGAR A SERVIR
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
            // Disponer de recurso cuando sea necesario
        }
    }


    //Metodo para eliminar cada uno de los Usuarios en base a su Id.
    public void DeleteUser(Users user)
    {
        if (user == null) //Verifico que el Usuario no sea null
        {
            throw new ArgumentNullException(nameof(user));
        }
        //Se retorna al Controller que no hay errores
        _context.Users.Remove(user);
    }

    //Por el momento no es necesario añadirle nada solo llamar a la funcion
    public void UpdateUser(Users user)
    {
    }

   
}

