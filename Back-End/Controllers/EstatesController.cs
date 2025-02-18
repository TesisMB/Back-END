﻿using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;

using Entities.DataTransferObjects.Estates___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PDF_Generator.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Wkhtmltopdf.NetCore;

namespace Back_End.Controllers
{
    [Route("api/Estates")]
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

        [HttpGet]
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


