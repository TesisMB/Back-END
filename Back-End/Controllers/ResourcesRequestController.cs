using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities.DataTransferObjects.Resources_Request___Dto;
using System.Collections.Generic;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Helpers;
using System;
using Back_End.Models;
using Repository;

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
        public async Task<ActionResult<ResourcesRequest>> GetAllResourceResquest([FromQuery] string Condition)
        {

            var resource_Request = await _repository.Resources_Requests.GetAllResourcesRequest(Condition);
            _logger.LogInfo($"Returned all Resources_Request from database.");

            var resource_RequestResult = _mapper.Map<IEnumerable<ResourcesRequestDto>>(resource_Request);



            var query = from st in resource_RequestResult
                        select st; 
            

            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;
            CruzRojaContext db = new CruzRojaContext();


            foreach (var item in query)
            {

                foreach (var item2 in item.Resources_RequestResources_Materials_Medicines_Vehicles)
                {

                    ResourcesRequestMaterialsMedicinesVehicles resourcesRequestMaterialsMedicinesVehicles = null;

                    //sin material y vehiculo
                    if (item2.Brand != null)
                    {
                        resourcesRequestMaterialsMedicinesVehicles = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                                                            .Include(a => a.Materials)
                                                                            .Where(a => a.ID == item2.ID)
                                                                            .AsNoTracking()
                                                                            .FirstOrDefault();


                        item2.Name = resourcesRequestMaterialsMedicinesVehicles.Materials.MaterialName;
                        item2.ResourceID = resourcesRequestMaterialsMedicinesVehicles.Materials.ID;
                    }

                    //sin medicamentos y vehiculo
                    if(item2.MedicineLab != null)
                    {
                        resourcesRequestMaterialsMedicinesVehicles = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                                                           .Include(a => a.Medicines)
                                                                            .Where(a => a.ID == item2.ID)
                                                                           .AsNoTracking()
                                                                           .FirstOrDefault();


                        item2.Name = resourcesRequestMaterialsMedicinesVehicles.Medicines.MedicineName;
                        item2.ResourceID = resourcesRequestMaterialsMedicinesVehicles.Medicines.ID; 
                    }

                    if(item2.VehiclePatent != null)
                    {

                
                        resourcesRequestMaterialsMedicinesVehicles = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                                                .Where(a => a.ID == item2.ID)
                                                                .Include(a => a.Vehicles)
                                                                .ThenInclude(a => a.BrandsModels)
                                                                .ThenInclude(a => a.Brands)
                                                                .Include(a => a.Vehicles)
                                                                .ThenInclude(a => a.BrandsModels)
                                                                .ThenInclude(a => a.Model)
                                                                .AsNoTracking()
                                                                .FirstOrDefault();


                        vehicles = db.Vehicles
                                  .Where(a => a.ID == resourcesRequestMaterialsMedicinesVehicles.FK_VehicleID)
                                  .AsNoTracking()
                                  .FirstOrDefault();


                        item2.Name = resourcesRequestMaterialsMedicinesVehicles.Vehicles.BrandsModels.Brands.BrandName + " " + resourcesRequestMaterialsMedicinesVehicles.Vehicles.BrandsModels.Model.ModelName;
                        item2.ResourceID = resourcesRequestMaterialsMedicinesVehicles.Vehicles.ID;

                    }
                }
            }


            return Ok(resource_RequestResult);
        }


   


        [HttpPost]
        public ActionResult<ResourcesRequest> CreateResource_Request([FromBody] ResourcesRequestForCreationDto resources_Request)
        {
            try
            {
                var user = UsersRepository.authUser;

                ResourcesRequest userReq = null;


                userReq = db.Resources_Requests
                 .Where(a => a.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                         && a.FK_UserID == user.UserID)
                         .AsNoTracking()
                         .FirstOrDefault();


                if(userReq!= null && userReq.Condition != "Pendiente")
                {
                    return BadRequest(ErrorHelper.Response(400, "Su solicitud fue " +  userReq.Condition +" debe realizar una nueva solicitud"));

                }

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
