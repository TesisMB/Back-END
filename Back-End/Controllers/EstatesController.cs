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
using Wkhtmltopdf.NetCore;

namespace Back_End.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EstatesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private byte[] pdf;
        private readonly IMapper _mapper;
        public readonly IGeneratePdf _generatePdf;

        public EstatesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IGeneratePdf generatePdf)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _generatePdf = generatePdf;

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


        [HttpPost("Recursos/PDF")]
        public async Task<FileResult> GetEmployeeIDPDF(string LocationDepartmentName)
        {

            if (LocationDepartmentName == null)
            {
                var employee =  _repository.Estates.GetAllEstatesByPdf(LocationDepartmentName);
                 pdf = await _generatePdf.GetByteArray("Views/Resources/ResourcesInfo.cshtml", employee);
            }
            else
            {
                var employee = _repository.Estates.GetAllEstatesByPdf(LocationDepartmentName);
                pdf = await _generatePdf.GetByteArray("Views/Resources/ResourceInfo.cshtml", employee);
            }

            return new FileContentResult(pdf, "application/pdf");
        }

        
    }
}


