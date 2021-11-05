using AutoMapper;
using Back_End.Hubs;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private IMapper _mapper;
        private ILoggerManager _logger;
        private IRepositorWrapper _repository;
        private IHubContext<Mensaje> _hubContext;

        public NotificationsController(IMapper mapper, ILoggerManager logger, IRepositorWrapper repository, IHubContext<Mensaje> hubContext)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _hubContext = hubContext;
        }
    
        [HttpPost]
        public IActionResult SendNotifications([FromBody] Notifications notifications)
        {
            string not = Newtonsoft.Json.JsonConvert.SerializeObject(notifications);

            _hubContext.Clients.All.SendAsync("notificar", not);

            return Ok(new { resp = "Enivado de forma satisfactoria" });
       }
    }
}
