using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.DataTransferObjects.MarksModels___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TypesEmergenciesDisastersController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private IMapper _mapper;
        public TypesEmergenciesDisastersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("TypesEmergenciesDisasters")]
        public async Task<ActionResult<Alerts>> GetAllTypesEmergenciesDisasters()
        {
            try
            {
                var alerts = await _repository.TypesEmergenciesDisasters.GetAllTypesEmergenciesDisasters();
                _logger.LogInfo($"Returned all TypesEmergenciesDisasters from database.");

                var alertResult = _mapper.Map<IEnumerable<TypesEmergenciesDisastersDto>>(alerts);

                return Ok(alertResult);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTypesEmergenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpGet("TypesEmergenciesDisasters/{typeEmergencyDisasterId}")]
        public async Task<ActionResult<TypesEmergenciesDisasters>> GetTypesEmergencyDisaster(int typeEmergencyDisasterId)
        {
            try
            {
                var alerts = await _repository.TypesEmergenciesDisasters.GetTypeEmergencyDisaster(typeEmergencyDisasterId);
                _logger.LogInfo($"Returned all TypesEmergenciesDisasters from database.");

                var alertResult = _mapper.Map<TypesEmergenciesDisastersDto>(alerts);

                return Ok(alertResult);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTypesEmergenciesDisasters action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpGet("TypesVehicles")]
        public async Task<ActionResult<BrandsModels>> GetTypesVehicles()
        {
            try
            {
                var types = await _repository.TypesVehicles.GetAllTypesVehicles();

                _logger.LogInfo($"Returned all TypesVehicles from database.");

                var typesResult = _mapper.Map<IEnumerable<BrandsModelsForSelectDto>>(types);

                return Ok(typesResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllTypesVehicles action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }

        }

    }
}
