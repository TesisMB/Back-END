using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Back_End.Controllers;
using Back_End.Models;
using System.Security.Claims;
using Back_End.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Back_End.Services;
using AutoMapper;

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


        [HttpPost]
        public ActionResult<UserAuthDto> Login([FromBody] Users user)
        {
            using (var db = new CruzRojaContext())
            {
                /*Al momento de ingresar la Contraseña se la encripta para verificar que la contraseña 
                  ingresada coincida con la contraseña hasheada en la base de datos*/
                string Pass = user.UserPassword;
                string ePass = Encrypt.GetSHA256(Pass);

                
                user = db.Users.Include(u => u.Persons)
                               .Include(u => u.Roles)
                               .Where(u => u.UserDni == user.UserDni
                                      && u.UserPassword == ePass).FirstOrDefault();

                if (user.UserAvailability)
                {
                    return Ok(_mapper.Map<UserAuthDto>(user));
                }
                    return Unauthorized();
            }
        }
    }
}



     