using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DataTransferObjects.Resources_Request___Dto;
using Entities.DataTransferObjects.ResourcesRequest___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDF_Generator.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        CruzRojaContext db = new CruzRojaContext();
        private IConverter _converter;


        ResourcesRequestMaterialsMedicinesVehicles resources = null;


        public ResourcesRequestController(IMapper mapper, ILoggerManager logger, IRepositorWrapper repository, IConverter converter)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _converter = converter;
        }

        //********************************* FUNCIONANDO *********************************
        [HttpGet]
        public async Task<ActionResult<ResourcesRequest>> GetAllResourceResquest([FromQuery] int userId, [FromQuery] string? Condition, [FromQuery] string? state)
        {
            {

                var resource_Request = await _repository.Resources_Requests.GetAllResourcesRequest(userId, Condition, state);
                _logger.LogInfo($"Returned all Resources_Request from database.");

                var resource_RequestResult = _mapper.Map<IEnumerable<ResourcesRequestDto>>(resource_Request);


                var query = from st in resource_RequestResult
                            select st;




                foreach (var item1 in query)
                {

                    foreach (var item2 in item1.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {

                        if (item2.Materials != null)
                        {

                            resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                        .Where(a => a.FK_MaterialID == item2.Materials.ID
                                                && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                           .AsNoTracking()
                                        .FirstOrDefault();

                            item2.Materials.Quantity = resources.Quantity;

                        }

                        else if (item2.Medicines != null)
                        {
                            resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                 .Where(a => a.FK_MedicineID == item2.Medicines.ID
                                         && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                    .AsNoTracking()
                                 .FirstOrDefault();

                            item2.Medicines.Quantity = resources.Quantity;

                        }

                        else if (item2.Vehicles != null)
                        {
                            resources = db.Resources_RequestResources_Materials_Medicines_Vehicles
                                 .Where(a => a.FK_VehicleID == item2.Vehicles.ID
                                         && a.FK_Resource_RequestID == item2.FK_Resource_RequestID)
                                    .AsNoTracking()
                                 .FirstOrDefault();

                            item2.Vehicles.Quantity = resources.Quantity;

                        }


                    }
                }


                return Ok(resource_RequestResult);

            }
        }

        //********************************* FUNCIONANDO *********************************

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


                _repository.Resources_Requests.CreateResource_Resquest(resourceRequest);

                _repository.Resources_Requests.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateResource_Request action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ResourcesRequest>> DeleteResourceRequest(int id)
        {
            try
            {
                var resource = await _repository.Resources_Requests.GetResourcesRequestByID(id);

                resource = _repository.Resources_Requests.UpdateStockDelete(resource);

                if (resource == null)
                {
                    return NotFound();
                }

                _repository.Resources_Requests.Delete(resource);
                _repository.Resources_Requests.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside DeleteResourceRequest action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        //********************************* FUNCIONANDO *********************************

        [HttpPost("{acceptRejectRequest}")]
        public ActionResult<ResourcesRequest> AcceptRejectRequest([FromBody] AcceptRejectRequestDto acceptRejectRequest)
        {
            try
            {
                if (acceptRejectRequest == null)
                {
                    _logger.LogError("AcceptRejectRequest object sent from client is null.");
                    return BadRequest("AcceptRejectRequest object is null");
                }

                var resourceRequest = _mapper.Map<ResourcesRequest>(acceptRejectRequest);

                _repository.Resources_Requests.AcceptRejectRequest(resourceRequest, acceptRejectRequest.UserRequest);

                return Ok();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AcceptRejectRequest action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("PDF/{userId}")]
        public async Task<IActionResult> CreatePDF(int userId)
        {
            var requests = await _repository.Resources_Requests.GetAllResourcesRequest(userId, "Rechazada");

            //quien es el actual usuario
            Users user = null;
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            user = cruzRojaContext.Users
                    .Where(x => x.UserID == userId)
                    .AsNoTracking()
                    .FirstOrDefault();

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent =  RequestHistory.GetHTMLString(requests),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesForRequest.css") },
                FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
            };

            //var globalSettings = new GlobalSettings
            //{
            //    ColorMode = ColorMode.Color,
            //    Orientation = Orientation.Landscape,
            //    PaperSize = PaperKind.A4,
            //    Margins = new MarginSettings { Top = 10 },
            //    DocumentTitle = "Reporte de recursos",
            //};

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Historial de solicitudes",
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf");
        }




    }

}
