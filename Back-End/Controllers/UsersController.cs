using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Back_End.Dto;
using Back_End.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

                //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
                return Ok(_mapper.Map<IEnumerable<UsersDto>>(usersFromRepo));

            }
        }


        //Listar Usuarios por Id
        //Name me permite interactuar con el Post para crear un nuevo Id para el usuario solicitado
        [HttpGet("{userId}", Name = "GetUser")]
        [Authorize(Policy = "ListUserId")]
        public IActionResult GetUser(int userId)
        {
            var usersFromRepo = _cruzRojaRepository.GetUser(userId);


            //Si el Ide del Usuario no existe se retorna Error.
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
            var userEntity = _mapper.Map<Models.Users>(user);
            _cruzRojaRepository.AddUser(userEntity);
            _cruzRojaRepository.save();

            var authorToReturn = _mapper.Map<UsersDto>(userEntity);


            return Ok("Usuario Creado Correctamente");

            //devuelvo una nueva ruta donde se genera un Id nuevo para ese usuario añadido.
            /*   return CreatedAtRoute("GetUser",
                 new { userId = authorToReturn.IdUser },
                    authorToReturn); */
        }


        //Este metodo permite actualizar y modificar todos los datos de los Usuarios que estan en el Sistema
        [HttpPut("{userId}")]
        public ActionResult UpdateUser(int userId, UsersForUpdate user)
        {
            var userFromRepo = _cruzRojaRepository.GetUser(userId);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(user, userFromRepo);

            _cruzRojaRepository.UpdateUser(userFromRepo);

            _cruzRojaRepository.save();

            return Ok("Usuario Actualizado Correctamente");
        }

        //se debe crear otro Patch en donde solamente se pueda actualizar el Rol del Usuario

        //Utilizo este metodo para actualizar los datos que son posibles modificar (Phone-Password-Email)
        [HttpPatch("{userId}")]
        [Authorize(Policy = "UpdateUser")]

        public ActionResult UpdatePartialUser(int userId, JsonPatchDocument<UsersForUpdate> patchDocument)
        {
            var userFromRepo = _cruzRojaRepository.GetUser(userId);
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

            return Ok("Usuario Actualizado Correctamente");

        }



        //Eliminar un Usuario particular en base al Id proporcionado del mismo
        [HttpDelete("{userId}")]
        [Authorize(Policy = "DeleteUser")]

        public ActionResult DeleteUser(int userId)
        {

            var userFromRepo = _cruzRojaRepository.GetUser(userId);


            // si el Id del Usuario no existe de retorna Error.
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository.DeleteUser(userFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return Ok("Usuario Eliminado Correctamente");

        }



    }

}
