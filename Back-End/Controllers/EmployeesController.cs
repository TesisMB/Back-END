using AutoMapper;
using Back_End.EmployeesPDF;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;

using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Employees___Dto;
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
    [Route("api/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public EmployeesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        //********************************* FUNCIONANDO *********************************

        [HttpGet("Employees")]
        public async Task<ActionResult<Users>> GetAllEmployees([FromQuery] int userId)
        {
            try
            {
                var employees = await _repository.Users.GetEmployeesVolunteers(userId);

                _logger.LogInfo($"Returned all employees from database.");

                var employeesResult = _mapper.Map<IEnumerable<EmployeeUserDto>>(employees);

                foreach (var item in employeesResult)
                {
                    item.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{item.Avatar}";
                }

                CruzRojaContext cruzRojaContext = new CruzRojaContext();


                foreach (var item in employeesResult)
                {
                    var userByChat = cruzRojaContext.UsersChatRooms.
                                            Where(a => a.FK_UserID.Equals(item.UserID))
                                            .AsNoTracking()
                                            .ToList();

                    foreach (var item2 in userByChat)
                    {
                        var userByEmergency = cruzRojaContext.EmergenciesDisasters
                                               .Where(a => a.EmergencyDisasterID.Equals(item2.FK_ChatRoomID))
                                               .ToList();

                        foreach (var item5 in userByEmergency)
                        {
                            var userByAlert = cruzRojaContext.Alerts
                                              .Where(a => a.AlertID.Equals(item5.FK_AlertID))
                                              .FirstOrDefault();

                            var userByTypeEmergency = cruzRojaContext.TypesEmergenciesDisasters
                                             .Where(a => a.TypeEmergencyDisasterID.Equals(item5.FK_TypeEmergencyID))
                                             .FirstOrDefault();

                            var userByLocation = cruzRojaContext.LocationsEmergenciesDisasters
                                         .Where(a => a.ID.Equals(item5.EmergencyDisasterID))
                                         .FirstOrDefault();


                            if (item.EmergencyDisastersReports == null)
                            {
                                item.EmergencyDisastersReports = new List<EmergenciesDisasterByUser>();

                                returnList(item.EmergencyDisastersReports, item5, userByAlert, userByTypeEmergency, userByLocation);
                            }
                            else
                            {
                                returnList(item.EmergencyDisastersReports, item5, userByAlert, userByTypeEmergency, userByLocation);

                            }
                        }
                    }
                }

                foreach (var item in employeesResult)
                {
                    var resourcesRequest = cruzRojaContext.Resources_Requests
                              .Where(a => a.CreatedBy.Equals(item.UserID))
                              .ToList();

                    if (resourcesRequest != null)
                        foreach (var x in resourcesRequest)
                        {
                            if (item.ResourcesRequestReports == null)
                            {
                                item.ResourcesRequestReports = new List<ResourcesRequestReports>();

                                returnList2(item.ResourcesRequestReports, x);
                            }
                            else
                            {
                                returnList2(item.ResourcesRequestReports, x);
                            }
                        }
                }

                return Ok(employeesResult);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployees action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [NonAction]
        public List<EmergenciesDisasterByUser> returnList(List<EmergenciesDisasterByUser> emergenciesDisasterByUsers,
                                                          EmergenciesDisasters emergenciesDisasters, Alerts alerts,
                                                          TypesEmergenciesDisasters typesEmergenciesDisasters, LocationsEmergenciesDisasters location)
        {
            var locations = location.LocationCityName.Split(',');
            var newCity = "" ;

            if (locations.Length == 1)
            {
                newCity = locations[locations.Length - 1];

            }
            else if (locations.Length == 2)
            {
                newCity = locations[locations.Length - 2];
            }
            else
            {
                newCity = locations[locations.Length - 3];
            }


            emergenciesDisasterByUsers.Add(new EmergenciesDisasterByUser
            {
                ID = emergenciesDisasters.EmergencyDisasterID,
                Type = typesEmergenciesDisasters.TypeEmergencyDisasterName,
                Degree = alerts.AlertDegree,
                City = newCity,
                State = (emergenciesDisasters.EmergencyDisasterEndDate == null) ? "Activa" : "Inactiva",
                StartDate = emergenciesDisasters.EmergencyDisasterStartDate,
                EndDate = emergenciesDisasters.EmergencyDisasterEndDate,
                Icon = emergenciesDisasters.TypesEmergenciesDisasters.TypeEmergencyDisasterIcon

            });

            return emergenciesDisasterByUsers;
        }



        [NonAction]
        public List<ResourcesRequestReports> returnList2(List<ResourcesRequestReports> resourcesRequestReports,
                                                      ResourcesRequest resourcesRequest)
        {
            var locations = resourcesRequest.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName.Split(',');
            var newCity = "";

            if (locations.Length == 1)
            {
                newCity = locations[locations.Length - 1];

            }
            else if (locations.Length == 2)
            {
                newCity = locations[locations.Length - 2];
            }
            else
            {
                newCity = locations[locations.Length - 3];
            }

            resourcesRequestReports.Add(new ResourcesRequestReports
            {
                ID = resourcesRequest.ID,
                Condition = resourcesRequest.Condition,
                City = newCity,
                RequestDate = resourcesRequest.RequestDate,
                EmergencyDisasterID = resourcesRequest.EmergenciesDisasters.EmergencyDisasterID,
                Type = resourcesRequest.EmergenciesDisasters.TypesEmergenciesDisasters.TypeEmergencyDisasterName
            });

            return resourcesRequestReports;
        }

        //********************************* FUNCIONANDO *********************************
        [HttpGet("Employees/{employeeId}")]
        public async Task<ActionResult<Users>> GetEmployeeWithDetails(int employeeId)
        {
            try
            {
                var employee = await _repository.Users.GetEmployeeVolunteerById(employeeId);

                //var employee = _repository.Employees.GetEmployeeWithDetails(employeeId);

                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned employe with details for id: {employeeId}");

                    var employeeResult = _mapper.Map<EmployeeUserDto>(employee);

                    employeeResult.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{employeeResult.Avatar}";

                    CruzRojaContext cruzRojaContext = new CruzRojaContext();

                        var userByChat = cruzRojaContext.UsersChatRooms.
                                                Where(a => a.FK_UserID.Equals(employeeResult.UserID))
                                                .AsNoTracking()
                                                .ToList();

                        foreach (var item2 in userByChat)
                        {
                            var userByEmergency = cruzRojaContext.EmergenciesDisasters
                                                   .Where(a => a.EmergencyDisasterID.Equals(item2.FK_ChatRoomID))
                                                   .ToList();

                            foreach (var item5 in userByEmergency)
                            {
                                var userByAlert = cruzRojaContext.Alerts
                                                  .Where(a => a.AlertID.Equals(item5.FK_AlertID))
                                                  .FirstOrDefault();

                                var userByTypeEmergency = cruzRojaContext.TypesEmergenciesDisasters
                                                 .Where(a => a.TypeEmergencyDisasterID.Equals(item5.FK_TypeEmergencyID))
                                                 .FirstOrDefault();

                                var userByLocation = cruzRojaContext.LocationsEmergenciesDisasters
                                             .Where(a => a.ID.Equals(item5.EmergencyDisasterID))
                                             .FirstOrDefault();

                                if (employeeResult.EmergencyDisastersReports == null)
                                {
                                    employeeResult.EmergencyDisastersReports = new List<EmergenciesDisasterByUser>();

                                    returnList(employeeResult.EmergencyDisastersReports, item5, userByAlert, userByTypeEmergency, userByLocation);
                                }
                                else
                                {
                                    returnList(employeeResult.EmergencyDisastersReports, item5, userByAlert, userByTypeEmergency, userByLocation);

                                }
                            }
                        }

                   
                        var resourcesRequest = cruzRojaContext.Resources_Requests
                                  .Where(a => a.CreatedBy.Equals(employeeResult.UserID))
                                  .ToList();

                        if (resourcesRequest != null)
                            foreach (var x in resourcesRequest)
                            {
                                if (employeeResult.ResourcesRequestReports == null)
                                {
                                employeeResult.ResourcesRequestReports = new List<ResourcesRequestReports>();

                                    returnList2(employeeResult.ResourcesRequestReports, x);
                                }
                                else
                                {
                                    returnList2(employeeResult.ResourcesRequestReports, x);
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


        //********************************* FUNCIONANDO *********************************

        [HttpPost("Employees")]
        public async Task<ActionResult<Users>> CreateEmployee([FromBody] UsersEmployeesForCreationDto employee, [FromQuery] int userId)
        {
            try
            {

                Roles roles = new Roles();

                CruzRojaContext db = new CruzRojaContext();

                roles = db.Roles
                    .Where(a => a.RoleID == employee.FK_RoleID)
                    .FirstOrDefault();


                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }


                if (employee == null)

                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }


                var employeeEntity = _mapper.Map<Users>(employee);
               employeeEntity.CreatedDate = DateTime.Now;
               employeeEntity.Avatar = "avatar-user.png";

                if (roles.RoleName != "Voluntario")
                {
                    employeeEntity.Employees = new Employees
                    {
                        EmployeeCreatedate = DateTime.Now
                    };


                }
                else
                {

                    employee.Volunteers = new VolunteersForCreationDto();
                    employeeEntity.Volunteers = new Volunteers();

                    if (employee.Avatar == null)
                    {
                        employeeEntity.Avatar = "avatar-user.png";
                    }
                    //else
                    //{
                    //    employeeEntity.Volunteers.VolunteerAvatar =  UploadController.SaveImage(employee.Volunteers.ImageFile);
                    //}

                }

                _repository.Employees.CreateEmployee(employeeEntity);

                return Ok();
            }

            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************

        //TO-DO Falta ver alertId
        [HttpPatch("Employees/{employeeId}")]
        public async Task<ActionResult> UpdatePartialUser(int employeeId, JsonPatchDocument<UsersForUpdateDto> _Employees)
        {
            try
            {
                var employeeEntity = await _repository.Users.GetEmployeeVolunteerById(employeeId);

                if (employeeEntity == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }

                var employeeToPatch = _mapper.Map<UsersForUpdateDto>(employeeEntity);

                _Employees.ApplyTo(employeeToPatch, ModelState);


                if (!TryValidateModel(employeeToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                List<UsersChatRooms> employeeEmg = new List<UsersChatRooms>();
                List<EmergenciesDisasters> employeeEmg2 = new List<EmergenciesDisasters>();
                Users user = new Users();
                List<Users> users = new List<Users>();

                using (var db = new CruzRojaContext())
                {
                    employeeEmg = db.UsersChatRooms.Where(a => a.FK_UserID.Equals(employeeId))
                                                          .AsNoTracking()
                                                          .ToList();

                    user = db.Users.Where(a => a.UserID.Equals(employeeId))
                             .AsNoTracking()
                            .FirstOrDefault();

                    users = db.Users.Where(a => a.FK_EstateID.Equals(user.FK_EstateID)
                                            && a.FK_RoleID.Equals(4))
                            .AsNoTracking()
                            .ToList();
                            

                    if(!employeeEmg.Count().Equals(0))
                    {
                        foreach (var item in employeeEmg)
                        {
                            employeeEmg2 = db.EmergenciesDisasters.Where(a => a.EmergencyDisasterID.Equals(item.FK_ChatRoomID)
                                                                     && a.EmergencyDisasterEndDate == null)
                                                              .AsNoTracking()
                                                              .ToList();
                        }
                    }
                }


                if (users.Count().Equals(1))
                {
                    return BadRequest(ErrorHelper.Response(400, "No se puede deshabilitar ya que es el unico Encargado de Logistica"));
                }


                if (employeeToPatch.UserAvailability == false && !employeeEmg.Count().Equals(0))
                {
                    return BadRequest(ErrorHelper.Response(400, "Este usuario se encuentra en una alerta activa"));
                }



                Users authUser = new Users();
                if (!string.IsNullOrEmpty(employeeToPatch.UserNewPassword))
                {
                    // AGREGARLOS EN EL REPOSITORIO
                    var userPass = employeeToPatch.UserPassword;
                    employeeToPatch.UserPassword = Encrypt.GetSHA256(userPass);

                    using (var db = new CruzRojaContext())
                        authUser = db.Users.Where(u => u.UserID == employeeEntity.UserID
                               && u.UserPassword == employeeToPatch.UserPassword)
                                .FirstOrDefault();


                    if (authUser == null)
                    {
                        return BadRequest(ErrorHelper.Response(400, "La contraseña es erronea."));
                    }

                    else
                    {
                        employeeToPatch.UserNewPassword = employeeToPatch.UserNewPassword.Trim();

                        var userNewPass = employeeToPatch.UserNewPassword;
                        employeeToPatch.UserNewPassword = Encrypt.GetSHA256(userNewPass);

                        employeeToPatch.UserPassword = employeeToPatch.UserNewPassword;
                    }

                }

             

                var employeeResult = _mapper.Map(employeeToPatch, employeeEntity);

                _repository.Users.Update(employeeResult);

                 if(employeeToPatch.FK_EstateID != employeeEntity.FK_EstateID)
                {
                    employeeEntity.FK_EstateID = employeeToPatch.FK_EstateID;
                }
                _repository.Employees.SaveAsync();

                return Ok(employeeResult);
            }


            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }



        [HttpPut("SendDevice")]
        public async Task<ActionResult> UpdateDevice(DeviceForUpdateDto users, [FromQuery] int userId, [FromQuery] string deviceToken)
        {
            try
            {
                var userEntity = await _repository.Users.SendDeviceById(userId) ;

                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                    return NotFound();
                }

                var userToPatch = _mapper.Map<DeviceForUpdateDto>(userEntity);

                userToPatch.DeviceToken = users.DeviceToken;
                //users.ApplyTo(userToPatch, ModelState);


                if (!TryValidateModel(userToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                var userResult = _mapper.Map(userToPatch, userEntity);

                if (!String.IsNullOrEmpty(deviceToken))
                    userResult.DeviceToken = string.Empty;

                _repository.Users.Update(userResult);

                _repository.Users.SaveAsync();

                return NoContent();
            }


            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        //********************************* FUNCIONANDO *********************************
        [HttpDelete("Employees/{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = await _repository.Users.GetUserEmployeeById(employeeId);

                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't ben found in db.");
                    return NotFound();
                }

                /*if (_repository.Vehicles.VehciclesByEmployees(employeeId).Any())
                {
                    _logger.LogError($"Cannot delete employee with id: {employeeId}. It has related {_repository.Vehicles}. Delete those accounts first");
                    return BadRequest();
                }*/

                _repository.Users.DeletUser(employee);

                _repository.Employees.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }







}
