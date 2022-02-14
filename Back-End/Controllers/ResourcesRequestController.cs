using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesRequestController : ControllerBase
    {
        private IMapper _mapper;
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        public static ResourcesRequest resources_Request;
        public static ResourcesRequest reourceRequest;

        public static CruzRojaContext db = new CruzRojaContext();

        public ResourcesRequestController(IMapper mapper, ILoggerManager logger, IRepositorWrapper repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ResourcesRequest>> GetAllResourceResquest()
        {

            var resource_Request = await _repository.Resources_Requests.GetAllResourcesRequest();
            _logger.LogInfo($"Returned all Resources_Request from database.");

            var resource_RequestResult = _mapper.Map<IEnumerable<ResourcesRequestDto>>(resource_Request);

            return Ok(resource_RequestResult);
        }


        [HttpPost]
        public ActionResult<ResourcesRequest> CreateResource_Request([FromBody] ResourcesRequestForCreationDto resources_Request)
        {
            try
            {

               if (!ModelState.IsValid)
               {
                    return BadRequest(ErrorHelper.GetModelStateErrorsResourcesStock(ModelState));

               }

                if (resources_Request == null)
                {
                    _logger.LogError("Resource_Request object sent from client is null.");
                    return BadRequest("Resource_Request object is null");
                }


                var resourceRequest = _mapper.Map<ResourcesRequest>(resources_Request);


                _repository.Resources_Requests.CreateResource_Resquest(resourceRequest, resources_Request.UserRequest);

                _repository.Resources_Requests.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateResource_Request action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
