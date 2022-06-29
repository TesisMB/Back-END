using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Medicines___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/Medicamentos")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public MedicinesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //********************************* FUNCIONANDO *********************************
        [HttpGet]
        public async Task<ActionResult<Medicines>> GetAllMedicines([FromQuery] int userId)
        {
//            var imgBytes = await _repository.Medicines.Get(fileName);
////          return File(imgBytes, "application/pdf ");

//            return File(imgBytes, "image/webp");

            try
            {
                var medicines = await _repository.Medicines.GetAllMedicines(userId);

                _logger.LogInfo($"Returned all Materials from database.");

                var medicinesResult = _mapper.Map<IEnumerable<Resources_Dto>>(medicines);

                foreach (var item in medicinesResult)
                {

                    if (item.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        item.Picture = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{item.Picture}";


                        DateTime date = Convert.ToDateTime(item.Medicines.MedicineExpirationDate);

                        if (date > DateTime.Now)
                        {
                            item.Availability = false;
                        }
                    }
                }

                return Ok(medicinesResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllMaterials action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        //[Route("download")]
        //[HttpGet]
        //public async Task<IActionResult> Download(string fileName)
        //{
        //    var imagBytes = await _repository.Medicines.Get(fileName);
        //    return new FileContentResult(imagBytes, "application/octet-stream")
        //    {
        //        FileDownloadName = Guid.NewGuid().ToString() + ".webp",
        //    };
        //}


        //********************************* FUNCIONANDO *********************************
        [HttpGet("{medicineId}")]
        public async Task<ActionResult<Medicines>> GetMedicineWithDetails(string medicineId)
        {
            try
            {
                var employee = await _repository.Medicines.GetMedicinelWithDetails(medicineId);

                if (employee == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned medicine with details for id: {medicineId}");

                    var employeeResult = _mapper.Map<Resources_Dto>(employee);

                    if (employeeResult.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        employeeResult.Picture = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{employeeResult.Picture}";
                    }

                    DateTime date = Convert.ToDateTime(employeeResult.Medicines.MedicineExpirationDate);

                    if (date < DateTime.Now)
                    {
                        employeeResult.Availability = false;
                    }

                    return Ok(employeeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPost]
        public async Task<ActionResult<Medicines>> CreateMedicine([FromBody] Resources_ForCreationDto medicine)
        {

            //if (medicine.ImageFile != null)
            //{
            //    await _repository.Medicines.Upload(medicine);
            //}
            //return Ok();

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (medicine == null)
                {
                    _logger.LogError("Medicine object sent from client is null.");
                    return BadRequest("Medicine object is null");
                }

                var medicineEntity = _mapper.Map<Medicines>(medicine);

                if (medicine.Picture == null)
                    medicineEntity.MedicinePicture = "https://i.imgur.com/S9HJEwF.png";
                else
                    medicineEntity.MedicinePicture = medicine.Picture;


                _repository.Medicines.CreateMedicine(medicineEntity);

                _repository.Medicines.SaveAsync();


                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        
        //********************************* FUNCIONANDO *********************************
        [HttpPatch("{medicineId}")]
        public async Task<ActionResult> UpdateMedicine(string medicineId, JsonPatchDocument<MedicineForUpdateDto> patchDocument)
        {
            try
            {
                var medicineEntity = await _repository.Medicines.GetMedicineById(medicineId);

                if (medicineEntity == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }

                var medicineToPatch = _mapper.Map<MedicineForUpdateDto>(medicineEntity);
                medicineToPatch.DateModified = DateTime.Now;


                patchDocument.ApplyTo(medicineToPatch, ModelState);


                if (!TryValidateModel(medicineToPatch))
                {
                    return ValidationProblem(ModelState);
                }


                var medicineResult = _mapper.Map(medicineToPatch, medicineEntity);

                _repository.Medicines.Update(medicineResult);

                _repository.Medicines.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

      
        //********************************* FUNCIONANDO *********************************
        [HttpDelete("{medicineId}")]
        public async Task<ActionResult> DeleteMedicine(string medicineId)
        {
            try
            {
                var medicine = await _repository.Medicines.GetMedicineById(medicineId);

                if (medicine == null)
                {
                    _logger.LogError($"MedicineId with id: {medicineId}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.Medicines.Delete(medicine);

                _repository.Medicines.SaveAsync();

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
