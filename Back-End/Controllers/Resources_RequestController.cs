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
    public class Resources_RequestController : ControllerBase
    {
        private IMapper _mapper;
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        public static Resources_Request resources_Request;
        public static Resources_Request reourceRequest;

        public static CruzRojaContext db = new CruzRojaContext();

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
                    return BadRequest(ErrorHelper.GetModelStateErrorsResourcesStock(ModelState));

               }

                if (resources_Request == null)
                {
                    _logger.LogError("Resource_Request object sent from client is null.");
                    return BadRequest("Resource_Request object is null");
                }


                var resourceRequest = _mapper.Map<Resources_Request>(resources_Request);

                resourceRequest.FK_UserID = UsersRepository.authUser.UserID;


                //Revisar si hay en stock Materials o Medicines


                //Pongo Null los valores Materials - Medicines - Vehicles para que no hay auna inconsistencia en los datos y algun error con la base de datos.

                _repository.Resources_Requests.CreateResource_Resquest(resourceRequest);


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateResource_Request action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPut("{reourceRequestID}")]
        public async Task<ActionResult> UpdateResourceRequest(int reourceRequestID,
                                                              Resource_RequestForUpdateDto _resourceRequest)
          {
          
            try
            {

                var resources = await _repository.Resources_Requests.GetResourcesRequestByID(reourceRequestID);


                if (resources == null)
                {
                    _logger.LogError($"Resource_Request with id: {reourceRequestID}, hasn't been found in db.");
                    return NotFound();
                }


                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ErrorHelper.GetModelStateErrorsResourcesStock(ModelState));
                        }

                var recursos = _mapper.Map(_resourceRequest, resources);

                foreach (var item in recursos.Resources_RequestResources_Materials_Medicines_Vehicles)
                {
                    item.FK_Resource_RequestID = reourceRequestID;
                }



                _repository.Resources_Requests.DeleteResource(recursos);

                _repository.Resources_Requests.Update(recursos);

                //Revisar si hay en stock Materials o Medicines

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
