using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Back_End.Models.Employees___Dto;
using Back_End.Models.Volunteers__Dto;
using Back_End.VolunteersPDF;
using Contracts.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.DataTransferObjects.Volunteers__Dto;
using Entities.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositorWrapper _repository;
        private readonly IMapper _mapper;
        public byte[] pdf;
        private IConverter _converter;
        public VolunteersController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _converter = converter;

        }

        //********************************* FUNCIONANDO *********************************

        [HttpGet("api/Voluntarios/PDF/{volunteerId}")]
        public async Task<IActionResult> CreatePDF(int volunteerId)
        {

            var volunteer = await _repository.Volunteers.GetVolunteerWithDetails(volunteerId);

            //quien es el actual usuario
            Users user = new Users();
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            user = cruzRojaContext.Users
                    .Where(x => x.UserID == volunteerId)
                    .Include(a => a.Estates)
                    .AsNoTracking()
                    .FirstOrDefault();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"Reporte de {volunteer.Users.Persons.FirstName} {volunteer.Users.Persons.LastName}",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = VolunteerPdf.GetHTMLString(volunteer),
                // Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
                //FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf");
        }

        [HttpGet("api/Voluntarios/GetAll/PDF/{volunteerId}")]
        public async Task<IActionResult> CreatePDFVolunteers(int volunteerId)
        {

            var volunteer = await _repository.Volunteers.GetAllVolunteers(volunteerId);

            string title = string.Empty;
            foreach (var emp in volunteer)
            {
                title = emp.Users.Estates.Locations.LocationCityName;
            }

            //quien es el actual usuario
            Users user = new Users();
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            user = cruzRojaContext.Users
                    .Where(x => x.UserID == volunteerId)
                    .Include(a => a.Estates)
                    .AsNoTracking()
                    .FirstOrDefault();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"Reporte de Voluntarios - {title}",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = VolunteersPdf.GetHTMLString(volunteer),
                // Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "stylesForVolunteers.css") },
                FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
                //FooterSettings = { FontName = "Times New Roman", FontSize = 8, Right = $@"USUARIO: {user.UserDni}          IMPRESIÓN: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}          [page]", Line = true, },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf");
        }

        [Route("api/Voluntarios")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetAllVolunteers([FromQuery] int userId)
        {
            try
            {
                var volunteers = await _repository.Volunteers.GetAllVolunteers(userId);

                _logger.LogInfo($"Returned all Volunteers from database.");

                var volunteersResult = _mapper.Map<IEnumerable<Resources_Dto>>(volunteers);



                foreach (var item in volunteersResult)
                {
                    var user = EmployeesRepository.GetAllEmployeesById(item.Volunteers.ID);
                    
                    item.Name = user.Persons.FirstName + " " + user.Persons.LastName;
                    item.Volunteers.Address = user.Persons.Address;
                    item.Volunteers.Phone = user.Persons.Phone;
                    item.Volunteers.Birthdate = user.Persons.Birthdate;
                    item.Availability = user.Persons.Status;

                    if (item.Picture != "https://i.imgur.com/8AACVdK.png")
                    {
                        item.Picture = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{item.Picture}";

                    }
                }


                return Ok(volunteersResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllVolunteers action:  {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }



        //********************************* FUNCIONANDO *********************************

        [Route("api/Voluntarios/{volunteerId}")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetVolunteer(int volunteerId)
        {
            try
            {
                var volunteer = await _repository.Volunteers.GetVolunteerWithDetails(volunteerId);

                if (volunteer == null)

                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned volunteer with id: {volunteerId}");
                    var volunteerResult = _mapper.Map<Resources_Dto>(volunteer);

                    var user = EmployeesRepository.GetAllEmployeesById(volunteerId);

                    volunteerResult.Name = user.Persons.FirstName + " " + user.Persons.LastName;
                    volunteerResult.Volunteers.Address = user.Persons.Address;
                    volunteerResult.Volunteers.Phone = user.Persons.Phone;
                    volunteerResult.Volunteers.Birthdate = user.Persons.Birthdate;
                    volunteerResult.Availability = user.Persons.Status;



                    if (volunteerResult.Picture != "https://i.imgur.com/8AACVdK.png")
                    {
                        volunteerResult.Picture = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{volunteerResult.Picture}";

                    }


                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }


        //********************************* FUNCIONANDO *********************************
        [Route("api/app/Volunteers")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetAllVolunteersApp()
        {
            try
            {
                var volunteers1 = await _repository.Volunteers.GetAllVolunteersApp();

                _logger.LogInfo($"Returned all Volunteers from database.");

                var volunteersResult = _mapper.Map<IEnumerable<VolunteersAppDto>>(volunteers1);


                foreach (var item in volunteersResult)
                {
                    if (item.VolunteerAvatar != "https://i.imgur.com/8AACVdK.png")
                    {
                        item.VolunteerAvatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{item.VolunteerAvatar}";

                    }
                }




                return Ok(volunteersResult);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVolunteersApp action:  {ex.Message}");
                return StatusCode(500, "Internal Server error");
            }
        }

       
        //********************************* FUNCIONANDO *********************************
        [Route("api/app/Volunteers/{volunteerId}")]
        [HttpGet]
        public async Task<ActionResult<Volunteers>> GetAllVolunteerApp(int volunteerId)
        {
            try
            {
                var volunteer = await _repository.Volunteers.GetVolunteerAppWithDetails(volunteerId);

                if (volunteer == null)

                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't been found in db.");
                    return NotFound();


                }
                else

                {
                    _logger.LogInfo($"Returned volunteer with id: {volunteerId}");
                    var volunteerResult = _mapper.Map<VolunteersAppDto>(volunteer);



                    if (volunteerResult.VolunteerAvatar != "https://i.imgur.com/8AACVdK.png")
                    {
                        volunteerResult.VolunteerAvatar = String.Format("{0}://{1}{2}/StaticFiles/Images/Resources/{3}",
                                        Request.Scheme, Request.Host, Request.PathBase, volunteerResult.VolunteerAvatar);
                    }


                    return Ok(volunteerResult);

                }

            }
            catch (Exception ex)

            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

       

        //TODO - Revisar como funciona el patch de voluntarios en el Front
        [Route("api/Voluntarios/{volunteerId}")]
        [HttpPatch]
        public async Task<ActionResult> UpdatePartialUser(int volunteerId, JsonPatchDocument<VolunteersForUpdatoDto> patchDocument)
        {

            var userFromRepo = await _repository.Volunteers.GetVolunteersById(volunteerId);

            if (userFromRepo == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<VolunteersForUpdatoDto>(userFromRepo);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            Users authUser = new Users();
            if (!string.IsNullOrEmpty(userToPatch.Users.UserNewPassword))
            {
                // AGREGARLOS EN EL REPOSITORIO
                var userPass = userToPatch.Users.UserPassword;
                userToPatch.Users.UserPassword = Encrypt.GetSHA256(userPass);

                using (var db = new CruzRojaContext())
                    authUser = db.Users.Where(u => u.UserID == userFromRepo.Users.UserID
                          && u.UserPassword == userToPatch.Users.UserPassword)
                        .AsNoTracking()
                        .FirstOrDefault();


                if (authUser == null)
                {
                    return BadRequest(ErrorHelper.Response(400, "La contraseña es erronea."));
                }

                else
                {
                    userToPatch.Users.UserNewPassword = userToPatch.Users.UserNewPassword.Trim();

                    var userNewPass = userToPatch.Users.UserNewPassword;
                    userToPatch.Users.UserNewPassword = Encrypt.GetSHA256(userNewPass);

                    userToPatch.Users.UserPassword = userToPatch.Users.UserNewPassword;
                }
            }


            _mapper.Map(userToPatch, userFromRepo);

            _repository.Volunteers.Update(userFromRepo);

            //_repository.Save();

            return NoContent();
        }


        //TODO - Revisar como funciona el delete de voluntarios en el Front

        [Route("api/Voluntarios/{volunteerId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteVolunteer(int volunteerId)
        {
            try
            {
                var volunteer = await _repository.Users.GetUserVolunteerById(volunteerId);


                if (volunteer == null)
                {
                    _logger.LogError($"Volunteer with id: {volunteerId}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.Users.Delete(volunteer);

                _repository.Volunteers.SaveAsync();

                // Se retorna con exito la eliminacion del Usuario especificado
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteVolunteer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
