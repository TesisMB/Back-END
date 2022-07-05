using AutoMapper;
using Back_End.Entities;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Contracts.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DataTransferObjects.Estates___Dto;
using Entities.Helpers;
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


        [HttpGet("PDF")]
        public IActionResult CreatePDF([FromQuery] string dateStart, [FromQuery] string dateEnd, [FromQuery] int userId, [FromQuery] string getall)
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
                    .Where(x => x.UserID == userId)
                    .AsNoTracking()
                    .FirstOrDefault();

            if (getall == null)
            {
                var estates = _repository.Estates.GetAllEstateByPdf(userId);

                var materiales = _repository.Materials.GetAllMaterials(dateConvert, dateConvertEnd, userId);
                var medicamentos = _repository.Medicines.GetAllMedicines(dateConvert, dateConvertEnd, userId);
                var vehiculos = _repository.Vehicles.GetAllVehicles(dateConvert, dateConvertEnd, userId);


                List<bool> matList = new List<bool>();
                List<bool> medList = new List<bool>();
                List<bool> vehList = new List<bool>();
                bool med = false;
                bool mat = false;
                bool veh = false;



                if (String.IsNullOrEmpty(dateEnd))
                {
                    estates.Materials =
                                from x in estates.Materials
                                where x.MaterialDateCreated >= dateConvert && x.MaterialAvailability != false
                                && x.FK_EstateID == estates.EstateID
                                select x;

                    mat = estates.Materials.GetEnumerator().MoveNext();
                    matList.Add(mat);

                    estates.Medicines =
                              from x in estates.Medicines
                              where x.MedicineDateCreated >= dateConvert
                              && x.MedicineAvailability != false
                              && x.FK_EstateID == estates.EstateID
                              select x;

                    med = estates.Medicines.GetEnumerator().MoveNext();
                    medList.Add(med);


                    estates.Vehicles =
                           from x in estates.Vehicles
                           where x.VehicleDateCreated >= dateConvert 
                           && x.VehicleAvailability != false
                           && x.FK_EstateID == estates.EstateID
                           select x;

                    veh = estates.Vehicles.GetEnumerator().MoveNext();
                    vehList.Add(veh);
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

                    mat = estates.Materials.GetEnumerator().MoveNext();
                    matList.Add(mat);

                    estates.Medicines =
                              from x in estates.Medicines
                              where x.MedicineDateCreated >= dateConvert
                              && x.MedicineDateCreated <= dateConvertEnd
                              && x.MedicineAvailability == true
                              && x.FK_EstateID == estates.EstateID
                              select x;


                    med = estates.Medicines.GetEnumerator().MoveNext();
                    medList.Add(med);

                    estates.Vehicles =
                           from x in estates.Vehicles
                           where x.VehicleDateCreated >= dateConvert
                           && x.VehicleDateCreated <= dateConvertEnd
                           && x.VehicleAvailability == true
                           && x.FK_EstateID == estates.EstateID
                           select x;

                    veh = estates.Vehicles.GetEnumerator().MoveNext();
                    vehList.Add(veh);
                }


                if (matList.Contains(false) && medList.Contains(false) && vehList.Contains(false) && estates.Materials.Count() == 0 && estates.Medicines.Count() == 0
                           &&  estates.Vehicles.Count() == 0)
                {
                    return BadRequest(ErrorHelper.Response(400, "No hay ningun recurso en la fecha establecida."));
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


                List<bool> matList2 = new List<bool>();
                List<bool> medList2 = new List<bool>();
                List<bool> vehList2 = new List<bool>();
                bool med2 = false;
                bool mat2 = false;
                bool veh2 = false;


                foreach (var item in estates)
                {
                    if (String.IsNullOrEmpty(dateEnd))
                    {
                        item.Materials =
                                    from x in item.Materials
                                    where x.MaterialDateCreated >= dateConvert && x.MaterialAvailability != false
                                    select x;

                        mat2 = item.Materials.GetEnumerator().MoveNext();
                        matList2.Add(mat2);

                        item.Medicines =
                                  from x in item.Medicines
                                  where x.MedicineDateCreated >= dateConvert && x.MedicineAvailability != false
                                  select x;

                        med2 = item.Medicines.GetEnumerator().MoveNext();
                        medList2.Add(med2);


                        item.Vehicles =
                               from x in item.Vehicles
                               where x.VehicleDateCreated >= dateConvert && x.VehicleAvailability != false
                               select x;

                        veh2 = item.Vehicles.GetEnumerator().MoveNext();
                        vehList2.Add(veh2);
                    }
                    else
                    {
                        item.Materials =
                                 from x in item.Materials
                                 where x.MaterialDateCreated >= dateConvert && x.MaterialDateCreated <= dateConvertEnd && x.MaterialAvailability == true
                                 select x;

                        mat2 = item.Materials.GetEnumerator().MoveNext();
                        matList2.Add(mat2);

                        item.Medicines =
                                  from x in item.Medicines
                                  where x.MedicineDateCreated >= dateConvert && x.MedicineDateCreated<= dateConvertEnd && x.MedicineAvailability == true
                                  select x;

                        med2 = item.Medicines.GetEnumerator().MoveNext();
                        medList2.Add(med2);

                        item.Vehicles =
                               from x in item.Vehicles
                               where x.VehicleDateCreated >= dateConvert && x.VehicleDateCreated <= dateConvertEnd && x.VehicleAvailability == true
                               select x;

                        veh2 = item.Vehicles.GetEnumerator().MoveNext();
                        vehList2.Add(veh2);
                    }
                }




                        if (matList2.Contains(false)  && medList2.Contains(false) && vehList2.Contains(false) && materiales.Count() == 0 && medicamentos.Count() == 0
                        && vehiculos.Count() == 0)
                        {
                            return BadRequest(ErrorHelper.Response(400, "No hay ningun recurso en la fecha establecida."));
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


