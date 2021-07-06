using AutoMapper;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Volunteers__Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Back_End.Controllers
{
    [Route("")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public VolunteersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [Route("api/Volunteers")]
        [HttpGet]
        public IActionResult GetAllVolunteers()
        {
            try
            {
                var volunteers = _repository.Volunteers.GetAllVolunteers();

                _logger.LogInfo($"Returned all Volunteers from database.");

                var volunteersResult = _mapper.Map<IEnumerable<VolunteersDto>>(volunteers);

                return Ok(volunteersResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllVolunteers action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        [Route("api/app/Volunteers")]
        [HttpGet]
        public IActionResult GetAllVolunteersApp()
        {
            try
            {
                var volunteers = _repository.Volunteers.GetAllVolunteersApp();

                _logger.LogInfo($"Returned all Volunteers from database.");

                var volunteersResult = _mapper.Map<IEnumerable<VolunteersAppDto>>(volunteers);

                return Ok(volunteersResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllVolunteersApp action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [Route("api/Volunteers/{volunteerId}")]
        [HttpGet]
        public IActionResult GetVolunteer(int volunteerId)
        {
            try
            {

                var volunteer = _repository.Volunteers.GetVolunteerWithDetails(volunteerId);

                if (volunteer == null)

                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned volunteer with id: {volunteerId}");
                    var volunteerResult = _mapper.Map<VolunteersDto>(volunteer);
                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [Route("api/Volunteers")]
        [HttpPost]
        public IActionResult CreateVolunteer([FromBody] VolunteersForCreationDto volunteer)
        {
            try
            {
                if (volunteer == null)

                {
                    _logger.LogError("Volunteer object sent from client is null.");
                    return BadRequest("Volunteer object is null");

                }

                var volunteerEntity = _mapper.Map<Volunteers>(volunteer);

                // Al crear un Usuario se encripta dicha contraseña para mayor seguridad.
                volunteerEntity.Users.UserPassword = Encrypt.GetSHA256(volunteerEntity.Users.UserPassword);

                _repository.Volunteers.Create(volunteerEntity);

                _repository.Save();

                var createdVolunteer = _mapper.Map<VolunteersDto>(volunteerEntity);

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //[Authorize(Roles = "Coordinador General, Admin")] 
        [Route("api/Volunteers/{volunteerId}")]
        [HttpPatch]
        public IActionResult UpdatePartialUser(int volunteerId, JsonPatchDocument<VolunteersForUpdatoDto> patchDocument)
        {

            var userFromRepo = _repository.Volunteers.GetVolunteersById(volunteerId);
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

            _repository.Volunteers.Update(userFromRepo);

            _repository.Save();

            return NoContent();
        }


        [Route("api/Volunteers/{volunteerId}")]
        [HttpDelete]
        public IActionResult DeleteVolunteer(int volunteerId)
        {
            try
            {
            var volunteer = _repository.Users.GetUserVolunteerById(volunteerId);


            if (volunteer == null)
            {
                 _logger.LogError($"Volunteer with id: {volunteerId}, hasn't ben found in db.");
                 return NotFound();
            }

            _repository.Users.Delete(volunteer);

            _repository.Save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVolunteer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
