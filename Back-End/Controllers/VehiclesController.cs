using AutoMapper;
using Back_End.Models;
using Back_End.Models.Vehicles___Dto;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using Entities.DataTransferObjects.Vehicles___Dto.Update;
using Entities.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Back_End.Controllers
{
    [Route("api/Vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;


        public VehiclesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            try
            {
                var vehicles = _repository.Vehicles.GetAllVehicles();
                _logger.LogInfo($"Returned all vehicles from database.");

                var employeesResult = _mapper.Map<IEnumerable<VehiclesDto>>(vehicles);
                return Ok(employeesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVehicles action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }


        [HttpGet("{vehicleId}")]
        public IActionResult GetVehicle(int vehicleId)
        {
            try
            {
                var vehicle = _repository.Vehicles.GetVehicleWithDetails(vehicleId);

                if(vehicle == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't been found in db.");
                    return NotFound();
                }

                else
                {
                    _logger.LogInfo($"Returned vehicle with id: {vehicleId}");
                    var vehicleResult = _mapper.Map<VehiclesDto>(vehicle);
                    return Ok(vehicleResult);
                }

            }catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVehicleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehiclesForCreationDto vehicle)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (vehicle == null)
                {
                    _logger.LogError("Vehicle object sent from client is null.");
                    return BadRequest("Vehicle object is null");
                }

                var vehicleEntity = _mapper.Map<Vehicles>(vehicle);

                _repository.Vehicles.Create(vehicleEntity);

                _repository.Save();

                var createdVehicle = _mapper.Map<VehiclesDto>(vehicleEntity);

                return Ok();

            } catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{vehicleId}")]
        public IActionResult UpdateVehicle(int vehicleId, JsonPatchDocument<VehiclesForUpdateDto> patchDocument)
        {
            try
            {
                var vehicleEntity = _repository.Vehicles.GetVehicleById(vehicleId);

                if(vehicleEntity == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't been found in db.");
                    return NotFound();
                }

                var vehicleToPatch = _mapper.Map<VehiclesForUpdateDto>(vehicleEntity);

                patchDocument.ApplyTo(vehicleToPatch, ModelState);


                if (!TryValidateModel(vehicleToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                var vehicleResult = _mapper.Map(vehicleToPatch, vehicleEntity);

                _repository.Vehicles.Update(vehicleResult);

                _repository.Save();

                return NoContent();
            
            }catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{vehicleId}")]
        public IActionResult DeleteVehicle (int vehicleId)
        {
            try
            {
                var vehicle = _repository.Vehicles.GetVehicleById(vehicleId);

                if(vehicle == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.Vehicles.Delete(vehicle);

                _repository.Save();

                return NoContent();

            }catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
