using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ChatRoomsController(ILoggerManager logger, IMapper mapper, IRepositorWrapper repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
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
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChatRooms aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }
    }
}
