using AutoMapper;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : BaseApiController
    {
        private ILoggerManager _logger;
        private readonly IMapper _mapper;
        private IRepositorWrapper _repository;

        public LoginController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        ActionResult ret = null;

        //********************************* FUNCIONANDO *********************************

        [HttpPost("app/login")]
        public async Task<ActionResult<Users>> LoginApp([FromBody] UserLoginDto user)
        {

            try
            {
                var auth = await _repository.Users.ValidateUser(user);
                if (auth.UserAvailability)
                {

                    auth.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{auth.Avatar}";


                    ret = StatusCode(200, auth);
                    _logger.LogInfo($"Returned User.");
                }
                else
                {
                    ret = Unauthorized();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside User: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return ret;
        }


        //********************************* FUNCIONANDO *********************************
        [HttpPost("login")]
        public async Task<ActionResult<UserEmployeeAuthDto>> LoginWeb(UserLoginDto user)
        {
            UserEmployeeAuthDto auth;
            try
            {
                 auth = await _repository.Users.ValidateUser(user);
                if (auth.UserAvailability && auth.RoleName != "Voluntario")
                {
                    if(auth.UserDni == "44539766")
                    {
                        var u = await _repository.Users.GetUsers(auth.UserID);
                         _repository.Users.sendLoginNotification(u);

                    }

                    auth.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{auth.Avatar}";

                    ret = StatusCode(200, auth);
                    _logger.LogInfo($"Returned User.");
                }
                else if (!auth.UserAvailability && auth.RoleName != "Voluntario")
                {
                    ret = Unauthorized();
                }
                else
                {

                    return BadRequest(ErrorHelper.Response(400, "No puede acceder al sistema porque es voluntario"));
                }

            } catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside User: {ex.Message}");
                return StatusCode(500, "Internal error, directo al catch error");
            }
            return ret;
        }

     
        
        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword([FromBody] Persons email)
        {
            _repository.Users.ForgotPassword(email.Email);

            return Ok();
        }

        [HttpPost("reset-password/{token?}")]
        public ActionResult ResetPassword([FromQuery] string token, [FromBody] Users pass)
        {
            _repository.Users.ResetPassword(token, pass.UserPassword);
            return Ok();
        }

    }
}