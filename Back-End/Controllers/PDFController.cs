using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
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
        public PDFController(ILoggerManager logger, CruzRojaContext cruzRojaContext)
        {
            _logger = logger;
            _cruzRojaContext = cruzRojaContext;
        }


        [HttpGet]
        public ActionResult<PDF> GetPDF()
        {
            try
            {
                var folderName = Path.Combine("StaticFiles", "images", "PDF");
                var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var pdf = Directory.EnumerateFiles(pathToRead)
                    .Select(fullPath => Path.Combine(folderName, Path.GetFileName(fullPath)))
                     .Where(UploadController.IsPDFFile);
                return Ok(new { pdf });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        //[HttpPost]
        //[Route("upload")]
        //public async Task<ActionResult<PDF>> PDF([FromForm] PDF pdf)
        //{
        //    try
        //    {
        //        if (pdf == null)
        //        {
        //            _logger.LogError("PDF object sent from client is null.");
        //            return BadRequest("Material object is null");
        //        }

        //        pdf.Location = await UploadController.SaveImage(pdf.LocationFile, "PDF");

        //        _cruzRojaContext.Add(pdf);

        //        _cruzRojaContext.SaveChanges();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Something went wrong inside SavePDF action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string fileUrl)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), filePath);
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
