using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Materials___Dto;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Back_End.Controllers
{
    [Route("api/Materiales")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositorWrapper _repository;
        private readonly IMapper _mapper;

        /*Este metodo va a permitir despues poder conectarme tanto para mapear, como para obtener 
         las funciones que se establecieron repositorios correspondientes*/
        public MaterialsController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {

            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }



        //********************************* FUNCIONANDO *********************************
        [HttpGet]
        public async Task<ActionResult<Materials>> GetAllVolunteers([FromQuery] int userId)
        {
            try
            {
                var materials = await _repository.Materials.GetAllMaterials(userId);

                _logger.LogInfo($"Returned all Materials from database.");

                var materialsResult = _mapper.Map<IEnumerable<Resources_Dto>>(materials);

                foreach (var item in materialsResult)
                {
                    if (item.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        item.Picture = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{item.Picture}";
                    }

                }

                return Ok(materialsResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllMaterials action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");

            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpGet("{materialId}")]
        public async Task<ActionResult<Materials>> GetMaterial(string materialId)
        {
            try
            {

                var volunteer = await _repository.Materials.GetMaterialWithDetails(materialId);

                if (volunteer == null)

                {
                    _logger.LogError($"Material with id: {materialId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned Material with id: {materialId}");

                    var volunteerResult = _mapper.Map<Resources_Dto>(volunteer);


                    if (volunteerResult.Picture != "https://i.imgur.com/S9HJEwF.png")
                    {
                        volunteerResult.Picture = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{volunteerResult.Picture}";

                    }
                    return Ok(volunteerResult);
                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetMaterialById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPost()]
        public async Task<ActionResult<Materials>> CreateMaterial([FromBody] Resources_ForCreationDto material)
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

                if (material.Picture == null)
                {
                    materialEntity.MaterialPicture = "https://i.imgur.com/S9HJEwF.png";
                }
                else
                {
                    materialEntity.MaterialPicture = material.Picture;
                }


                _repository.Materials.CreateMaterial(materialEntity);
                _repository.Materials.SaveAsync();

                return Ok();

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside CreateMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPatch("{materialId}")]
        public async Task<ActionResult> UpdatePartialUser(string materialId, JsonPatchDocument<MaterialsForUpdateDto> _materials)
        {

            try
            {

                var materialEntity = await _repository.Materials.GetMaterialById(materialId);

                if (materialEntity == null)
                {
                    _logger.LogError($"Material with id: {materialId}, hasn't been found in db.");
                    return NotFound();
                }


                var materialToPatch = _mapper.Map<MaterialsForUpdateDto>(materialEntity);

                materialToPatch.DateModified = DateTime.Now;

                //se aplican los cambios recien aca
                _materials.ApplyTo(materialToPatch, ModelState);


                if (!TryValidateModel(materialToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }
            

                var employeeResult = _mapper.Map(materialToPatch, materialEntity);

                _repository.Materials.UpdateMaterial(employeeResult);

                _repository.Materials.SaveAsync();

                return NoContent();

            }


            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpDelete("{materialId}")]
        public async Task<ActionResult> DeleteEmployee(string materialId)
        {

            try
            {
                var material = await _repository.Materials.GetMaterialById(materialId);

                if (material == null)
                {
                    _logger.LogError($"Material with id: {materialId}, hasn't ben found in db.");
                    return NotFound();
                }


                _repository.Materials.Delete(material);

                 _repository.Materials.SaveAsync();

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
