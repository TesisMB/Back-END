using Back_End.Dto;
using Back_End.Helpers;
using Back_End.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login([FromBody]UserLoginDto user)
        {
            IActionResult ret = null;
            UserAuthDto auth = new UserAuthDto();
            UserSecurity mgr = new UserSecurity(_settings);

            auth = mgr.ValidateUser(user);
            if (auth.IsAuthenticated)
            {
                ret = StatusCode(200, auth); //devuelve el metodo Ok con el Usuario
            }
            else
            {
                ret = StatusCode(404, "Nombre del Usuario o Contraseña Invalido."); // devuelve Error
            }

            return ret;
        }

    }
}