using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/resources/[controller]")]
    [ApiController]
    public class VehiclesController : BaseApiController
    {

        private readonly ICruzRojaRepository<Vehicles> _cruzRojaRepository;
        private readonly IMapper _mapper;

        public VehiclesController(ICruzRojaRepository<Vehicles> cruzRojaRepository, IMapper mapper)

        {
            _cruzRojaRepository = cruzRojaRepository ??
                throw new ArgumentNullException(nameof(VehiclesRepository));

            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult<IEnumerable<VehiclesDto>> GetEstate()
        {
            {
                var vehiclesFromRepo = _cruzRojaRepository.GetList();
                return Ok(vehiclesFromRepo);
            }

        }

        //Obtener Estate por ID
        [HttpGet("{VehiclesID}", Name = "GetVehicles")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetVehicle(int EstateID)
        {
            var vehiclesFromRepo = _cruzRojaRepository.GetListId(EstateID);


            //Si el Id del Usuario no existe se retorna Error.
            if (vehiclesFromRepo == null)
            {
                return NotFound();
            }

            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
            return Ok(vehiclesFromRepo);
        }

        [HttpPost]
        //[Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult<VehiclesDto> CreateVehicle(VehiclesForCreation_UpdateDto vehicles)

        {
            //Se usa User para posteriormente almacenar los valores ingresados a la Base de datos

            var vehiclesEntity = _mapper.Map<Entities.Vehicles>(vehicles);

            /*llamo al metodo AddUser para comprobar que los datos que se ingresaroSn 
             del nuevo Usuario cumplan con los requisitos*/
            _cruzRojaRepository.Add(vehiclesEntity);
            _cruzRojaRepository.save();

            var authorToReturn = _mapper.Map<VehiclesDto>(vehiclesEntity);

            //La Operacion de añadir un Usuario se retorna con exito
            return Ok();
        }

        [HttpPatch("{VehiclesID}")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]

        public ActionResult UpdatePartialVehicles(int VehiclesID, JsonPatchDocument<VehiclesForCreation_UpdateDto> patchDocument)
        {
            var vehiclesFromRepo = _cruzRojaRepository.GetListId(VehiclesID);
            if (vehiclesFromRepo == null)
            {
                return NotFound();
            }

            var vehiclesToPatch = _mapper.Map<VehiclesForCreation_UpdateDto>(vehiclesFromRepo);

            patchDocument.ApplyTo(vehiclesToPatch, ModelState);

            if (!TryValidateModel(vehiclesToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(vehiclesToPatch, vehiclesFromRepo);

            _cruzRojaRepository.Update(vehiclesFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la actualizacion del Usuario especificado
            return Ok();
        }

        //Borrar Estate
        [HttpDelete("{VehiclesID}")]
        //[Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult Delete(int EstateID)
        {

            var estateFromRepo = _cruzRojaRepository.GetListId(EstateID);


            // si el Id del Usuario no existe de retorna Error.
            if (estateFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository.Delete(estateFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return Ok();

        }
    }

}
