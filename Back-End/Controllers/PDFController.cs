using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.PDF___Dto;
using Entities.Models;
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
                    item.LocationFile = $"https://almacenamientotesis.blob.core.windows.net/publicpdf/{item.Location}";                }

                return Ok(pdfResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<PDF>> PDF([FromForm] PDFForCreationDto pdf)
        {
            try
            {
                if (pdf == null)
                {
                    _logger.LogError("PDF object sent from client is null.");
                    return BadRequest("Material object is null");
                }

                var pdfEntity = _mapper.Map<PDF>(pdf);
                
                await _repository.PDF.Upload(pdf);

                _repository.PDF.Create(pdfEntity);


                _repository.PDF.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside SavePDF action: {ex.Message}");
                return StatusCode(500, "Internal server error");
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


    }
}
