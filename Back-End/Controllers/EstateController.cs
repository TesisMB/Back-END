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
    public class EstateController : BaseApiController
    { 

        private readonly ICruzRojaRepository<Estate> _cruzRojaRepository;
        private readonly IMapper _mapper;

        public EstateController(ICruzRojaRepository<Estate> cruzRojaRepository, IMapper mapper)

        {
            _cruzRojaRepository = cruzRojaRepository ??
                throw new ArgumentNullException(nameof(UsersRepository));

            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult<IEnumerable<EstateDto>> GetEstate()
        {
            {
                var estateFromRepo = _cruzRojaRepository.GetList();
                return Ok(estateFromRepo);
            }

        }
        
        //Obtener Estate por ID
        [HttpGet("{EstateID}", Name = "GetEstate")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetEstate(int EstateID)
        {
            var estateFromRepo = _cruzRojaRepository.GetListId(EstateID);


            //Si el Id del Usuario no existe se retorna Error.
            if (estateFromRepo == null)
            {
                return NotFound();
            }

            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
            return Ok(estateFromRepo);
        }

        [HttpPost]
        //[Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult<EstateDto> CreateUser(EstateForCreation_UpdateDto estate)
            
        {
            //Se usa User para posteriormente almacenar los valores ingresados a la Base de datos
            
            var estateEntity = _mapper.Map<Entities.Estate>(estate);

            /*llamo al metodo AddUser para comprobar que los datos que se ingresaroSn 
             del nuevo Usuario cumplan con los requisitos*/
            _cruzRojaRepository.Add(estateEntity);
            _cruzRojaRepository.save();

            var authorToReturn = _mapper.Map<EstateDto>(estateEntity);

            //La Operacion de añadir un Usuario se retorna con exito
            return Ok();
        }

        [HttpPatch("{EstateID}")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]

        public ActionResult UpdatePartialEstate(int EstateID, JsonPatchDocument<EstateForCreation_UpdateDto> patchDocument)
        {
            var estateFromRepo = _cruzRojaRepository.GetListId(EstateID);
            if (estateFromRepo == null)
            {
                return NotFound();
            }

            var estateToPatch = _mapper.Map<EstateForCreation_UpdateDto>(estateFromRepo);

            patchDocument.ApplyTo(estateToPatch, ModelState);

            if (!TryValidateModel(estateToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(estateToPatch, estateFromRepo);

            _cruzRojaRepository.Update(estateFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la actualizacion del Usuario especificado
            return Ok();
        }

        //Borrar Estate
        [HttpDelete("{EstateID}")]
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

