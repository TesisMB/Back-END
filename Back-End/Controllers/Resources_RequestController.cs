using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var resource_Request = await _repository.Resources_RequestRepository.GetAllResourcesRequest();
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
                if (resources_Request == null)
                {
                    _logger.LogError("Resource_Request object sent from client is null.");
                    return BadRequest("Resource_Request object is null");
                }

                var resourceRequest = _mapper.Map<Resources_Request>(resources_Request);

                resourceRequest.FK_UserID = UsersRepository.authUser.UserID;

                _repository.Resources_RequestRepository.CreateResource_Resquest(resourceRequest);

                _repository.Resources_RequestRepository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMedicine action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }

}
