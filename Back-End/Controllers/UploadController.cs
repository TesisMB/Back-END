using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [ApiController]
    public class UploadController : ControllerBase
    {
        [NonAction]
        public static async Task<string> SaveImage(IFormFile Image)
        {
            {

                var folderName = Path.Combine("StaticFiles", "Images");
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
    }
}
