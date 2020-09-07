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

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {

        private readonly ICruzRojaRepository _cruzRojaRepository;
        private readonly IMapper _mapper;

        public UsersController(ICruzRojaRepository cruzRojaRepository, IMapper mapper)

        {
            _cruzRojaRepository = cruzRojaRepository ??
                throw new ArgumentNullException(nameof(cruzRojaRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        // Listar Usuarios de forma completa
        [HttpGet]
       [Authorize(Policy = "ListUsers")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
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
        [Authorize(Policy = "ListUserId")]
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
        [Authorize(Policy = "AddNewUser")]
        public ActionResult<UsersDto> CreateUser(UsersForCreationDto user)
        {

            //Realizo un mapeo entre Users - UsersForCreationDto 
            var userEntity = _mapper.Map<Entities.Users>(user);
            _cruzRojaRepository.AddUser(userEntity);
            _cruzRojaRepository.save();

            var authorToReturn = _mapper.Map<UsersDto>(userEntity);


            return Ok();

            //devuelvo una nueva ruta donde se genera un Id nuevo para ese usuario añadido.
            /*   return CreatedAtRoute("GetUser",
                 new { userId = authorToReturn.IdUser },
                    authorToReturn); */
        }


        //Este metodo permite actualizar y modificar todos los datos de los Usuarios que estan en el Sistema
        [HttpPut("{UserID}")]
        public ActionResult UpdateUser(int UserID, UsersForUpdate user)
        {
            var userFromRepo = _cruzRojaRepository.GetUser(UserID);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(user, userFromRepo);

            _cruzRojaRepository.UpdateUser(userFromRepo);

            _cruzRojaRepository.save();

            return Ok();
        }

        //se debe crear otro Patch en donde solamente se pueda actualizar el Rol del Usuario

        //Utilizo este metodo para actualizar los datos que son posibles modificar (Phone-Password-Email)
        [HttpPatch("{UserID}")]
        [Authorize(Policy = "UpdateUser")]

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

            return Ok();

        }



        //Eliminar un Usuario particular en base al Id proporcionado del mismo
        [HttpDelete("{UserID}")]
        [Authorize(Policy = "DeleteUser")]

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

