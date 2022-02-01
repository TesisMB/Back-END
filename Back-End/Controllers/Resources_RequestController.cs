using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Resources_RequestController : ControllerBase
    {
        private IMapper _mapper;
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;

        public Resources_RequestController(IMapper mapper, ILoggerManager logger, IRepositorWrapper repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Resources_Request>> GetAllResourceResquest()
        {

            var resource_Request = await _repository.Resources_Requests.GetAllResourcesRequest();
            _logger.LogInfo($"Returned all Resources_Request from database.");

            if (resource_Request == null)
            {
                return Unauthorized();
            }

            var resource_RequestResult = _mapper.Map<IEnumerable<Resources_RequestDto>>(resource_Request);

            return Ok(resource_RequestResult);
        }

        [HttpPost]
        public ActionResult<Resources_Request> CreateResource_Request([FromBody] Resources_RequestForCreationDto resources_Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (resources_Request == null)
                {
                    _logger.LogError("Resource_Request object sent from client is null.");
                    return BadRequest("Resource_Request object is null");
                }

                var resourceRequest = _mapper.Map<Resources_Request>(resources_Request);

                foreach (var item2 in resourceRequest.Resources_RequestResources_Materials_Medicines_Vehicles)
                {
                    //MEDICINES
                    if (item2.Resources_Medicines.FK_MedicineID == 0 && item2.FK_VehiclesID != 0 && item2.Resources_Materials.FK_MaterialID != 0)
                    {
                        item2.Resources_Medicines = null;
                    }
                    else if (item2.Resources_Medicines.FK_MedicineID != 0 && item2.FK_VehiclesID != 0 && item2.Resources_Materials.FK_MaterialID == 0)
                    {
                        item2.Resources_Materials = null;

                    }
                    else if (item2.Resources_Medicines.FK_MedicineID != 0 && item2.FK_VehiclesID == 0 && item2.Resources_Materials.FK_MaterialID == 0)
                    {
                        item2.Resources_Materials = null;
                        item2.FK_VehiclesID = null;

                    }
                    else if (item2.Resources_Medicines.FK_MedicineID == 0 && item2.FK_VehiclesID == 0 && item2.Resources_Materials.FK_MaterialID != 0)
                    {
                        item2.Resources_Medicines = null;
                        item2.FK_VehiclesID = null;
                    }
                    else if (item2.Resources_Medicines.FK_MedicineID == 0 && item2.FK_VehiclesID != 0 && item2.Resources_Materials.FK_MaterialID == 0)
                    {
                        item2.Resources_Materials = null;
                        item2.Resources_Medicines = null;
                    }
                }

                resourceRequest.FK_UserID = UsersRepository.authUser.UserID;


                //Revisar si hay en stock Materials o Medicines
                var resource = _repository.Resources_Requests.Stock(resourceRequest);

                foreach (var resources in resource.Resources_RequestResources_Materials_Medicines_Vehicles)
                {
                    if (resources.Resources_Materials == null && resources.Resources_Medicines == null && !resources.Vehicles.VehicleAvailability)
                    {

                        return BadRequest(ErrorHelper.Response(400, @$"Vehicle no disponible {resources.FK_VehiclesID}"));
                    }

                    if (resources.Resources_Medicines == null && resources.FK_VehiclesID == null && resources.Resources_Materials.Quantity - resources.Resources_Materials.Materials.MaterialQuantity > 0)
                 
                    {
                        return BadRequest(ErrorHelper.Response(400, @$"Material sin stock {resources.Resources_Materials.FK_MaterialID}"));
                    }

                    if (resources.Resources_Materials == null && resources.FK_VehiclesID == null && resources.Resources_Medicines.Quantity - resources.Resources_Medicines.Medicines.MedicineQuantity > 0)
                    {
                        return BadRequest(ErrorHelper.Response(400, @$"Medicine sin stock {resources.Resources_Medicines.FK_MedicineID}"));

                    }
                }
                
                //Pongo Null los valores Materials - Medicines - Vehicles para que no hay auna inconsistencia en los datos y algun error con la base de datos.
                var recurso = _repository.Resources_Requests.DeleteResource(resource);

                _repository.Resources_Requests.CreateResource_Resquest(resource);

                _repository.Resources_Requests.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateResource_Request action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{reourceRequestID}")]
        public async Task<ActionResult> UpdateResourceRequest(int reourceRequestID, JsonPatchDocument<Resource_RequestForUpdateDto> _resourceRequest)
        {
            try
            {
                var reourceRequest = await _repository.Resources_Requests.GetResourcesRequestByID(reourceRequestID);

                if (reourceRequest == null)
                {
                    _logger.LogError($"Resource_Request with id: {reourceRequestID}, hasn't been found in db.");
                    return NotFound();
                }

                var resourceRequestToPatch = _mapper.Map<Resource_RequestForUpdateDto>(reourceRequest);

                _resourceRequest.ApplyTo(resourceRequestToPatch, ModelState);

                if (!TryValidateModel(resourceRequestToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }


                var resourceRequestResult = _mapper.Map(resourceRequestToPatch, reourceRequest);

                _repository.Resources_Requests.UpdateResource_Resquest2(resourceRequestResult);

                _repository.Resources_Requests.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePartialResource_Request action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
