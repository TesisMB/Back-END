using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.PDF___Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PDFRepository : RepositoryBase<PDF>, IPDFRepository
    {
        private CruzRojaContext _cruzRojaContext;
        private readonly BlobServiceClient _blobServiceClient;

        public PDFRepository(CruzRojaContext cruzRojaContext, BlobServiceClient blobServiceClient) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
            _blobServiceClient = blobServiceClient;

        }
        public async Task<IEnumerable<PDF>> GetAllPDF(int userId)
        {
            //var user = EmployeesRepository.GetAllEmployeesById(userId);

            //var collection = _cruzRojaContext.PDF as IQueryable<PDF>;

            //collection = collection.Where(
            //                                a => a.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

            return await FindAll()
                .Include(a=> a.EmergenciesDisasters)
                .ThenInclude(a=> a.TypesEmergenciesDisasters)

                .Include(a => a.EmergenciesDisasters)
                .ThenInclude(a => a.Alerts)


                .Include(a => a.EmergenciesDisasters)
                .ThenInclude(a => a.LocationsEmergenciesDisasters)
               .ToListAsync();
        }

        public async Task Upload(PDFForCreationDto pdf)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("publicpdf");

            var blobClient = blobContainer.GetBlobClient(pdf.LocationFile.FileName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "application/pdf" };

            //await blobClient.UploadAsync(pdf.LocationFile.OpenReadStream());

            await blobClient.UploadAsync(pdf.LocationFile.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHttpHeader });
        }


        public async Task<byte[]> Get(string imageName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("publicpdf");

            var blobClient = blobContainer.GetBlobClient(imageName);
            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public void CreatePDF(PDF pdf)
        {
            Create(pdf);
        }
    }
}
