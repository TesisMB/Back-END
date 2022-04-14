
using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.BrandsModels__Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public static List<BrandsModelsDto> Brands = new List<BrandsModelsDto>();
        public static List<TypesSelect> typesSelect = new List<TypesSelect>();
        public static List<ModelsSelect> modelSelect = new List<ModelsSelect>();
        public static IEnumerable<Vehicles> brands = null;

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
        public async Task<ActionResult<TypeVehicles>> GetTypesVehicles([FromQuery] BrandsModelsForSelectDto brandsModels)
        {


            try
            {
                var types = await _repository.TypesVehicles.GetAllTypesVehicles();

                _logger.LogInfo($"Returned all TypesVehicles from database.");

               var typesResult = _mapper.Map<IEnumerable<BrandsModelsForSelectDto>>(types);


                //agrego x tipo de auto
             //   foreach (var item in types)
               // {
                  /*  typesSelect.Add(new TypesSelect()
                    {
                        TypeID = item.Vehicles.TypeVehicles.ID,
                        Type = item.Vehicles.TypeVehicles.Type,
                    });*/

                   // typesSelect = typesSelect.Distinct(new TypesSelectComparer()).ToList();
                //}


                //Marca
              /*  foreach (var item in types)
                {

                    Brands.Add(new BrandsSelect()
                    {
                        BrandID = item.Brands.ID,
                        BrandsName = item.Brands.BrandName
                    });

                    Brands = Brands.Distinct(new BrandsComparer()).ToList();
                }*/


                //Modelo
                /*foreach (var item in types)
                {

                    modelSelect.Add(new ModelsSelect()
                    {
                        ModelID = item.Model.ID,
                        ModelName = item.Model.ModelName
                    });

                    modelSelect = modelSelect.Distinct(new ModelsComparer()).ToList();
                }*/




             /*   foreach (var item in typesSelect)
                {

                    Key.Add(new BrandsModelsForSelectDto()
                    {
                        TypeID = item.TypeID,
                        Type = item.Type,
                        Brands = Retornar()
                    });*/


                  //  Key = Key.Distinct(new BrandsModelComparer()).ToList();

                    //Key.Where(elem => elem.TypeID == item.Vehicles.TypeVehicles.ID);

                    //Key.Add(item.Brands.BrandName);

                //}

                //contador = 0;
                return Ok(typesResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllTypesVehicles action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }

        }


        public static IEnumerable<Vehicles> Select(BrandsModelsDto brand)
            {
                foreach (var item in typesSelect)
                {
                    CruzRojaContext cruzRojaContext = new CruzRojaContext();

                    //todas las veces que aparece el tipo
              /*      brands = cruzRojaContext.Vehicles.Where(
                                        a => a.TypeVehicles.Type == item.Type
                                        && a.BrandsModels.Brands.ID == brand.BrandID)
                                        .AsNoTracking()
                                        .ToList();
              */
                }

                    return brands;
        }

        public static BrandsModelsDto Retornar()
        {
            var llave = Brands;
           
            if(llave.Count == contador)
            {
                contador = 0;
            }

            var valor = llave.ElementAt(contador);

            contador += 1;

            Select(valor);

            return valor;
        }



    }
}
