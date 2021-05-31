//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Back_End.Entities;
//using Back_End.Services;
//using Back_End.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using AutoMapper;
//using Microsoft.AspNetCore.JsonPatch;

//namespace Back_End.Controllers
//{
//    [Route("api/resources/[controller]")]
//    [ApiController]
//    public class MaterialsController : BaseApiController
//    {
//        private readonly ICruzRojaRepository<Materials> _cruzRojaRepository;
//        private readonly IMapper _mapper;
//        public MaterialsController(ICruzRojaRepository<Materials> cruzRojaRepository, IMapper mapper)

//        {
//            _cruzRojaRepository = cruzRojaRepository ??
//                throw new ArgumentNullException(nameof(MaterialsRepository));

//            _mapper = mapper ??
//               throw new ArgumentNullException(nameof(mapper));
//        }


//        [HttpGet]
//        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
//        public ActionResult<IEnumerable<MaterialsDto>> GetMaterials()
//        {
//            {
//                var materialsFromRepo = _cruzRojaRepository.GetList();
//                return Ok(materialsFromRepo);
//            }

//        }

//        //Obtener Estate por ID
//        [HttpGet("{MaterialsID}", Name = "GetMaterials")]
//        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
//        public IActionResult GetMaterials(int MaterialsID)
//        {
//            var materialsFromRepo = _cruzRojaRepository.GetListId(MaterialsID);


//            //Si el Id del Usuario no existe se retorna Error.
//            if (materialsFromRepo == null)
//            {
//                return NotFound();
//            }

//            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
//            return Ok(materialsFromRepo);
//        }

//        [HttpPost]
//        //[Authorize(Roles = "Coordinador General, Admin")]
//        public ActionResult<MaterialsDto> CreateMedicine(MaterialsForCreation_UpdateDto materials)

//        {
//            //Se usa User para posteriormente almacenar los valores ingresados a la Base de datos

//            var materialsEntity = _mapper.Map<Entities.Materials>(materials);

//            /*llamo al metodo AddUser para comprobar que los datos que se ingresaroSn 
//             del nuevo Usuario cumplan con los requisitos*/
//            _cruzRojaRepository.Add(materialsEntity);
//            _cruzRojaRepository.save();

//            var authorToReturn = _mapper.Map <MaterialsDto>(materialsEntity);

//            //La Operacion de añadir un Usuario se retorna con exito
//            return Ok();
//        }

//        [HttpPatch("{MaterialsID}")]
//        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]

//        public ActionResult UpdatePartialMaterials(int MaterialsID, JsonPatchDocument<MaterialsForCreation_UpdateDto> patchDocument)
//        {
//            var materialsFromRepo = _cruzRojaRepository.GetListId(MaterialsID);
//            if (materialsFromRepo == null)
//            {
//                return NotFound();
//            }

//            var materialsToPatch = _mapper.Map<MaterialsForCreation_UpdateDto>(materialsFromRepo);

//            patchDocument.ApplyTo(materialsToPatch, ModelState);

//            if (!TryValidateModel(materialsToPatch))
//            {
//                return ValidationProblem(ModelState);
//            }

//            _mapper.Map(materialsToPatch, materialsFromRepo);

//            _cruzRojaRepository.Update(materialsFromRepo);

//            _cruzRojaRepository.save();

//            // Se retorna con exito la actualizacion del Usuario especificado
//            return Ok();
//        }

//        //Borrar Estate
//        [HttpDelete("{MaterialsID}")]
//        //[Authorize(Roles = "Coordinador General, Admin")]
//        public ActionResult Delete(int MaterialsID)
//        {

//            var materialsFromRepo = _cruzRojaRepository.GetListId(MaterialsID);


//            // si el Id del Usuario no existe de retorna Error.
//            if (materialsFromRepo == null)
//            {
//                return NotFound();
//            }

//            _cruzRojaRepository.Delete(materialsFromRepo);

//            _cruzRojaRepository.save();

//            // Se retorna con exito la eliminacion del Usuario especificado
//            return Ok();

//        }

//    }
//}

