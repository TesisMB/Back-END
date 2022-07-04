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
    [Route("api/Estates")]
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

        [HttpGet]
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
        public IActionResult CreatePDF(int estateId, [FromQuery] string dateStart, [FromQuery] string? dateEnd )
        {
            //quien es el actual usuario
            Users user = null;
            CruzRojaContext cruzRojaContext = new CruzRojaContext();
            DateTime dateConvert, dateConvertEnd;
            dateConvertEnd = Convert.ToDateTime("01/01/0001");

                        //Fecha de comienzo
                        dateStart = dateStart.Substring(4, 11);
                        var monthStart = dateStart.Substring(0, 3);
                        var dayStart = dateStart.Substring(3, 4);
                        var yearStart = dateStart.Substring(6, 5);

                        monthStart = _repository.Estates.Date(monthStart);

                        var dateStartNew = dayStart + "/" + monthStart + "/" + yearStart;
                        dateConvert = Convert.ToDateTime(dateStartNew);


                    //Fecha de finalizacion Opcional
                    if (dateEnd != null)
                    {
                        dateEnd = dateEnd.Substring(4, 11);
                        var monthEnd= dateEnd.Substring(0, 3);
                        var dayEnd = dateEnd.Substring(3, 4);
                        var yearEnd = dateEnd.Substring(6, 5);

                        monthEnd = _repository.Estates.Date(monthEnd);
                        Console.WriteLine("Mes final " + monthEnd);
                        var dateEndNew = dayEnd + "/" + monthEnd + "/" + yearEnd;
                        dateConvertEnd = Convert.ToDateTime(dateEndNew);
                    }

                         Console.WriteLine("Fecha" + dateConvertEnd);


            user = cruzRojaContext.Users
                    .Where(x => x.UserID == estateId)
                    .AsNoTracking()
                    .FirstOrDefault();

            if (estateId != 2)
            {
                var estates = _repository.Estates.GetAllEstateByPdf(estateId);

                var materiales = _repository.Materials.GetAllMaterials(dateConvert, dateConvertEnd, estateId);
                var medicamentos = _repository.Medicines.GetAllMedicines(dateConvert, dateConvertEnd, estateId);
                var vehiculos = _repository.Vehicles.GetAllVehicles(dateConvert, dateConvertEnd, estateId);

                if (String.IsNullOrEmpty(dateEnd))
                {
                    estates.Materials =
                                from x in estates.Materials
                                where x.MaterialDateCreated >= dateConvert && x.MaterialAvailability != false
                                && x.FK_EstateID == estates.EstateID
                                select x;

                    estates.Medicines =
                              from x in estates.Medicines
                              where x.MedicineDateCreated >= dateConvert
                              && x.MedicineAvailability != false
                              && x.FK_EstateID == estates.EstateID
                              select x;

                    estates.Vehicles =
                           from x in estates.Vehicles
                           where x.VehicleDateCreated >= dateConvert 
                           && x.VehicleAvailability != false
                           && x.FK_EstateID == estates.EstateID
                           select x;
                }
                else
                {
                    estates.Materials =
                             from x in estates.Materials
                             where x.MaterialDateCreated >= dateConvert 
                             && x.MaterialDateCreated <= dateConvertEnd
                             && x.MaterialAvailability == true
                             && x.FK_EstateID == estates.EstateID
                             select x;

                    estates.Medicines =
                              from x in estates.Medicines
                              where x.MedicineDateCreated >= dateConvert
                              && x.MedicineDateCreated <= dateConvertEnd
                              && x.MedicineAvailability == true
                              && x.FK_EstateID == estates.EstateID
                              select x;

                    estates.Vehicles =
                           from x in estates.Vehicles
                           where x.VehicleDateCreated >= dateConvert
                           && x.VehicleDateCreated <= dateConvertEnd
                           && x.VehicleAvailability == true
                           && x.FK_EstateID == estates.EstateID
                           select x;
                }

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Reporte",
                };

                objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = Resource.GetHTMLString(estates, materiales, medicamentos, vehiculos, dateConvert, dateConvertEnd),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesForResource.css") },
                    FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
                };

            }
            else
            {
                var estates = _repository.Estates.GetAllEstatesByPdf();

                //Sin stock
                var materiales = _repository.Materials.GetAllMaterials(dateConvert, dateConvertEnd);
                var medicamentos = _repository.Medicines.GetAllMedicines(dateConvert, dateConvertEnd);
                var vehiculos = _repository.Vehicles.GetAllVehicles(dateConvert, dateConvertEnd);


                foreach (var item in estates)
                {
                    if (String.IsNullOrEmpty(dateEnd))
                    {
                        item.Materials =
                                    from x in item.Materials
                                    where x.MaterialDateCreated >= dateConvert && x.MaterialAvailability != false
                                    select x;

                        item.Medicines =
                                  from x in item.Medicines
                                  where x.MedicineDateCreated >= dateConvert && x.MedicineAvailability != false
                                  select x;

                        item.Vehicles =
                               from x in item.Vehicles
                               where x.VehicleDateCreated >= dateConvert && x.VehicleAvailability != false
                               select x;
                    }
                    else
                    {
                        item.Materials =
                                 from x in item.Materials
                                 where x.MaterialDateCreated >= dateConvert && x.MaterialDateCreated <= dateConvertEnd && x.MaterialAvailability == true
                                 select x;

                        item.Medicines =
                                  from x in item.Medicines
                                  where x.MedicineDateCreated >= dateConvert && x.MedicineDateCreated<= dateConvertEnd && x.MedicineAvailability == true
                                  select x;

                        item.Vehicles =
                               from x in item.Vehicles
                               where x.VehicleDateCreated >= dateConvert && x.VehicleDateCreated <= dateConvertEnd && x.VehicleAvailability == true
                               select x;
                    }
                }


                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Reporte de recursos",
                };

                objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = Resources.GetHTMLString(estates, materiales, medicamentos, vehiculos, dateConvert, dateConvertEnd),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesForResource.css") },
                    FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
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


