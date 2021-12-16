using AutoMapper;
using Back_End.Entities;
using Back_End.Hubs;
using Contracts.Interfaces;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositorWrapper _repository;
        private IHubContext<Mensaje> _hubContext;


        public ChatRoomsController(ILoggerManager logger, IMapper mapper, IRepositorWrapper repository, IHubContext<Mensaje> hubContext)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<TypesChatRooms>> GetAllChatRooms()
        {
            try
            {
                var chatRooms = await _repository.ChatRooms.GetChatRooms();
                _logger.LogInfo($"Returned all ChatRooms from database. ");

                var chatRoomsToResult = _mapper.Map<IEnumerable<TypesChatsDto>>(chatRooms);

                return Ok(chatRoomsToResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChatRooms aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }

        [HttpGet("{chatRoomID}")]
        public async Task<ActionResult<ChatRooms>> GetChatRoom(int chatRoomID)
        {
            try
            {
                var chatRooms = await _repository.Chat.GetChat(chatRoomID);
                _logger.LogInfo($"Returned all ChatRooms from database. ");

                if(chatRooms == null)
                {
                    return NotFound();
                }

                var chatRoomsToResult = _mapper.Map<ChatRoomsDto>(chatRooms);

                return Ok(chatRoomsToResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChatRooms aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }

        [HttpPost]
        public  IActionResult SendMessage([FromBody] MessagesForCreationDto message)
        {

            string not = Newtonsoft.Json.JsonConvert.SerializeObject(message);

            _hubContext.Clients.All.SendAsync("notificar", not);

            return Ok(new { resp = "Enivado de forma satisfactoria" });
        }
    }
}
