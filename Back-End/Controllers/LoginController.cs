using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Login___Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;

namespace Back_End.Controllers
{
    
    [Route("api/Login")]
    [ApiController]
    public class LoginController: BaseApiController
    {
        private ILoggerManager _logger;
        private readonly IMapper _mapper;

        public LoginController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        ActionResult ret = null;
        UserEmployeeAuthDto auth = new UserEmployeeAuthDto();


        [HttpPost]
        public ActionResult<UserEmployeeAuthDto> Login([FromBody] UserLoginDto user)
        {
            try
            {
                auth = ValidateUser(user);
                if (auth.UserAvailability)
                {
                    ret = StatusCode(200, auth);
                    _logger.LogInfo($"Returned User.");

                }
                else
                {
                    ret = Unauthorized();
                }

            }catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside User: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return ret;
        }

        public UserEmployeeAuthDto ValidateUser(UserLoginDto user)
        {
            UserEmployeeAuthDto ret = new UserEmployeeAuthDto();

            Users authUser = null;

            string Pass = user.UserPassword;
            string ePass = Encrypt.GetSHA256(Pass);

            //se conecta a la base de datos para verificar las datos del usuario en cuestion
                using (var db = new CruzRojaContext())
                    authUser = db.Users.Include(u => u.Persons)
                                  .Include(u => u.Roles)
                                  .Include(u => u.Estates)
                                  .ThenInclude(u => u.LocationAddress)
                                  .Include(u => u.Estates.EstatesTimes)
                                  .ThenInclude(u => u.Times)
                                  .ThenInclude(u => u.Schedules)
                                   .Where(u => u.UserDni == user.UserDni
                                      && u.UserPassword == ePass).FirstOrDefault();

            if (authUser != null)
            {
                    ret = _mapper.Map<UserEmployeeAuthDto>(authUser); //si los datos son correctos se crea el objeto del usuario autentificado
            }

            return ret; //retornamos el valor de este objeto
        }
    }
}