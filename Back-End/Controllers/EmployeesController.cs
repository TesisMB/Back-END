using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Employees___Dto;
using Entities.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace Back_End.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;
        readonly IGeneratePdf _generatePdf;

        public EmployeesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IGeneratePdf generatePdf)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _generatePdf = generatePdf;
        }

        [HttpGet("PDF/{employeeId}")]
        public async Task<FileResult> GetEmployeeIDPDF(int employeeId)
        {
            var employee = await _repository.Employees.GetEmployeeWithDetails(employeeId);
            
                        var options = new ConvertOptions
                        {
                            PageMargins = new Wkhtmltopdf.NetCore.Options.Margins()
                            {
                                Top = 5
                            }
                        };

             _generatePdf.SetConvertOptions(options);



            // var filePath = $"{employee.Users.Persons.FirstName} {employee.Users.Persons.LastName}.pdf"; // Here, you should validate the request and the existance of the file.

            // var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            //return File(bytes, "Views/Employee/EmployeeInfo.cshtml", Path.GetFileName(filePath));

            var pdf = await _generatePdf.GetByteArray("Views/Employee/EmployeeInfo.cshtml", employee);


            //var pdf = await _generatePdf.GetByteArray("Views/Employee/EmployeeInfo.cshtml", employee);

            //return new FileStreamResult(pdfStream, "application/pdf");

            //return await _generatePdf.GetPdf("Views/Employee/EmployeeInfo.cshtml", pdfStream);

            return File(pdf, "application/pdf", $"{employee.Users.Persons.FirstName} {employee.Users.Persons.LastName}.pdf");
        }




        [HttpGet]
        // [Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public async Task<ActionResult<Employees>> GetAllEmployees()
        {
            try
            {
                var employees = await _repository.Employees.GetAllEmployees();

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


        [HttpGet("{employeeId}")]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public async Task<ActionResult<Employees>> GetEmployeeWithDetails(int employeeId)
        {
            try
            {
                var employee = await _repository.Employees.GetEmployeeWithDetails(employeeId);

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

        //[Authorize(Roles = "Coordinador General, Admin")] 
        [HttpPost]
        public ActionResult<Employees> CreateEmployee([FromBody] EmployeesForCreationDto employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (employee == null)

                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }


                var employeeEntity = _mapper.Map<Employees>(employee);

                _repository.Employees.CreateEmployee(employeeEntity);

                _repository.Employees.SaveAsync();

                return Ok();
            }

            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize(Roles = "Coordinador General, Admin")] 
        [HttpPatch("{employeeId}")]
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
                            .AsNoTracking()
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
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        [HttpDelete("{employeeId}")]
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
