using AutoMapper;
using Back_End.EmergencyDisasterPDF;
using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;

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
    

        public EmergenciesDisastersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
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
                       .AsNoTracking()
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


                var user = cruzRojaContext.Users
                             .Where(a => a.UserID == userId)
                              .AsNoTracking()
                             .FirstOrDefault();

                var userCG  = cruzRojaContext.Users
                               .Where(a => a.FK_EstateID == user.FK_EstateID
                                      && a.Roles.RoleName == "Coordinador General")
                               .AsNoTracking()
                               .ToList();

                var userAdm = cruzRojaContext.Users
                          .Where(a => a.FK_EstateID == user.FK_EstateID
                                 && a.Roles.RoleName == "Admin")
                          .AsNoTracking()
                          .ToList();

                emergencyDisaster.ChatRooms.UsersChatRooms = new List<UsersChatRooms>();

                    var userChatRooms = cruzRojaContext.UsersChatRooms
                                 .OrderByDescending(a => a.ID)
                                 .FirstOrDefault();


                int ID;
                if (userChatRooms == null)
                {
                    ID = 1;
                }
                else
                {
                    ID = userChatRooms.ID + 1;
                }

                //Completo con el C.Emergencia Elejido para la emergencia
                emergencyDisaster.ChatRooms.UsersChatRooms.Add(new UsersChatRooms()
                {
                    FK_UserID = (int)emergencyDisaster.Fk_EmplooyeeID,
                    FK_ChatRoomID = emergencyDisaster.EmergencyDisasterID,
                    Status = true,
                    ID = ID
                }); ;


                if(userCG != null)
                {
                    ID += 1; 
                //Completo con los C.General
                    foreach (var item in userCG)
                    {
                        emergencyDisaster.ChatRooms.UsersChatRooms.Add(new UsersChatRooms()
                        {
                           FK_UserID = item.UserID,
                           FK_ChatRoomID = emergencyDisaster.EmergencyDisasterID,
                           Status = true,
                            ID = ID
                        });
                    }
                }

                //***************** CONSULTAR *****************
                //if (userAdm != null)
                //{
                //    ID += 1;
                //    //Completo con los Admin
                //    foreach (var item in userAdm)
                //    {
                //        emergencyDisaster.ChatRooms.UsersChatRooms.Add(new UsersChatRooms()
                //        {
                //            FK_UserID = item.UserID,
                //            FK_ChatRoomID = emergencyDisaster.EmergencyDisasterID,
                //            ID = ID
                //        });
                //    }
                //}



                _repository.EmergenciesDisasters.CreateEmergencyDisaster(emergencyDisaster);

                _repository.EmergenciesDisasters.SaveAsync();

                //var response = await SendController.SendNotification(userId, emergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName, emergencyDisaster.EmergencyDisasterID);


                return Ok();
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
                CruzRojaContext cruzRojaContext = new CruzRojaContext();


                var emergencyDisaster = await _repository.EmergenciesDisasters.GetEmergencyDisasterById(emegencyDisasterID);

                if (emergencyDisaster == null)
                {
                    return NotFound();
                }

                var CG = cruzRojaContext.UsersChatRooms.Where(a => a.FK_UserID.Equals(emergencyDisaster.Fk_EmplooyeeID))
                                                                .FirstOrDefault();


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


                var userChatRooms = cruzRojaContext.UsersChatRooms
                             .OrderByDescending(a => a.ID)
                             .FirstOrDefault();


                //Completo con el C.Emergencia Elejido para la emergencia
               var usersChatRoomsCE = new UsersChatRooms()
                {
                    FK_UserID = (int)emergencyDisaster.Fk_EmplooyeeID,
                    FK_ChatRoomID = emergencyDisaster.EmergencyDisasterID,
                    Status = true,
                    ID = userChatRooms.ID + 1
                };

                _repository.UsersChatRooms.Create(usersChatRoomsCE);
                _repository.UsersChatRooms.LeaveGroup(CG);
                _repository.UsersChatRooms.SaveAsync();
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
