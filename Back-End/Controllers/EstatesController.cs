using AutoMapper;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        public EstatesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _repository.Estates.GetAllEstates();
                _logger.LogInfo($"Returned all estates from database.");

                var employeesResult = _mapper.Map<IEnumerable<EstatesDto>>(employees);
                return Ok(employeesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEstates action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }
    }
}
