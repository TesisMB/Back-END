using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
using Back_End.Services;
using Back_End.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Back_End.Controllers
{
    [Route("api/resources/[controller]")]
    [ApiController]
    public class MedicineController : BaseApiController
    {
        private readonly ICruzRojaRepository<Medicine> _cruzRojaRepository;
        private readonly IMapper _mapper;
        public MedicineController(ICruzRojaRepository<Medicine> cruzRojaRepository, IMapper mapper)

        {
            _cruzRojaRepository = cruzRojaRepository ??
                throw new ArgumentNullException(nameof(MedicineRepository));

            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        //[Authorize(Roles = "Coordinador General, Admin")]  //Autorizo unicamente los usuarios que tenga el permiso de listar los usuarios
        public ActionResult<IEnumerable<MedicineDto>> GetMedicine()
        {
            {
                var medicineFromRepo = _cruzRojaRepository.GetList();
                return Ok(medicineFromRepo);
            }

        }

        //Obtener Estate por ID
        [HttpGet("{MedicineID}", Name = "GetMedicine")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]
        public IActionResult GetMedicine(int MedicineID)
        {
            var medicineFromRepo = _cruzRojaRepository.GetListId(MedicineID);


            //Si el Id del Usuario no existe se retorna Error.
            if (medicineFromRepo == null)
            {
                return NotFound();
            }

            //Al momento de mapear utilizo UsersDto para devolver aquellos valores imprecidibles
            return Ok(medicineFromRepo);
        }

        [HttpPost]
        //[Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult<MedicineDto> CreateMedicine(MedicineForCreation_UpdateDto medicine)

        {
            //Se usa User para posteriormente almacenar los valores ingresados a la Base de datos

            var medicineEntity = _mapper.Map<Entities.Medicine>(medicine);

            /*llamo al metodo AddUser para comprobar que los datos que se ingresaroSn 
             del nuevo Usuario cumplan con los requisitos*/
            _cruzRojaRepository.Add(medicineEntity);
            _cruzRojaRepository.save();

            var authorToReturn = _mapper.Map<MedicineDto>(medicineEntity);

            //La Operacion de añadir un Usuario se retorna con exito
            return Ok();
        }

        [HttpPatch("{MedicineID}")]
        //[Authorize(Roles = "Coordinador General, Admin, Coordinador de Emergencias y Desastres, Encargado de Logistica")]

        public ActionResult UpdatePartialMedicine(int MedicineID, JsonPatchDocument<MedicineForCreation_UpdateDto> patchDocument)
        {
            var medicineFromRepo = _cruzRojaRepository.GetListId(MedicineID);
            if (medicineFromRepo == null)
            {
                return NotFound();
            }

            var medicineToPatch = _mapper.Map<MedicineForCreation_UpdateDto>(medicineFromRepo);

            patchDocument.ApplyTo(medicineToPatch, ModelState);

            if (!TryValidateModel(medicineToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(medicineToPatch, medicineFromRepo);

            _cruzRojaRepository.Update(medicineFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la actualizacion del Usuario especificado
            return Ok();
        }

        //Borrar Estate
        [HttpDelete("{MedicineID}")]
        //[Authorize(Roles = "Coordinador General, Admin")]
        public ActionResult Delete(int MedicineID)
        {

            var medicineFromRepo = _cruzRojaRepository.GetListId(MedicineID);


            // si el Id del Usuario no existe de retorna Error.
            if (medicineFromRepo == null)
            {
                return NotFound();
            }

            _cruzRojaRepository.Delete(medicineFromRepo);

            _cruzRojaRepository.save();

            // Se retorna con exito la eliminacion del Usuario especificado
            return Ok();

        }

    }
}
