using AutoMapper;
using Back_End.EmergencyDisasterPDF;
using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergenciesDisastersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositorWrapper _repository;
        public readonly CruzRojaContext db = new CruzRojaContext();
        public ResourcesRequestMaterialsMedicinesVehicles resources = null;
        public byte[] pdf;
        private IConverter _converter;

        public EmergenciesDisastersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _converter = converter;
        }



        [HttpGet("PDF/{emegencyDisasterID}")]
        public async Task<IActionResult> CreatePDF(int emegencyDisasterID)
        {

            var employees = await _repository.EmergenciesDisasters.GetEmergencyDisasterWithDetails(emegencyDisasterID);

            //quien es el actual usuario
            Users user = new Users();
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            //user = cruzRojaContext.Users
            //        .Where(x => x.UserID == employeeId)
            //        .Include(a => a.Estates)
            //        .AsNoTracking()
            //        .FirstOrDefault();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"Reporte de emergencia",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = EmergencyDisasterPdf.GetHTMLString(employees),
                // Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
                //FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf");
        }


        //********************************* FUNCIONANDO *********************************
        [HttpGet]
        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmegenciesDisasters(int userId)
        {
            try
            {
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisasters(userId);

                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersSelectDto>>(emergenciesDisasters);

                var query = from st in emergenciesDisastersResult
                            select st;


                return Ok(emergenciesDisastersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpGet("WithoutFilter")]

        public async Task<ActionResult<EmergenciesDisasters>> GetAllEmergenciesDisastersWithoutFilter([FromQuery] int userId, [FromQuery] string limit)
        {
            try
            {
                var emergenciesDisasters = await _repository.EmergenciesDisasters.GetAllEmergenciesDisastersWithourFilter(userId, limit);

                _logger.LogInfo($"Returned all emergenciesDisasters from database.");

                var emergenciesDisastersResult = _mapper.Map<IEnumerable<EmergenciesDisastersAppDto>>(emergenciesDisasters);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();
   

                foreach (var item in emergenciesDisastersResult)
                {
                    foreach (var item2 in item.ChatRooms.DateMessage)
                    {
                        foreach (var item3 in item2.Messages)
                        {
                             var person = cruzRojaContext.Persons
                                           .Where(a => a.ID == item3.userID)
                                           .AsNoTracking()
                                           .FirstOrDefault();

                            item3.Name = person.FirstName + " " + person.LastName;
                         }
                    }
                }

                return Ok(emergenciesDisastersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmegenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }



        //********************************* FUNCIONANDO *********************************

        //TODO Hacer modelo dto aparte tanto para victims como resourceRequest(WebApp)
        [HttpGet("WithoutFilter/{emegencyDisasterID}")]
        public async Task<ActionResult<EmergenciesDisasters>> GetEmegencyDisasterIDWithDetails(int emegencyDisasterID)
        {
            try
            {
                var emegencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterWithDetails(emegencyDisasterID);

                if (emegencyDisaster == null)
                {
                    _logger.LogError($"EmergenciesDisasters with id: {emegencyDisasterID}, hasn't been found in db.");

                    return NotFound();
                }

                _logger.LogInfo($"Returned emegencyDisaster with details for id: {emegencyDisasterID}");

                var emergencyDisasterResult = _mapper.Map<EmergenciesDisastersDto>(emegencyDisaster);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var emergency = cruzRojaContext.EmergenciesDisasters
                                .OrderByDescending(a => a.EmergencyDisasterID)
                                .FirstOrDefault();

                    foreach (var item in emergencyDisasterResult.ChatRooms.DateMessage)
                    {
                        foreach (var item3 in item.Messages)
                        {
                            var person = cruzRojaContext.Persons
                                          .Where(a => a.ID == item3.userID)
                                          .AsNoTracking()
                                          .FirstOrDefault();


                            item3.Name = person.FirstName + " " + person.LastName;
                        }
                    }

                return Ok(emergencyDisasterResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmegencyDisasterIDWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPost]
        public async Task<ActionResult<EmergenciesDisasters>> CreateEmergencyDisaster([FromBody] EmergenciesDisastersForCreationDto emergenciesDisasters,
            [FromQuery] int userId)
        {
            emergenciesDisasters.CreatedBy = userId;

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (emergenciesDisasters == null)
                {
                    _logger.LogError("EmergencyDisaster object sent from client is null.");
                    return BadRequest("EmergencyDisaster object is null");
                }


                var emergencyDisaster = _mapper.Map<EmergenciesDisasters>(emergenciesDisasters);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var emergency = cruzRojaContext.EmergenciesDisasters
                       .OrderByDescending(a => a.EmergencyDisasterID)
                       .FirstOrDefault();

                if(emergency == null)
                {
                    emergencyDisaster.EmergencyDisasterID = 1;

                }
                else
                {
                  emergencyDisaster.EmergencyDisasterID = emergency.EmergencyDisasterID + 1;
                }


                emergencyDisaster.ChatRooms = new ChatRooms();
                emergencyDisaster.ChatRooms.CreationDate = DateTime.Now;
                emergencyDisaster.ChatRooms.FK_TypeChatRoomID = emergenciesDisasters.FK_TypeChatRoomID;


                _repository.EmergenciesDisasters.CreateEmergencyDisaster(emergencyDisaster);

                _repository.EmergenciesDisasters.SaveAsync();

                var response = await SendController.SendNotification(userId, emergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName, emergencyDisaster.EmergencyDisasterID);


                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateEmergenciesDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPatch("{emegencyDisasterID}")]
        public async Task<ActionResult> UpdatePartialEmegencyDisaster(int emegencyDisasterID, [FromQuery] int userId,
            JsonPatchDocument<EmergenciesDisastersForUpdateDto> _emergencyDisaster)
        {

            try
            {
                var emergencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);

                if (emergencyDisaster == null)
                {
                    return NotFound();
                }


                var emergencyDisasterToPatch = _mapper.Map<EmergenciesDisastersForUpdateDto>(emergencyDisaster);

                emergencyDisasterToPatch.EmergencyDisasterDateModified = DateTime.Now;

                _emergencyDisaster.ApplyTo(emergencyDisasterToPatch, ModelState);

                if (emergencyDisasterToPatch.EmergencyDisasterEndDate != null)
                {
                    emergencyDisasterToPatch.EmergencyDisasterEndDate = DateTime.Now;
                }

                emergencyDisasterToPatch.ModifiedBy = userId;

                if (!TryValidateModel(emergencyDisasterToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }


                var emergencyDisasterResult = _mapper.Map(emergencyDisasterToPatch, emergencyDisaster);

                _repository.EmergenciesDisasters.UpdateEmergencyDisaster(emergencyDisasterResult);

                _repository.EmergenciesDisasters.SaveAsync();

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePartialEmegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************

        [HttpDelete("{emegencyDisasterID}")]
        public async Task<ActionResult> DeletEmegencyDisaster(int emegencyDisasterID, [FromQuery] int userId)
        {
            try
            {

                var emegencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);

                if (emegencyDisaster == null)
                {
                    _logger.LogError($"EmergencyDisaster with id: {emegencyDisasterID}, hasn't ben found in db.");

                    return NotFound();
                }


                _repository.EmergenciesDisasters.DeleteEmergencyDisaster(emegencyDisaster);

                _repository.EmergenciesDisasters.SaveAsync();

                return NoContent();

            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletemegencyDisaster action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


    }
}
