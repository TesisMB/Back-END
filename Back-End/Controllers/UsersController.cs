using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Back_End.Models;
using Back_End.Services;
using Back_End.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {

        private readonly ICruzRojaRepository _cruzRojaRepository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/

        public UsersController(ICruzRojaRepository cruzRojaRepository, IMapper mapper)

        {
            _cruzRojaRepository = cruzRojaRepository ??
                throw new ArgumentNullException(nameof(cruzRojaRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        // Listar Usuarios de forma completa
        [HttpGet]
       [Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult<IEnumerable<UsersDto>> GetUsers()
        {
            {
                var usersFromRepo = _cruzRojaRepository.GetUsers();

                //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecindibles
                return Ok(_mapper.Map<IEnumerable<UsersDto>>(usersFromRepo));
            }
        }


        //Listar Usuarios por Id
        //Name me permite interactuar con el Post para crear un nuevo Id para el usuario solicitado
        [HttpGet("{UserID}", Name = "GetUser")]
        [Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetUser(int UserID)
        {
            var usersFromRepo = _cruzRojaRepository.GetUser(UserID);


            //Si el Id del Usuario no existe se retorna Error.
            if (usersFromRepo == null)
            {
                return NotFound();
            }

            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
            return Ok(_mapper.Map<UsersDto>(usersFromRepo));
        }


        //Agregar un nuevo Usuario y devolve el Id Creado del Usuario
        [HttpPost]
        [Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult<UsersDto> CreateUser(UsersForCreationDto user)
        {
            //Se usa User para posteriormente almacenar los valores ingresados a la Base de datos
            var userEntity = _mapper.Map<Entities.Users>(user);

            /*llamo al metodo AddUser para comprobar que los datos que se ingresaron 
             del nuevo Usuario cumplan con los requisitos*/
            _cruzRojaRepository.AddUser(userEntity);
            _cruzRojaRepository.save();

            /*Una vez comprobado con exito todo se procede a realizar el mapeo 
            que va a permitir manipular como se devuelven los datos */
            var authorToReturn = _mapper.Map<UsersDto>(userEntity);

            //La Operacion de añadir un Usuario se retorna con exito
            return Ok();
        }

        //Utilizo este metodo para actualizar los datos que son posibles modificar (Phone-Password-Email)
        [HttpPatch("{UserID}")]
        [Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]

        public ActionResult UpdatePartialUser(int UserID, JsonPatchDocument<UsersForUpdate> patchDocument)
        {
            var userFromRepo = _cruzRojaRepository.GetUser(UserID);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<UsersForUpdate>(userFromRepo);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userToPatch, userFromRepo);

            _cruzRojaRepository.UpdateUser(userFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la actualizacion del Usuario especificado
            return Ok();
        }

        //Eliminar un Usuario particular en base al Id proporcionado del mismo
        [HttpDelete("{UserID}")]
        [Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult DeleteUser(int UserID)
        {

            var userFromRepo = _cruzRojaRepository.GetUser(UserID);


            // si el Id del Usuario no existe de retorna Error.
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository.DeleteUser(userFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return Ok();
        }
    }
}

