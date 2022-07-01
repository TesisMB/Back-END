using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.PDF___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly CruzRojaContext _cruzRojaContext = new CruzRojaContext();
        private readonly IRepositorWrapper _repository;
        private readonly IMapper  _mapper;

        public PDFController(ILoggerManager logger, CruzRojaContext cruzRojaContext, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _cruzRojaContext = cruzRojaContext;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<PDF>> GetAllPDF([FromQuery] int userId)
        {
                //var folderName = Path.Combine("StaticFiles", "images", "PDF");
                //var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                //var pdf = Directory.EnumerateFiles(pathToRead)
                //    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)))
                //     .Where(UploadController.IsPDFFile);
                //return Ok(new { pdf });


                //return File(imgBytes, "application/pdf ");

                //return File(imgBytes, "image/webp");

            try
            {

                var pdfAll = await _repository.PDF.GetAllPDF(userId);


                _logger.LogInfo($"Returned all PDF from database.");

                var pdfResult = _mapper.Map<IEnumerable<PDFDto>>(pdfAll);

                foreach (var item in pdfResult)
                {
                    item.LocationFile = $"https://almacenamientotesis.blob.core.windows.net/publicpdf/{item.Location}";             
                }

                return Ok(pdfResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<PDF>> PDF([FromBody] PDFForCreationDto pdf)
        {
            try
            {
                if (pdf == null)
                {
                    _logger.LogError("PDF object sent from client is null.");
                    return BadRequest("Material object is null");
                }

                var pdfEntity = _mapper.Map<PDF>(pdf);
                
                //await _repository.PDF.Upload(pdf);

                _repository.PDF.CreatePDF(pdfEntity);

                _repository.PDF.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside SavePDF action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet]
        //[Route("download")]
        //public async Task<IActionResult> Download([FromQuery] string fileUrl)
        //{

        //    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);

        //    //if (!System.IO.File.Exists(filePath))
        //    //    return NotFound();

        //    //var memory = new MemoryStream();
        //    //await using (var stream = new FileStream(filePath, FileMode.Open))
        //    //{
        //    //    await stream.CopyToAsync(memory);
        //    //}
        //    //memory.Position = 0;

        //    //return File(memory, GetContentType(filePath), filePath);
        //}

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string fileName)
        {
            var imagBytes = await _repository.PDF.Get(fileName);
            return new FileContentResult(imagBytes, "application/pdf")
            {
                FileDownloadName = Guid.NewGuid().ToString() + ".pdf",
            };
        }


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }


        [HttpPatch("{pdfId}")]
        public async Task<ActionResult> UpdatePartialUser(int pdfId, JsonPatchDocument<PDFForUpdateDto> _pdf)
        {

            try
            {

                var pdfEntity = await _repository.PDF.GetPdf(pdfId);

                if (pdfEntity == null)
                {
                    _logger.LogError($"PDF with id: {pdfId}, hasn't been found in db.");
                    return NotFound();
                }


                var pdfToPatch = _mapper.Map<PDFForUpdateDto>(pdfEntity);

                 pdfToPatch.PDFDateModified = DateTime.Now;

                //se aplican los cambios recien aca
                _pdf.ApplyTo(pdfToPatch, ModelState);


                if (!TryValidateModel(pdfToPatch))
                {
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }


                var employeeResult = _mapper.Map(pdfToPatch, pdfEntity);

                _repository.PDF.UpdatePDF(employeeResult);

                _repository.PDF.SaveAsync();

                return NoContent();
            }


            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatMaterial action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }



        //********************************* FUNCIONANDO *********************************
        [HttpDelete("{pdfId}")]
        public async Task<ActionResult> DeletePdf(int pdfId)
        {

            try
            {
                var pdf = await _repository.PDF.GetPdf(pdfId);

                if (pdf == null)
                {
                    _logger.LogError($"PDF with id: {pdfId}, hasn't ben found in db.");
                    return NotFound();
                }


                _repository.PDF.Delete(pdf);

                _repository.PDF.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePDF action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
