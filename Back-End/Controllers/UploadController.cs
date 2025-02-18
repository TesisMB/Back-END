﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/upload/")]
    [ApiController]
    public class UploadController : ControllerBase
    {
    private readonly BlobServiceClient _blobServiceClient;

        public UploadController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<string> SaveImage()
        {

            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[5];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            var name = BitConverter.ToString(randomBytes).Replace("-", "");

            var Image = Request.Form.Files[0];


                var blobContainer = _blobServiceClient.GetBlobContainerClient("publicuploads");

                var blobClient = blobContainer.GetBlobClient(name);

                var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };

                await blobClient.UploadAsync(Image.OpenReadStream());

                await blobClient.UploadAsync(Image.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHttpHeader });

                //var fileName = ContentDispositionHeaderValue.Parse(Image.ContentDisposition).FileName.Trim('"');


            //return Ok();

            //{
            //    tipo = "Resources";
            //    var folderName = Path.Combine("StaticFiles", "images", tipo);
            //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            //    var fileName = ContentDispositionHeaderValue.Parse(Image.ContentDisposition).FileName.Trim('"');
            //    var fullPath = Path.Combine(pathToSave, fileName);
            //    var dbPath = Path.Combine(folderName, fileName);

            //    //crear nueva funcion
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        await Image.CopyToAsync(stream);
            //    }
                //

                return name;
        }

        [HttpPost("pdf"), DisableRequestSizeLimit]
        //[HttpPost("pdf")]
        public async Task<string> SavePDF()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[5];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            var name = BitConverter.ToString(randomBytes).Replace("-", "");

            var pdf = Request.Form.Files[0];


            var blobContainer = _blobServiceClient.GetBlobContainerClient("publicpdf");

            var blobClient = blobContainer.GetBlobClient(name);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "application/pdf" };

            //await blobClient.UploadAsync(pdf.LocationFile.OpenReadStream());

            await blobClient.UploadAsync(pdf.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHttpHeader });

            //var fileName = ContentDispositionHeaderValue.Parse(pdf.ContentDisposition).FileName.Trim('"');


            //return Ok();

            //{
            //    tipo = "Resources";
            //    var folderName = Path.Combine("StaticFiles", "images", tipo);
            //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            //    var fileName = ContentDispositionHeaderValue.Parse(Image.ContentDisposition).FileName.Trim('"');
            //    var fullPath = Path.Combine(pathToSave, fileName);
            //    var dbPath = Path.Combine(folderName, fileName);

            //    //crear nueva funcion
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        await Image.CopyToAsync(stream);
            //    }
            //

            return name;
        }

        //[HttpPost, DisableRequestSizeLimit]
        //public IActionResult Upload()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        var folderName = Path.Combine("StaticFiles", "images", "Resources");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(dbPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return Ok(new { fileName });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}
       

                [NonAction]

        public static bool IsPDFFile(string fileName)
        {
            return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
                   //|| fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   //|| fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

    }
}
