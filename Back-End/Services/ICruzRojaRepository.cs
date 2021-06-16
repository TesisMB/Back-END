using Back_End.Entities;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    //La interfaz va a ser la que lleve todos los Metodos necesarios para realizar los CRUD del Sistema.
    public interface ICruzRojaRepository<TEntity>
    {

        //lista completa de usuarios
        IEnumerable<TEntity> GetList();

        //lista de usuarios por Id
        TEntity GetListId(int TEntity);

        TEntity GetListVolunteerId(int TEntity);

        //Añadir un nuevo Usuario
        void Add(TEntity TEntity);

        //Actualizar datos de un Usuario
        void Update(TEntity TEntity);

        //Eliminar un Usuario
        void Delete(TEntity TEntity);


        //Metodo para verificar la existenica de un usuario
        //bool UserExists(int UserID);

        //Metodo para poder verificar que los datos a guardar no falte.
        bool save();


    }
}