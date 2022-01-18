using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Resources_Request___Dto;
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


                //Rebisar si hay en stock Materials o Medicines
                var resource = _repository.Resources_Requests.Stock(resourceRequest);

                foreach (var resources in resource.Resources_RequestResources_Materials_Medicines_Vehicles)
                {

                    if (resources.Resources_Materials.Materials == null || resources.Resources_Medicines.Medicines == null)
                    {
                        return BadRequest(ErrorHelper.Response(400, "No hay Stock!!!"));
                    }

                    resources.Resources_Materials.Materials = null;
                    resources.Resources_Medicines.Medicines = null;
                }

                _repository.Resources_Requests.CreateResource_Resquest(resourceRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMedicine action: {ex.Message}");
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
                _logger.LogError($"Something went wrong inside UpdatePartialEmegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
