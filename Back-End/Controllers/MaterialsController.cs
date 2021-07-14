using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/
        public MaterialsController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        // GET: api/<MaterialsController>
        [HttpGet]
        public IActionResult GetAllVolunteers()
        {
            try
            {
                var volunteers = _repository.Materials.GetAllMaterials();

                _logger.LogInfo($"Returned all Materials from database.");

                var volunteersResult = _mapper.Map<IEnumerable<MaterialsDto>>(volunteers);

                return Ok(volunteersResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllMaterials action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{materialId}")]
        public IActionResult GetMaterial(int materialId)
        {
            try
            {

                var volunteer = _repository.Materials.GetMaterialWithDetails(materialId);

                if (volunteer == null)

                {
                    _logger.LogError($"Material with id: {materialId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned Material with id: {materialId}");
                    var volunteerResult = _mapper.Map<MaterialsDto>(volunteer);
                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetMaterialById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        // POST api/<MaterialsController>
        [HttpPost]
        public IActionResult CreateMaterial([FromBody] MaterialsForCreationDto material)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if (material == null)

                {
                    _logger.LogError("Material object sent from client is null.");
                    return BadRequest("Material object is null");

                }

                var materialEntity = _mapper.Map<Materials>(material);


                _repository.Materials.Create(materialEntity);

                _repository.Save();

                var createdVolunteer = _mapper.Map<MaterialsDto>(materialEntity);

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize(Roles = "Coordinador General, Admin")] 
        [HttpPatch("{materialId}")]
        public IActionResult UpdatePartialUser(int materialId, JsonPatchDocument<MaterialsForUpdateDto> _materials)
        {

            try
            {

                var materialEntity = _repository.Materials.GetMaterialById(materialId);

                if (materialEntity == null)
                {
                    _logger.LogError($"Material with id: {materialId}, hasn't been found in db.");
                    return NotFound();
                }

                var materialToPatch = _mapper.Map<MaterialsForUpdateDto>(materialEntity);

                _materials.ApplyTo(materialToPatch, ModelState);


                if (!TryValidateModel(materialToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                var employeeResult = _mapper.Map(materialToPatch, materialEntity);

                _repository.Materials.Update(employeeResult);
                _repository.Save();

                return NoContent();

            }


            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }
        [HttpDelete("{materialId}")]
        public IActionResult DeleteEmployee(int materialId)
        {

            try
            {
                var material = _repository.Materials.GetMaterialById(materialId);

                if (material == null)
                {
                    _logger.LogError($"Material with id: {materialId}, hasn't ben found in db.");
                    return NotFound();
                }

                /*if (_repository.Vehicles.VehciclesByEmployees(employeeId).Any())
                {
                    _logger.LogError($"Cannot delete employee with id: {employeeId}. It has related {_repository.Vehicles}. Delete those accounts first");
                    return BadRequest();
                }*/

                _repository.Materials.Delete(material);

                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
