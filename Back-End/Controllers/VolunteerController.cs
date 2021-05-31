//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Back_End.Entities;
//using Back_End.Models;
//using Back_End.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;

//namespace Back_End.Controllers
//{
//    [Route("api/resources/[controller]")]
//    [ApiController]
//    public class VolunteerController : BaseApiController
//    {

//        private readonly ICruzRojaRepository<Volunteer> _cruzRojaRepository;
//        private readonly IMapper _mapper;

//        public VolunteerController(ICruzRojaRepository<Volunteer> cruzRojaRepository, IMapper mapper)

//        {
//            _cruzRojaRepository = cruzRojaRepository ??
//                throw new ArgumentNullException(nameof(VolunteersRepository));

//            _mapper = mapper ??
//               throw new ArgumentNullException(nameof(mapper));
//        }


//        [HttpGet]
//        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
//        public ActionResult<IEnumerable<VolunteerDto>> GetVolunteer()
//        {
//            {
//                var volunteerFromRepo = _cruzRojaRepository.GetList();
//                return Ok(volunteerFromRepo);
//            }

//        }

//        //Obtener Estate por ID
//        [HttpGet("{VolunteerID}", Name = "GetVolunteer")]
//        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
//        public IActionResult GetVolunteer(int VolunteerID)
//        {
//            var volunteerFromRepo = _cruzRojaRepository.GetListId(VolunteerID);


//            //Si el Id del Usuario no existe se retorna Error.
//            if (volunteerFromRepo == null)
//            {
//                return NotFound();
//            }

//            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
//            return Ok(volunteerFromRepo);
//        }

//        [HttpPost]
//        //[Authorize(Roles = "Coordinador General, Admin")]
//        public ActionResult<VolunteerDto> CreateVolunteer(VolunteerForCreation_UpdateDto volunteer)

//        {
//            //Se usa User para posteriormente almacenar los valores ingresados a la Base de datos

//            var volunteerEntity = _mapper.Map<Entities.Volunteer>(volunteer);

//            /*llamo al metodo AddUser para comprobar que los datos que se ingresaroSn 
//             del nuevo Usuario cumplan con los requisitos*/
//            _cruzRojaRepository.Add(volunteerEntity);
//            _cruzRojaRepository.save();

//            var authorToReturn = _mapper.Map<EstateDto>(volunteerEntity);

//            //La Operacion de añadir un Usuario se retorna con exito
//            return Ok();
//        }

//        [HttpPatch("{VolunteerID}")]
//        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]

//        public ActionResult UpdatePartialVoluteer(int VolunteerID, JsonPatchDocument<VolunteerForCreation_UpdateDto> patchDocument)
//        {
//            var volunteerFromRepo = _cruzRojaRepository.GetListId(VolunteerID);
//            if (volunteerFromRepo == null)
//            {
//                return NotFound();
//            }

//            var volunteerToPatch = _mapper.Map<VolunteerForCreation_UpdateDto>(volunteerFromRepo);

//            patchDocument.ApplyTo(volunteerToPatch, ModelState);

//            if (!TryValidateModel(volunteerToPatch))
//            {
//                return ValidationProblem(ModelState);
//            }

//            _mapper.Map(volunteerToPatch, volunteerFromRepo);

//            _cruzRojaRepository.Update(volunteerFromRepo);

//            _cruzRojaRepository.save();

//            // Se retorna con exito la actualizacion del Usuario especificado
//            return Ok();
//        }

//        //Borrar Estate
//        [HttpDelete("{VolunteerID}")]
//        //[Authorize(Roles = "Coordinador General, Admin")]
//        public ActionResult Delete(int EstateID)
//        {

//            var volunteerFromRepo = _cruzRojaRepository.GetListId(EstateID);


//            // si el Id del Usuario no existe de retorna Error.
//            if (volunteerFromRepo == null)
//            {
//                return NotFound();
//            }

//            _cruzRojaRepository.Delete(volunteerFromRepo);

//            _cruzRojaRepository.save();

//            // Se retorna con exito la eliminacion del Usuario especificado
//            return Ok();

//        }
//    }

//}

