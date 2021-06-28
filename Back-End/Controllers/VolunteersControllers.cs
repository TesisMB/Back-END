using AutoMapper;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Contracts.Interfaces;
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

        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
            private readonly IMapper _mapper;

        public VolunteersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

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
            catch(Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllVolunteers action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }

            //var volunterFromRepo = _cruzRojaRepository.GetList(volunteersResourceParameters);

            //return Ok(_mapper.Map<IEnumerable<VolunteersDto>>(volunterFromRepo));
        }


        [HttpGet("{volunteerId}")]
        public IActionResult GetVolunteer(int volunteerId)
        {
            try
            {

            var volunteer = _repository.Volunteers.GetVolunteersById(volunteerId);

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

            } catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

       [HttpPost]
        public IActionResult CreateVolunteer([FromBody] VolunteersForCreationDto volunteer)
        {
            try
            {
                if(volunteer == null)

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

            } catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpPatch("{volunteerId}")]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public ActionResult UpdatePartialUser(int volunteerId, JsonPatchDocument<VolunteersForUpdatoDto> patchDocument)
        {
            try
            {

                var volunteerEntity = _repository.Volunteers.GetVolunteersById(volunteerId);

                if (volunteerEntity == null)
                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't been found in db.");

                    return NotFound();
                }


                var volunteerToPatch = _mapper.Map<VolunteersForUpdatoDto>(volunteerEntity);


                patchDocument.ApplyTo(volunteerToPatch, ModelState);

                var userNewPass = volunteerToPatch.Users.UserNewPassword;

                if (!TryValidateModel(volunteerToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                volunteerToPatch.Users.UserNewPassword = Encrypt.GetSHA256(userNewPass);


                var volunteerResult = _mapper.Map(volunteerToPatch, volunteerEntity);

                volunteerResult.Users.UserPassword = volunteerToPatch.Users.UserNewPassword;

                _repository.Volunteers.Update(volunteerResult);
                _repository.Save();

                return NoContent();
            }

            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateVolunteer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpDelete("{volunteerId}")]
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
