using AutoMapper;
using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiController
    {
        private readonly IMapper _mapper;

        public LoginController(IMapper mapper)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        ActionResult ret = null;
        UserAuthDto auth = new UserAuthDto();
        //UserSecurity mgr = new UserSecurity(_settings);


        [HttpPost]
        public ActionResult<UserAuthDto> Login([FromBody] UserLoginDto user)
        {
            auth = ValidateUser(user);
            if (auth.UserAvailability)
            {
                ret = StatusCode(200, auth);
            }
            else
            {
                ret = Unauthorized();
            }

            return ret;
        }

        public UserAuthDto ValidateUser(UserLoginDto user)
        {
            UserAuthDto ret = new UserAuthDto();
            Users authUser = null;

            //se conecta a la base de datos para verificar las datos del usuario en cuestion
            using (var db = new CruzRojaContext())
            {
                /*Al momento de ingresar la Contraseña se la encripta para verificar que la contraseña 
                 ingresada coincida con la contraseña hasheada en la base de datos*/
                string Pass = user.UserPassword;
                string ePass = Encrypt.GetSHA256(Pass);


                authUser = db.Users.Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Where(u => u.UserDni == user.UserDni
                                      && u.UserPassword == ePass).FirstOrDefault();
            }

            if (authUser != null)
            {
                ret = _mapper.Map<UserAuthDto>(authUser); //si los datos son correctos se crea el objeto del usuario autentificado
            }

            return ret; //retornamos el valor de este objeto
        }
    }
}