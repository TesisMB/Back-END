using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Back_End.Controllers
{
    //enrutamiento
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/
        public EmployeesController(ILoggerManager logger,IRepositorWrapper repository,IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _repository.Employees.GetAllEmployees();
                _logger.LogInfo($"Returned all employees from database.");

                var employeesResult = _mapper.Map<IEnumerable<EmployeesDto>>(employees);
                return Ok(employeesResult);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployees action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpGet("{employeeId}")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = _repository.Employees.GetEmployeeById(employeeId);

                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't been found in db.");

                    return NotFound();
                }

                else
                {
                    _logger.LogInfo($"Returned employee with id: {employeeId}");
                    var employeeResult = _mapper.Map<EmployeesDto>(employee);
                    return Ok(employeeResult);
                }
            } catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
             }
        }


        [HttpGet("{employeeId}/Users")]
        public IActionResult GetEmployeeWithDetails(int employeeId)
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

                    var employeeResult = _mapper.Map<EmployeesDto>(employee);
                    return Ok(employeeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public IActionResult CreateEmployee([FromBody]EmployeesForCreationDto employee)
        {
            try
            {
                if(employee == null)

                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                var employeeEntity = _mapper.Map<Employees>(employee);

                // Al crear un Usuario se encripta dicha contraseña para mayor seguridad.
                employeeEntity.Users.UserPassword = Encrypt.GetSHA256(employeeEntity.Users.UserPassword);


                _repository.Employees.CreateEmployee(employeeEntity);

                _repository.Save();

                var createdEmployee = _mapper.Map<EmployeesDto>(employeeEntity);

                return Ok();
            }

            catch(Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPatch("{employeeId}")]
        //[Authorize(Roles = "Coordinador General, Admin")] 
        public IActionResult UpdatePartialUser(int employeeId, JsonPatchDocument<EmployeeForUpdateDto> patchDocument)
        {
            
            try
            {
                var employeeEntity = _repository.Employees.GetEmployeeById(employeeId);


                if (employeeEntity == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't been found in db.");
                    return NotFound();
                }

               var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);


                patchDocument.ApplyTo(employeeToPatch, ModelState);
                
            //    var userNewPass = employeeToPatch.Users.UserNewPassword;
                //employeeToPatch.Users.UserNewPassword = Encrypt.GetSHA256(userNewPass);


                if (!TryValidateModel(employeeToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                var userPass = employeeToPatch.Users.UserPassword;
                employeeToPatch.Users.UserPassword = Encrypt.GetSHA256(userPass);

                Users authUser = new Users();

               /* using (var db = new CruzRojaContext())
                    authUser = db.Users.Where(u => u.UserID == employeeEntity.Users.UserID
                          && u.UserPassword == employeeToPatch.Users.UserPassword).FirstOrDefault();*/

/*                if (authUser != null)
                {
                var Pass = Encrypt.GetSHA256(userNewPass);

                employeeToPatch.Users.UserNewPassword = Pass;
                //Encrypt.GetSHA256(employeeEntity.Users.UserPassword);*/

                var employeeResult = _mapper.Map(employeeToPatch, employeeEntity);

                // employeeResult.Users.UserPassword = employeeToPatch.Users.UserNewPassword;


                _repository.Employees.Update(employeeResult);
                _repository.Save();

                return NoContent();
                

 //               return BadRequest("Contraseña Incorrecta");
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpDelete("{employeeId}")]

        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public IActionResult DeleteEmployee(int employeeId)
        {

            try
            {
                var employee = _repository.Users.GetUserEmployeeById(employeeId);

                if(employee == null)
                {
                    _logger.LogError($"Employee with id: {employeeId}, hasn't ben found in db.");
                    return NotFound();
                }

                /*if (_repository.Vehicles.VehciclesByEmployees(employeeId).Any())
                {
                    _logger.LogError($"Cannot delete employee with id: {employeeId}. It has related {_repository.Vehicles}. Delete those accounts first");
                    return BadRequest();
                }*/

                _repository.Users.Delete(employee);
                _repository.Save();
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }

}
