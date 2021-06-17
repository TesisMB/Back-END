using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Back_End.ResourceParameters;
using Back_End.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly ICruzRojaRepository<Volunteers> _cruzRojaRepository;
        private readonly ICruzRojaRepository<Users> _cruzRojaRepository_2;

        private readonly IMapper _mapper;

        public VolunteersController(ICruzRojaRepository<Volunteers> volunteersRepository,ICruzRojaRepository<Users> UsersRepository, IMapper mapper)
        {
            _cruzRojaRepository = volunteersRepository ?? throw new ArgumentNullException(nameof(volunteersRepository));


            _cruzRojaRepository_2 = UsersRepository ??
              throw new ArgumentNullException(nameof(UsersRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<VolunteersDto>> GetList([FromQuery] VolunteersResourceParameters volunteersResourceParameters)
        {
            var volunterFromRepo = _cruzRojaRepository.GetList(volunteersResourceParameters);

            return Ok(_mapper.Map<IEnumerable<VolunteersDto>>(volunterFromRepo));
        }

        [HttpGet("{volunteerId}")]
        public IActionResult GetVolunteer(int volunteerId)
        {
            var volunteerFromRepo = _cruzRojaRepository.GetListId(volunteerId);

            if (volunteerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VolunteersDto>(volunteerFromRepo));
        }

       [HttpPost]
        public ActionResult<VolunteersDto> CreateVolunteer(VolunteersForCreationDto volunteer)
        {
            var volunteerEntity = _mapper.Map<Volunteers>(volunteer);

            volunteerEntity.Users.UserPassword = Encrypt.GetSHA256(volunteerEntity.Users.UserPassword);

            _cruzRojaRepository.Add(volunteerEntity);
            _cruzRojaRepository.save();

            return Ok();
        }

        [HttpPatch("{volunteerId}")]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public ActionResult UpdatePartialUser(int volunteerId, JsonPatchDocument<VolunteersForUpdatoDto> patchDocument)
        {
            var userFromRepo = _cruzRojaRepository.GetListId(volunteerId);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<VolunteersForUpdatoDto>(userFromRepo);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            string Pass = userToPatch.Users.UserPassword;
            //string ePass = Encrypt.GetSHA256(Pass);

            if (userFromRepo.Users.UserPassword != Pass)
            {
                //Nuevamente se debea encriptar la contraseña ingresada
                userToPatch.Users.UserPassword = Encrypt.GetSHA256(userToPatch.Users.UserPassword);
            }


            _mapper.Map(userToPatch, userFromRepo);

            _cruzRojaRepository.Update(userFromRepo);

            _cruzRojaRepository.save();

            return NoContent();
        }

        [HttpDelete("{volunteerId}")]
        public ActionResult DeleteVolunteer(int volunteerId)
        {

            var userFromRepo = _cruzRojaRepository_2.GetListVolunteerId(volunteerId);


            // si el Id del Usuario no existe de retorna Error.
            if (userFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository_2.Delete(userFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return NoContent();
        }
    }
}
