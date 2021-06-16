using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Users___Dto.Users___Persons;
using Back_End.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICruzRojaRepository<Employees> _cruzRojaRepository;
        private readonly ICruzRojaRepository<Users> _cruzRojaRepository_2;

        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/
        public EmployeesController(ICruzRojaRepository<Employees> EmployeesRepository, ICruzRojaRepository<Users> UsersRepository, IMapper mapper)

        {
            _cruzRojaRepository = EmployeesRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));

            _cruzRojaRepository_2 = UsersRepository ??
              throw new ArgumentNullException(nameof(UsersRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult<IEnumerable<EmployeesDto>> GetList()
        {
            {
                var usersFromRepo = _cruzRojaRepository.GetList();
                return Ok(_mapper.Map<IEnumerable<EmployeesDto>>(usersFromRepo));
            }
        }


        [HttpGet("{employeeId}")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetEmployee(int employeeId)
        {
            var usersFromRepo = _cruzRojaRepository.GetListId(employeeId);

            //Si el userID no existe se retorna NotFound.
            if (usersFromRepo == null)
            {
                return NotFound();
            }

            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
            return Ok(_mapper.Map<EmployeesDto>(usersFromRepo));
        }

        
        [HttpPost]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public ActionResult<EmployeesDto> CreateUser(EmployeesForCreationDto user)
        {
            //Realizo un mapeo entre Users - UsersForCreationDto 
            var userEntity = _mapper.Map<Employees>(user);

            //Al crear un Usuario se encripta dicha contraseña para mayor seguridad.
            userEntity.Users.UserPassword = Encrypt.GetSHA256(userEntity.Users.UserPassword);

            _cruzRojaRepository.Add(userEntity);
            _cruzRojaRepository.save();

            //var usertToReturn = _mapper.Map<UsersDto>(userEntity);

            return Ok();
        }

        
        [HttpPatch("{employeeId}")]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public ActionResult UpdatePartialUser(int employeeId, JsonPatchDocument<EmployeeForUpdateDto> patchDocument)
        {
            var userFromRepo = _cruzRojaRepository.GetListId(employeeId);
            if(userFromRepo == null) 
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<EmployeeForUpdateDto>(userFromRepo);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            string Pass = userToPatch.Users.UserPassword;
            //string ePass = Encrypt.GetSHA256(Pass);

            if(userFromRepo.Users.UserPassword != Pass) { 
            //Nuevamente se debea encriptar la contraseña ingresada
            userToPatch.Users.UserPassword = Encrypt.GetSHA256(userToPatch.Users.UserPassword);
            }
          

            _mapper.Map(userToPatch, userFromRepo);

            _cruzRojaRepository.Update(userFromRepo);

            _cruzRojaRepository.save();

            return NoContent();
        }
        
        //Eliminar un Usuario particular en base al Id proporcionado del mismo
        [HttpDelete("{userId}")]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult DeleteUser(int userId)
        {

            var userFromRepo = _cruzRojaRepository_2.GetListId(userId);


            // si el Id del Usuario no existe de retorna Error.
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository_2.Delete(userFromRepo);

            _cruzRojaRepository_2.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return NoContent();
        }
    }

}
