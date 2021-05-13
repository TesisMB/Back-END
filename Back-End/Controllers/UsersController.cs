using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICruzRojaRepository<Users> _cruzRojaRepository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/
        public UsersController(ICruzRojaRepository<Users> UsersRepository, IMapper mapper)

        {
            _cruzRojaRepository = UsersRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult<IEnumerable<UsersDto>> GetList()
        {
            {
                var usersFromRepo = _cruzRojaRepository.GetList();
                return Ok(_mapper.Map<IEnumerable<UsersDto>>(usersFromRepo));
            }
        }


        [HttpGet("{userId}")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetUser(int userId)
        {
            var usersFromRepo = _cruzRojaRepository.GetListId(userId);

            //Si el userID no existe se retorna NotFound.
            if (usersFromRepo == null)
            {
                return NotFound();
            }

            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
            return Ok(_mapper.Map<UsersDto>(usersFromRepo));
        }

        [HttpPost("{Register}")]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public ActionResult<UsersDto> CreateUser(UsersForCreationDto user)
        {

            //Realizo un mapeo entre Users - UsersForCreationDto 
            var userEntity = _mapper.Map<Users>(user);

            //Al crear un Usuario se encripta dicha contraseña para mayor seguridad.
            userEntity.UserPassword = Encrypt.GetSHA256(userEntity.UserPassword);

            _cruzRojaRepository.Add(userEntity);
            _cruzRojaRepository.save();

            var usertToReturn = _mapper.Map<UsersDto>(userEntity);

            return Ok();
        }

        [HttpPatch("Update/{userId}")]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public ActionResult UpdatePartialUser(int userId, JsonPatchDocument<UsersForUpdate> patchDocument)
        {
            var userFromRepo = _cruzRojaRepository.GetListId(userId);
            if(userFromRepo == null) 
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<UsersForUpdate>(userFromRepo);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            string Pass = userToPatch.UserPassword;
            //string ePass = Encrypt.GetSHA256(Pass);

            if(userFromRepo.UserPassword != Pass) { 
            //Nuevamente se debea encriptar la contraseña ingresada
            userToPatch.UserPassword = Encrypt.GetSHA256(userToPatch.UserPassword);
            }

            _mapper.Map(userToPatch, userFromRepo);

            _cruzRojaRepository.Update(userFromRepo);

            _cruzRojaRepository.save();

            return NoContent();
        }
        //Eliminar un Usuario particular en base al Id proporcionado del mismo
      
        [HttpDelete("Delete/{userId}")]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult DeleteUser(int userId)
        {

            var userFromRepo = _cruzRojaRepository.GetListId(userId);


            // si el Id del Usuario no existe de retorna Error.
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository.Delete(userFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return NoContent();
        }
    }

}
