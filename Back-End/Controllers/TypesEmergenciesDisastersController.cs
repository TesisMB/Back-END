
using AutoMapper;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
        public static int contador = 0;
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private IMapper _mapper;
        public static List<BrandsModelsForSelectDto> Key = new List<BrandsModelsForSelectDto>();
        //public static List<BrandsSelect> Brands = new List<BrandsSelect>();
        public static List<TypesSelect> typesSelect = new List<TypesSelect>();
        public static List<ModelsSelect> modelSelect = new List<ModelsSelect>();
        public static IEnumerable<Vehicles> brands = null;

        public TypesEmergenciesDisastersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //********************************* FUNCIONANDO *********************************

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

        //********************************* FUNCIONANDO *********************************
        //TO DO - Modificar retorno de datos
        [HttpGet("locations")]
        public ActionResult<LocationVolunteers> GetLocations()
        {
            try
            {
                var locations = _repository.Estates.GetAllLocations();
                _logger.LogInfo($"Returned all Locations from database.");

                //var alertResult = _mapper.Map<LocationsVolunteersDto>(locations);

                return Ok(locations);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Locations action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }


    }
}
