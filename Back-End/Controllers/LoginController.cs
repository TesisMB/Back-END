using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Back_End.Controllers;
using Back_End.Models;

namespace JWT_Server.Controllers
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
        public IActionResult Login([FromBody]Users user)
        {
            IActionResult ret = null;
            UserAuthDto auth = new UserAuthDto();
            UserSecurityDto mgr = new UserSecurityDto(_settings);

            auth = mgr.ValidateUser(user);
            if (auth.IsAuthenticated)
            {
                ret = StatusCode(200, auth); //devuelve el metodo Ok con el Usuario
            }
            else 
            {
               
                ret = StatusCode(404, "DNI o Contraseña incorrectas."); // devuelve Error
            
            }

            return ret;
        }

    }
}