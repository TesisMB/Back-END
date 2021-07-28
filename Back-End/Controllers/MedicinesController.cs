using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.Helpers;
using Entities.Models;
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
    public class MedicinesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public MedicinesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllMedicines()
        {
        

            try
            {
                var volunteers = _repository.Medicines.GetAllMedicines();

                _logger.LogInfo($"Returned all Materials from database.");

                var volunteersResult = _mapper.Map<IEnumerable<MedicinesDto>>(volunteers);

                return Ok(volunteersResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllMaterials action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        // GET api/<MedicinesControllers>/5
        [HttpGet("{medicineId}")]
        public IActionResult GetMedicineWithDetails(int medicineId)
        {
            try
            {
                var employee = _repository.Medicines.GetMedicinelWithDetails(medicineId);

                if (employee == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned medicine with details for id: {medicineId}");

                    var employeeResult = _mapper.Map<MedicinesDto>(employee);
                    return Ok(employeeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] MedicineForCreationDto medicine)
        {
            try
            {

              /*  if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }*/

                if (medicine == null)
                {
                    _logger.LogError("Medicine object sent from client is null.");
                    return BadRequest("Medicine object is null");
                }

                var medicineEntity = _mapper.Map<Medicines>(medicine);

                _repository.Medicines.Create(medicineEntity);

                //_repository.Save();

                var createdVehicle = _mapper.Map<MedicinesDto>(medicineEntity);

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPatch("{medicineId}")]
        public IActionResult UpdateVehicle(int medicineId, JsonPatchDocument<MedicineForUpdateDto> patchDocument)
        {
            try
            {
                var medicineEntity = _repository.Medicines.GetMedicineById(medicineId);

                if (medicineEntity == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }

                var medicineToPatch = _mapper.Map<MedicineForUpdateDto>(medicineEntity);

                patchDocument.ApplyTo(medicineToPatch, ModelState);


                if (!TryValidateModel(medicineToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                var medicineResult = _mapper.Map(medicineToPatch, medicineEntity);

                _repository.Medicines.Update(medicineResult);

               // _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<MedicinesControllers>/5
        [HttpDelete("{medicineId}")]
            public IActionResult DeleteMedicine(int medicineId)
            {
                try
                {
                    var medicine = _repository.Medicines.GetMedicineById(medicineId);

                    if (medicine == null)
                    {
                        _logger.LogError($"MedicineId with id: {medicineId}, hasn't ben found in db.");
                        return NotFound();
                    }

                    _repository.Medicines.Delete(medicine);

                   // _repository.Save();

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
