using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergenciesDisastersController : ControllerBase
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositorWrapper _repository;
        public EmergenciesDisastersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmegenciesDisasters()
        {
            try
            {
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisasters();
                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersDto>>(emergenciesDisasters);

                return Ok(emergenciesDisastersResult);

            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpGet("{emegencyDisasterID}")]
        public async Task<ActionResult<EmergenciesDisasters>> GetEmegencyDisasterIDWithDetails(int emegencyDisasterID)
        {
            try
            {
                var emegencyDisaster =  await _repository.EmergenciesDisasters.GetEmergencyDisasterWithDetails(emegencyDisasterID);
           
                if(emegencyDisaster == null)
                {
                    _logger.LogError($"EmergenciesDisasters with id: {emegencyDisasterID}, hasn't been found in db.");

                    return NotFound();
                }

                _logger.LogInfo($"Returned emegencyDisaster with details for id: {emegencyDisasterID}");

                var emergencyDisasterResult = _mapper.Map<EmergenciesDisastersDto>(emegencyDisaster);

                return Ok(emergencyDisasterResult);
            
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmegencyDisasterIDWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<EmergenciesDisasters> CreateEmergencyDisaster([FromBody] EmergenciesDisastersForCreationDto emergenciesDisasters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (emergenciesDisasters == null)
                {
                    _logger.LogError("EmergencyDisaster object sent from client is null.");
                    return BadRequest("EmergencyDisaster object is null");
                }
            

            var emergencyDisaster = _mapper.Map<EmergenciesDisasters>(emergenciesDisasters);

            _repository.EmergenciesDisasters.CreateEmergencyDisaster(emergencyDisaster);

             _repository.EmergenciesDisasters.SaveAsync();

            return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{emegencyDisasterID}")]
        public async Task<ActionResult> UpdatePartialEmegencyDisaster(int emegencyDisasterID, JsonPatchDocument<EmergenciesDisastersForUpdateDto> _emergencyDisaster)
        {
            try
            {
                var emergencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);
             
                if (emergencyDisaster == null)
                {
                    return NotFound();
                }

                var emergencyDisasterToPatch = _mapper.Map<EmergenciesDisastersForUpdateDto>(emergencyDisaster);

                _emergencyDisaster.ApplyTo(emergencyDisasterToPatch, ModelState);

                if (!TryValidateModel(emergencyDisasterToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                var emergencyDisasterResult = _mapper.Map(emergencyDisasterToPatch, emergencyDisaster);

                _repository.EmergenciesDisasters.UpdateEmergencyDisaster(emergencyDisasterResult);

                _repository.EmergenciesDisasters.SaveAsync();

                return NoContent();
            }

            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePartialEmegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{emegencyDisasterID}")]
        public async Task<ActionResult> DeletEmegencyDisaster(int emegencyDisasterID)
        {
            try
            {

            var emegencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);

            if(emegencyDisaster == null)
            {
                    _logger.LogError($"EmergencyDisaster with id: {emegencyDisasterID}, hasn't ben found in db.");

                    return NotFound();
            }


                _repository.EmergenciesDisasters.DeleteEmergencyDisaster(emegencyDisaster);

                _repository.EmergenciesDisasters.SaveAsync();

                return NoContent();

            }

            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletemegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


    }
}
