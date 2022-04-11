using AutoMapper;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Vehicles___Dto;
using Entities.DataTransferObjects.Vehicles___Dto.Update;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/Vehiculos")]
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
        public async Task<ActionResult<Vehicles>> GetAllVehicles([FromQuery] vehiclesFiltersDto vehiclesFilters)
        {
            try
            {
                var vehicles = await _repository.Vehicles.GetAllVehiclesFilters(vehiclesFilters);
                _logger.LogInfo($"Returned all vehicles from database.");

                var employeesResult = _mapper.Map<IEnumerable<Resources_Dto>>(vehicles);


                foreach (var item in employeesResult)
                {
                    if (item.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        item.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, item.Picture);
                    }

                }

                return Ok(employeesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVehicles action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }


        [HttpGet("{vehicleId}")]
        public async Task<ActionResult<Vehicles>> GetVehicle(int vehicleId)
        {
            try
            {
                var vehicle = await _repository.Vehicles.GetVehicleWithDetails(vehicleId);

                if (vehicle == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't been found in db.");
                    return NotFound();
                }

                else
                {
                    _logger.LogInfo($"Returned vehicle with id: {vehicleId}");



                    var vehicleResult = _mapper.Map<Resources_Dto>(vehicle);

                    if (vehicleResult.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        vehicleResult.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, vehicleResult.Picture);
                    }

                    return Ok(vehicleResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVehicleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vehicles>> CreateVehicle([FromBody] Resources_ForCreationDto vehicle)
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



                if (vehicle.Picture == null)
                {
                    vehicleEntity.VehiclePicture = "https://i.imgur.com/S9HJEwF.png";
                }
                else
                {
                    vehicleEntity.VehiclePicture = await UploadController.SaveImage(vehicle.ImageFile, "Resources");
                }

                vehicleEntity.VehicleAvailability = true;
                vehicleEntity.VehicleDonation = vehicle.Donation;
                vehicleEntity.VehicleQuantity = 1;
                vehicleEntity.VehicleDescription = vehicle.Description;

                vehicleEntity.VehiclePatent = vehicle.Vehicles.VehiclePatent;

                vehicleEntity.VehicleYear = vehicle.Vehicles.VehicleYear;

                vehicleEntity.VehicleUtility = vehicle.Vehicles.VehicleUtility;

                vehicleEntity.FK_EmployeeID = vehicle.Vehicles.FK_EmployeeID;

                vehicleEntity.Fk_TypeVehicleID = vehicle.Vehicles.Fk_TypeVehicleID;

                if (vehicle.Vehicles.TypeVehicles != null)
                {
                    vehicleEntity.TypeVehicles = new TypeVehicles();
                    vehicleEntity.TypeVehicles.Type = vehicle.Vehicles.TypeVehicles.Type;
                }

                vehicleEntity.BrandsModels = new BrandsModels();


                vehicleEntity.BrandsModels.FK_BrandID = vehicle.Vehicles.BrandsModels.FK_BrandID;
                vehicleEntity.BrandsModels.FK_ModelID = vehicle.Vehicles.BrandsModels.FK_ModelID;


                if (vehicle.Vehicles.BrandsModels.Brands != null || vehicle.Vehicles.BrandsModels.Model != null)
                {
                    vehicleEntity.BrandsModels.Brands = new Brands();
                    vehicleEntity.BrandsModels.Model = new Model();
                    vehicleEntity.BrandsModels.Brands.BrandName = vehicle.Vehicles.BrandsModels.Brands.BrandName;
                    vehicleEntity.BrandsModels.Model.ModelName = vehicle.Vehicles.BrandsModels.Model.ModelName;
                }

                _repository.Vehicles.CreateVehicle(vehicleEntity);

                _repository.Vehicles.SaveAsync();

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{vehicleId}")]
        public async Task<ActionResult> UpdateVehicle(int vehicleId, JsonPatchDocument<Resources_ForCreationDto> patchDocument)
        {
            try
            {
                var vehicleEntity = await _repository.Vehicles.GetVehicleById(vehicleId);

                if (vehicleEntity == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't been found in db.");
                    return NotFound();
                }

                var vehicleToPatch = _mapper.Map<Resources_ForCreationDto>(vehicleEntity);

                patchDocument.ApplyTo(vehicleToPatch, ModelState);


                if (!TryValidateModel(vehicleToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                VehiclesForUpdateDto vehicles = new VehiclesForUpdateDto();

                vehicles.VehicleAvailability = vehicleToPatch.Availability;
                vehicles.VehicleDonation = vehicleToPatch.Donation;
                vehicles.VehicleDescription = vehicleToPatch.Description;
                vehicles.VehicleUtility = vehicleToPatch.Vehicles.VehicleUtility;
                vehicles.FK_EstateID = vehicleToPatch.FK_EstateID;
                vehicles.FK_EmployeeID = vehicleToPatch.Vehicles.FK_EmployeeID;
                vehicles.VehicleYear = vehicleToPatch.Vehicles.VehicleYear;
                vehicles.VehiclePatent = vehicleToPatch.Vehicles.VehiclePatent;


                var vehicleResult = _mapper.Map(vehicles, vehicleEntity);

                _repository.Vehicles.Update(vehicleResult);

                _repository.Vehicles.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{vehicleId}")]
        public async Task<ActionResult> DeleteVehicle(int vehicleId)
        {
            try
            {
                var vehicle = await _repository.Vehicles.GetVehicleById(vehicleId);

                if (vehicle == null)
                {
                    _logger.LogError($"Vehicle with id: {vehicleId}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.Vehicles.DeleteVehicle(vehicle);

                _repository.Vehicles.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVehicle action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
