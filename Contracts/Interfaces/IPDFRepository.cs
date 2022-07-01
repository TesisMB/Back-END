using Entities.DataTransferObjects.PDF___Dto;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IPDFRepository: IRepositoryBase<PDF>
    {
        Task<IEnumerable<PDF>> GetAllPDF(int userId);
        Task<PDF> GetPdf(int pdfId);

        Task Upload(PDFForCreationDto pdf);


        Task<byte[]> Get(string imageName);

        void CreatePDF(PDF pdf);

        void UpdatePDF(PDF pdf);

        void DeletePdf(PDF pdf);

    }

}
