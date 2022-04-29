using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/upload")]
    [ApiController]

    public class UploadController : ControllerBase
    {
        [NonAction]
        public static async Task<string> SaveImage([FromForm] IFormFile Image, string tipo)
        {
            {
                tipo = "Resources";
                var folderName = Path.Combine("StaticFiles", "images", tipo);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = ContentDispositionHeaderValue.Parse(Image.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                //crear nueva funcion
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                //

                return fileName;
            }

        }


        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "images", "Resources");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(dbPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { fileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
       

                [NonAction]

        public static bool IsPDFFile(string fileName)
        {
            return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
                   //|| fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   //|| fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

    }
}
