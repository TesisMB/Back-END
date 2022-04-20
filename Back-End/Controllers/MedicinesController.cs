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
        public async Task<ActionResult<Medicines>> GetAllMedicines()
        {
            try
            {
                var volunteers = await _repository.Medicines.GetAllMedicines();

                _logger.LogInfo($"Returned all Materials from database.");

                var medicinesResult = _mapper.Map<IEnumerable<Resources_Dto>>(volunteers);

                foreach (var item in medicinesResult)
                {
                    
                     if(item.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        item.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, item.Picture);


                        DateTime date = Convert.ToDateTime(item.Medicines.MedicineExpirationDate);

                        if (date < DateTime.Now)
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

        [HttpGet("{medicineId}")]
        public async Task<ActionResult<Medicines>> GetMedicineWithDetails(int medicineId)
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
                        employeeResult.Picture = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, employeeResult.Picture);


                        DateTime date = Convert.ToDateTime(employeeResult.Medicines.MedicineExpirationDate);

                        if (date < DateTime.Now)
                        {
                            employeeResult.Availability = false;
                        }

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

        [HttpPost]
        public async Task<ActionResult<Medicines>> CreateMedicine([FromBody] Resources_ForCreationDto medicine)
        {
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


                if(medicine.ImageFile == null)
                {
                    medicineEntity.MedicinePicture = "https://i.imgur.com/S9HJEwF.png";
                }
                else
                {

                    medicineEntity.MedicinePicture = await UploadController.SaveImage(medicine.ImageFile, "Resources");
                }

                medicineEntity.MedicineName = medicine.Name;
                medicineEntity.MedicineDonation = medicine.Donation;
                medicineEntity.MedicineQuantity = medicine.Quantity;
                medicineEntity.MedicineAvailability = true;
                medicineEntity.MedicineUtility = medicine.Description;
                //medicineEntity.MedicineExpirationDate = medicine.Medicines.MedicineExpirationDate;
                medicineEntity.MedicineLab = medicine.Medicines.MedicineLab;
                medicineEntity.MedicineDrug = medicine.Medicines.MedicineDrug;
                medicineEntity.MedicineWeight = medicine.Medicines.MedicineWeight;
                medicineEntity.MedicineUnits = medicine.Medicines.MedicineUnits;


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

        [HttpPatch("{medicineId}")]
        public async Task<ActionResult> UpdateMedicine(int medicineId, JsonPatchDocument<Resources_ForCreationDto> patchDocument)
        {
            try
            {
                var medicineEntity = await _repository.Medicines.GetMedicineById(medicineId);

                if (medicineEntity == null)
                {
                    _logger.LogError($"Medicine with id: {medicineId}, hasn't been found in db.");
                    return NotFound();
                }

                var medicineToPatch = _mapper.Map<Resources_ForCreationDto>(medicineEntity);

                patchDocument.ApplyTo(medicineToPatch, ModelState);


                if (!TryValidateModel(medicineToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                MedicineForUpdateDto medicine = new MedicineForUpdateDto();
                medicine.MedicineQuantity = medicineToPatch.Quantity;
                medicine.MedicineName = medicineToPatch.Name;
                medicine.MedicineAvailability = medicineToPatch.Availability;
                medicine.MedicineDonation = medicineToPatch.Donation;
                medicine.MedicineUtility = medicineToPatch.Description;
                //medicine.MedicineExpirationDate = medicineToPatch.Medicines.MedicineExpirationDate;
                medicine.MedicineDrug = medicineToPatch.Medicines.MedicineDrug;
                medicine.MedicineWeight = medicineToPatch.Medicines.MedicineWeight;
                medicine.MedicineUnits = medicineToPatch.Medicines.MedicineUnits;
                medicine.FK_EstateID = medicineToPatch.FK_EstateID;
                medicine.MedicineLab = medicineToPatch.Medicines.MedicineLab;


                var medicineResult = _mapper.Map(medicine, medicineEntity);

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

        [HttpDelete("{medicineId}")]
        public async Task<ActionResult> DeleteMedicine(int medicineId)
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
