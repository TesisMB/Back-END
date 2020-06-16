using Back_End.Models;
using System.Collections.Generic;

namespace Back_End.Services
{

    //La interfaz va a ser la que lleve todos los Metodos necesarios para realizar el CRUD en Users
    public interface ICruzRojaRepository
    {

        //lista completa de usuarios
        IEnumerable<Users> GetUsers();

        //lista de usuarios por Id
        Users GetUser(int userId);

        //Añadir un nuevo Usuario
        void AddUser(Users user);

        //Actualizar datos de un Usuario
        void UpdateUser(Users user);

        //Eliminar un Usuario
        void DeleteUser(Users user);

        //Metodo para verificar la existenica de un usuario
        bool UserExists(int userId);

        //Metodo para poder verificar que los datos a guardar no falte.
        bool save();
    }
}