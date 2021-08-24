using AutoMapper;
using Back_End.Models;
using Contracts.Interfaces;
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

        public LoginController(ILoggerManager logger, IRepositorWrapper repository, IMapper mapper, IRepositorWrapper repositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repositorio;
        }

        ActionResult ret = null;
        UserEmployeeAuthDto auth = new UserEmployeeAuthDto();


        [HttpPost("login")]
        public async Task<ActionResult<UserEmployeeAuthDto>> Login([FromBody] UserLoginDto user)
        {
            try
            {
                auth = await  _repository.Users.ValidateUser(user);
                if (auth.UserAvailability)
                {
                    ret = StatusCode(200, auth);
                    _logger.LogInfo($"Returned User.");
                }

                else
                {
                    ret = Unauthorized();
                }

            } catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside User: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return ret;
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(string email )
        {
            _repository.Users.ForgotPassword(email);

            return Ok();
        }

        [HttpPost("reset-password/{token?}")]
        public ActionResult ResetPassword([FromQuery] string token, [FromBody] string password)
        {
            _repository.Users.ResetPassword(token, password);
            return Ok();
        }

    }
}