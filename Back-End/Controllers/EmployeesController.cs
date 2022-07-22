using AutoMapper;
using Back_End.EmployeesPDF;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Employees___Dto;
using Entities.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
//using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Wkhtmltopdf.NetCore;

namespace Back_End.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositorWrapper _repository;
        private readonly IMapper _mapper;
        public byte[] pdf;
        private IConverter _converter;

        public EmployeesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _converter = converter;
        }

        [HttpGet("Employees/PDF/{employeeId}")]
        public IActionResult CreatePDF(int employeeId)
        {

            var employees = _repository.Employees.GetEmployeeWithDetails(employeeId);

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
                DocumentTitle = $"Reporte de empleado",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = EmployeePdf.GetHTMLString(employees),
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


        [HttpGet("Employees/GetAll/PDF/{employeeId}")]
        public async Task<IActionResult> CreatePDFEmployees(int employeeId)
        {

            var employees = await _repository.Employees.GetAllEmployees(employeeId);

            //quien es el actual usuario
            Users user = null;
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            string title = string.Empty;
            foreach (var emp in employees)
            {
                title = emp.Users.Estates.Locations.LocationCityName;
            }

            user = cruzRojaContext.Users
                    .Where(x => x.UserID == employeeId)
                    .AsNoTracking()
                    .FirstOrDefault();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"Reporte de empleados - {title}",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = EmployeesPdf.GetHTMLString(employees),
                // Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "StylesForEmployeescss.css") },
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

        [HttpGet("Employees/Credential/{employeeId}")]
        public IActionResult CredentialPDF(int employeeId)
        {

            var employees = _repository.Employees.GetEmployeeWithDetails(employeeId);

            //quien es el actual usuario
            Users user = new Users();
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            user = cruzRojaContext.Users
                    .Where(x => x.UserID == employeeId)
                    .Include(a => a.Estates)
                    .AsNoTracking()
                    .FirstOrDefault();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"Credencial",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = Credential.GetHTMLString(employees),
                // Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "Credential.css") },
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

        [HttpGet("Employees")]
        public async Task<ActionResult<Employees>> GetAllEmployees([FromQuery] int userId)
        {
            try
            {
                var employees = await _repository.Employees.GetAllEmployees(userId);

                _logger.LogInfo($"Returned all employees from database.");

                var employeesResult = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                return Ok(employeesResult);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployees action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        //********************************* FUNCIONANDO *********************************
        [HttpGet("Employees/{employeeId}")]
        public async Task<ActionResult<Employees>> GetEmployeeWithDetails(int employeeId)
        {
            try
            {
                var employee = _repository.Employees.GetEmployeeWithDetails(employeeId);

                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned employe with details for id: {employeeId}");

                    var employeeResult = _mapper.Map<EmployeeDto>(employee);
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

                    if (employee.Volunteers.VolunteerAvatar == null)
                    {
                        employeeEntity.Volunteers.VolunteerAvatar = "https://i.imgur.com/8AACVdK.png";
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
        public async Task<ActionResult> UpdatePartialUser(int employeeId, JsonPatchDocument<EmployeeForUpdateDto> _Employees)
        {
            try
            {
                var employeeEntity = await _repository.Employees.GetEmployeeById(employeeId);

                if (employeeEntity == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }

                var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

                _Employees.ApplyTo(employeeToPatch, ModelState);


                if (!TryValidateModel(employeeToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }


                Users authUser = new Users();
                if (!string.IsNullOrEmpty(employeeToPatch.Users.UserNewPassword))
                {
                    // AGREGARLOS EN EL REPOSITORIO
                    var userPass = employeeToPatch.Users.UserPassword;
                    employeeToPatch.Users.UserPassword = Encrypt.GetSHA256(userPass);

                    using (var db = new CruzRojaContext())
                        authUser = db.Users.Where(u => u.UserID == employeeEntity.Users.UserID
                               && u.UserPassword == employeeToPatch.Users.UserPassword)
                                .FirstOrDefault();


                    if (authUser == null)
                    {
                        return BadRequest(ErrorHelper.Response(400, "La contraseña es erronea."));
                    }

                    else
                    {
                        employeeToPatch.Users.UserNewPassword = employeeToPatch.Users.UserNewPassword.Trim();

                        var userNewPass = employeeToPatch.Users.UserNewPassword;
                        employeeToPatch.Users.UserNewPassword = Encrypt.GetSHA256(userNewPass);

                        employeeToPatch.Users.UserPassword = employeeToPatch.Users.UserNewPassword;
                    }

                }

                var employeeResult = _mapper.Map(employeeToPatch, employeeEntity);

                _repository.Employees.Update(employeeResult);

                _repository.Employees.SaveAsync();

                return NoContent();
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
                var userEntity = await _repository.Users.GetUserEmployeeById(userId) ;

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
