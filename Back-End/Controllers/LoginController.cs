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

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiController
    {
        private JwtSettings _settings;
        public LoginController(JwtSettings settings)
        {
            _settings = settings;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Users user)
        {
            IActionResult ret = null;
            UserAuthDto auth = new UserAuthDto();
            UserSecurity mgr = new UserSecurity(_settings);

            //Llamo al metodo ValidateUser para verificar los datos del Usuario logueado
            auth = mgr.ValidateUser(user);

            //Compruebo si el usuario esta Authenticado
            if (auth.UserAvailable)
            {
                ret = StatusCode(200, auth); //En caso de que se encuentre el Usuario en la base de datos se devolvera un 200 con sus respectivos datos
            }
            else    //En caso de que no se encuentre los datos del Usuario se devolvera un 401
            {
               
                ret = StatusCode(401); 
            
            }
            return ret; //Retorno el mensaje correspondiente
        }
    }
}