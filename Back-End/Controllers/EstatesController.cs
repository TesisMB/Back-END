using AutoMapper;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/")]
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




        [HttpGet("Estates")]
        public async Task<ActionResult<Estates>> GetAllEstatesType()
        {
            try
            {
                var employees = await _repository.Estates.GetAllEstates();
                _logger.LogInfo($"Returned all estates from database.");

                var employeesResult = _mapper.Map<IEnumerable<EstatesTypeDto>>(employees);
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


