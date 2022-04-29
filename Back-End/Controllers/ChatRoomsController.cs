using AutoMapper;
using Contracts.Interfaces;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositorWrapper _repository;

        public ChatRoomsController(ILoggerManager logger, IMapper mapper, IRepositorWrapper repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ChatRooms>> GetAllChatRooms()
        {
            try
            {
                var chatRooms = await _repository.ChatRooms.GetChatRooms();
                _logger.LogInfo($"Returned all ChatRooms from database. ");




                var chatRoomsToResult = _mapper.Map<IEnumerable<ChatRoomsDto>>(chatRooms);

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

                if (chatRooms == null)
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
        public IActionResult SendMessage([FromBody] MessagesForCreationDto message)
        {
            try
            {
                if (message == null)
                {

                    _logger.LogError("Message object sent from client is null.");
                    return BadRequest("Message object is null");
                }

                var messages = _mapper.Map<Messages>(message);

                _repository.Messages.CreateMessage(messages);

                _repository.Messages.SaveAsync();

                return StatusCode(200, message);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SendMessage aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }


        [HttpPost("JoinGroup")]
        public IActionResult JoinGroup([FromBody] UsersChatRoomsJoin_LeaveGroupDto usersChat)
        {
            try
            {
                if (usersChat == null)
                {
                    _logger.LogError("UsersChat object sent from client is null.");
                    return BadRequest("UsersChat object is null");
                }


                var userChatRoom = _mapper.Map<UsersChatRooms>(usersChat);

                _repository.UsersChatRooms.JoinGroup(userChatRoom, usersChat.Coords.Latitude, usersChat.Coords.Longitude);

                _repository.Messages.SaveAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside JoinGroup aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }


        [HttpDelete("LeaveGroup/{UserID}/{chatRoomID}")]
        public async Task<ActionResult> LeaveGroup(int UserID, int chatRoomID)
        {
            try
            {
                var usersChatRooms = await _repository.UsersChatRooms.GetUsersChatRooms(UserID, chatRoomID);

                if (usersChatRooms == null)
                {
                    _logger.LogError($"User with id: {UserID} and ChatRoom with id: {chatRoomID}, hasn't ben found in db.");
                    return NotFound();
                }

                _repository.UsersChatRooms.LeaveGroup(usersChatRooms);
                _repository.UsersChatRooms.SaveAsync();

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside LeaveGroup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
