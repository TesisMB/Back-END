using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PDF_Generator.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Wkhtmltopdf.NetCore;

namespace Back_End.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EstatesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private byte[] pdf;
        private readonly IMapper _mapper;
        private IConverter _converter;
        private ObjectSettings objectSettings = new ObjectSettings();
        private GlobalSettings globalSettings = new GlobalSettings();
        public EstatesController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _converter = converter;

        }



        [HttpGet("Estates")]
        public async Task<ActionResult<Estates>> GetAllEstatesType()
        {
            try
            {
                var employees = await _repository.Estates.GetAllEstates();
                _logger.LogInfo($"Returned all estates from database.");

                var employeesResult = _mapper.Map<IEnumerable<EstatesTypeDto>>(employees);
                return Ok(employeesResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEstates action: {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }


        [HttpGet("PDF/{estateId}")]
        public IActionResult CreatePDF(int estateId)
        {

            //quien es el actual usuario
            Users user = null;
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            user = cruzRojaContext.Users
                    .Where(x => x.UserID == estateId)
                    .AsNoTracking()
                    .FirstOrDefault();

         if(estateId != 2)
            {
                var estates = _repository.Estates.GetAllEstateByPdf(estateId);

                estates.Materials =
                            from x in estates.Materials
                            where x.MaterialQuantity != 0
                            select x;

                estates.Medicines =
                          from x in estates.Medicines
                          where x.MedicineExpirationDate > DateTime.Now && x.MedicineQuantity != 0
                          select x;

                estates.Vehicles =
                       from x in estates.Vehicles
                       where x.VehicleAvailability != false
                       select x;

                objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = Resource.GetHTMLString(estates),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesForResource.css") },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Right = "[page]", Line = true, },
                };

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = $"Reporte de recursos - {estates.EstateTypes} {estates.Locations.LocationCityName}",
                };

            }else
            {
                var estates = _repository.Estates.GetAllEstatesByPdf();

                foreach (var item in estates)
                {
                    item.Materials =
                                from x in item.Materials
                                where x.MaterialQuantity == 0
                                select x;

                    item.Medicines =
                              from x in item.Medicines
                              where x.MedicineQuantity == 0
                              select x;

                    item.Vehicles =
                           from x in item.Vehicles
                           where x.VehicleAvailability != true
                           select x;
                }

                objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = Resources.GetHTMLString(estates),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesForResource.css") },
                    //HeaderSettings = { HtmUrl = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ResourcesInfo.html") },
                    //FooterSettings = { FontName = "Arial", FontSize = 9, Right = "[page]", Line = true}
                     //HeaderSettings = { HtmUrl = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ResourcesInfo.html"),Spacing= 1.8},
                    //FooterSettings = { HtmUrl = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ResourcesInfo.html") },  
                    FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
                };

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Reporte de recursos",
                };
            }





            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf");
        }


        
    }
}


