using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [ApiController]

    public class UploadController : ControllerBase
    {
        [HttpPost]
        [Route("api/upload")]

        public static async Task<string> SaveImage(IFormFile Image, string tipo)
        {
            {

                tipo = "Resources";

                var folderName = Path.Combine("StaticFiles", "images", tipo);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = ContentDispositionHeaderValue.Parse(Image.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);

                }
                return fileName;
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
