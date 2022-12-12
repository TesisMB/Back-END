using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.DataTransferObjects.Messages___Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        //********************************* FUNCIONANDO *********************************

        //TODO Cambia modelo 
        [HttpGet]
        public async Task<ActionResult<ChatRooms>> GetAllChatRooms([FromQuery] int userId, [FromQuery] bool? status)
        {
            try
            {
                var chatRooms = await _repository.ChatRooms.GetChatRooms(userId);
                _logger.LogInfo($"Returned all ChatRooms from database. ");

                var chatRoomsToResult = _mapper.Map<IEnumerable<ChatRoomsDto>>(chatRooms);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                List<int> messageFalse = new List<int>();

                int cant = 0;

                foreach (var item in chatRoomsToResult)
                {
                    item.EmergenciesDisasters.UsersChatRooms.Clear();

                    foreach (var item2 in item.UsersChatRooms.ToList())
                    {
                        var person = cruzRojaContext.Persons
                                       .Where(a => a.ID == userId)
                                       .AsNoTracking()
                                       .FirstOrDefault();

                        var user = cruzRojaContext.Users
                                       .Where(a => a.UserID == userId)
                                      .AsNoTracking()
                                      .FirstOrDefault();

                        item2.Name = person.FirstName + " " + person.LastName;
                        item2.UserDni = user.UserDni;



                        var roles = cruzRojaContext.Roles
                                      .Where(a => a.RoleID == user.FK_RoleID)
                                      .AsNoTracking()
                                      .FirstOrDefault();

                        item2.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{user.Avatar}";
                        item2.RoleName = roles.RoleName;

                        if (item2.Status == false)
                        {
                            item.UsersChatRooms.Remove(item2);
                        }
                    }
                }


                foreach (var item in chatRoomsToResult)
                {
                    foreach (var item2 in item.DateMessage)
                    {
                        foreach (var item3 in item2.Messages)
                        {
                            if(item3.MessageState == false)
                            {
                                cant += 1;

                                messageFalse.Add(cant);
                            }

                            var person = cruzRojaContext.Persons
                                          .Where(a => a.ID == userId)
                                          .AsNoTracking()
                                          .FirstOrDefault();

                            var user = cruzRojaContext.Users
                                          .Where(a => a.UserID == userId)
                                          .AsNoTracking()
                                          .FirstOrDefault();

                            item3.Name = person.FirstName + " " + person.LastName;
                            item3.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{user.Avatar}";
                        }
                    }
                    item.Quantity = messageFalse.Count();
                }


                return Ok(chatRoomsToResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChatRooms aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }


        //********************************* FUNCIONANDO *********************************
        [HttpGet("{chatRoomID}")]
        public async Task<ActionResult<ChatRooms>> GetChatRoom(int chatRoomID, [FromQuery] bool status)
        {
            try
            {
                var chatRooms = await _repository.Chat.GetChat(chatRoomID, status);
                _logger.LogInfo($"Returned all ChatRooms from database. ");

                if (chatRooms == null)
                {
                    return NotFound();
                }


                var chatRoomsToResult = _mapper.Map<ChatRoomsDto>(chatRooms);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                chatRoomsToResult.EmergenciesDisasters.UsersChatRooms.Clear();

                foreach (var item in chatRoomsToResult.UsersChatRooms.ToList())
                {

                    var user = cruzRojaContext.Users
                                             .Where(a => a.UserID == item.UserID)
                                             .AsNoTracking()
                                             .FirstOrDefault();

                    var person = cruzRojaContext.Persons
                                     .Where(a => a.ID == item.UserID)
                                     .Include(a => a.Users)
                                     .AsNoTracking()
                                     .FirstOrDefault();


                    item.Name = person.FirstName + " " + person.LastName;
                    item.UserDni = user.UserDni;

                 

                    var roles = cruzRojaContext.Roles
                                  .Where(a => a.RoleID == user.FK_RoleID)
                                  .AsNoTracking()
                                  .FirstOrDefault();

                    item.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{user.Avatar}";
                    item.RoleName = roles.RoleName;

                    if(status != item.Status)
                    {
                        chatRoomsToResult.UsersChatRooms.Remove(item);
                    }

                    if (item.RoleName == "Admin")
                    {
                        chatRoomsToResult.UsersChatRooms.Remove(item);
                    }
                }

                foreach (var item in chatRoomsToResult.DateMessage)
                {
                    foreach (var item3 in item.Messages)
                    {
                        var person = cruzRojaContext.Persons
                                      .Where(a => a.ID == item3.userID)
                                      .AsNoTracking()
                                      .FirstOrDefault();

                        var user = cruzRojaContext.Users
                                      .Where(a => a.UserID == item3.userID)
                                      .AsNoTracking()
                                      .FirstOrDefault();

                        var roles = cruzRojaContext.Roles
                                      .Where(a => a.RoleID == user.FK_RoleID)
                                      .AsNoTracking()
                                      .FirstOrDefault();

                        item3.Name = person.FirstName + " " + person.LastName;
                        item3.Avatar = $"https://almacenamientotesis.blob.core.windows.net/publicuploads/{user.Avatar}";
                        item3.RoleName = roles.RoleName;
                    }
                }

                return Ok(chatRoomsToResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllChatRooms aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }



        //********************************* FUNCIONANDO *********************************
        //Revisar con APP
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessagesForCreationDto message, [FromQuery] int userId)
        {
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            var person = cruzRojaContext.Persons
                        .Where(a => a.ID == userId)
                        .AsNoTracking()
                        .FirstOrDefault();


            message.DateMessage = new DateMessageForCreationDto();
            message.DateMessage.CreatedDate = message.DateMessage.Date.ToString("dd/MM/yyyy");

            message.DateMessage.FK_ChatRoomID = Convert.ToInt32(message.chatRoomID);

            var date = cruzRojaContext.DateMessage
                        .Where(a => a.CreatedDate.Equals(message.DateMessage.CreatedDate)
                              && a.FK_ChatRoomID == Convert.ToInt32(message.chatRoomID))
                        .AsNoTracking()
                       .FirstOrDefault();


            message.userID = userId;
            message.Name = person.FirstName + " " + person.LastName;


            try
            {
                if (message == null)
                {

                    _logger.LogError("Message object sent from client is null.");
                    return BadRequest("Message object is null");
                }



                var messages = _mapper.Map<Messages>(message);

               //messages.CreatedDate = DateTime.Now;
                    var fehca = DateTime.Now.ToString(@"hh");
                    var min = DateTime.Now.ToString(@"mm");
                    var seg = DateTime.Now.ToString(@"ss");
                    var day = DateTime.Now.ToString(@"dd/MM/yyyy");
                var total = Convert.ToInt32(fehca) - 3;
                string totalf;

                if (total <= 9)
                {
                    totalf = day + " " + "0" + Convert.ToString(total) + ":" + $"{min}:" + $"{seg}" ;
                }
                else
                {
                    totalf = day + " " + Convert.ToString(total) + ":" + $"{min}:" + $"{seg}";
                }

                var totalfecha = Convert.ToDateTime(totalf);

                messages.CreatedDate = totalfecha;
                if (date != null)
                {
                    messages.DateMessage = null;
                    messages.FK_DataMessageID = date.ID;
                    _repository.Messages.CreateMessage(messages);

                    _repository.Messages.SaveAsync();
                }
                else
                {
                    _repository.Messages.CreateMessage(messages);

                    _repository.Messages.SaveAsync();
                }


              var response = await SendController.SendNotificationByChat(userId, messages.Message, Convert.ToInt32(message.chatRoomID));

                return StatusCode(200);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SendMessage aciton: {ex.Message}");
                return StatusCode(500, "internal Server Error");
            }
        }


        //********************************* FUNCIONANDO *********************************

        //TODO Revisar con APP
        [HttpPost("JoinGroup")]
        public IActionResult JoinGroup([FromBody] UsersChatRoomsJoin_LeaveGroupDto usersChat, [FromQuery] int userId)
        {

            usersChat.FK_UserID = userId;

            try
            {
                if (usersChat == null)
                {
                    _logger.LogError("UsersChat object sent from client is null.");
                    return BadRequest("UsersChat object is null");
                }


                var userChatRoom = _mapper.Map<UsersChatRooms>(usersChat);

                //_repository.UsersChatRooms.JoinGroup(userChatRoom, usersChat.Coords.Latitude, usersChat.Coords.Longitude);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();

                var userChatRooms = cruzRojaContext.UsersChatRooms
                                    .OrderByDescending(a => a.ID)
                                    .FirstOrDefault();

                if(userChatRooms == null)
                {
                    userChatRoom.ID = 1;

                }
                else
                {
                userChatRoom.ID = userChatRooms.ID + 1;
                }


                _repository.UsersChatRooms.Create(userChatRoom);
                _repository.UsersChatRooms.SaveAsync();

                var response =  SendController.SendNotificationJoinGroup(userId, usersChat);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside JoinGroup aciton: {ex.Message}");
                return StatusCode(500, "interval Server Error");
            }
        }



        //TODO Revisar con APP
        [HttpPost("AcceptRejectRequest")]
        public async Task<ActionResult> AcceptRejectRequestChat([FromQuery] int UserID, [FromQuery] int chatRoomID, [FromQuery] bool status)
        {

            try
            {
                var usersChatRooms = await _repository.UsersChatRooms.GetUsersChatRooms(UserID, chatRoomID);

                if (usersChatRooms == null)
                {
                    _logger.LogError($"User with id: {UserID} and ChatRoom with id: {chatRoomID}, hasn't ben found in db.");
                    return NotFound();
                }


                if (status == false)
                {
                    _repository.UsersChatRooms.LeaveGroup(usersChatRooms);
                }
                else
                {
                    usersChatRooms.Status = true;
                    _repository.UsersChatRooms.Update(usersChatRooms);
                }


                _repository.UsersChatRooms.SaveAsync();

                var response = await SendController.SendNotificationAcceptRejectRequestChat(UserID, chatRoomID, status);

                CruzRojaContext cruzRojaContext = new CruzRojaContext();


                var userChatRooms = cruzRojaContext.UsersChatRooms.Where(a => a.FK_ChatRoomID.Equals(chatRoomID)).ToList();

                int count = userChatRooms.Count();

                foreach (var item in userChatRooms.ToList())
                {
                if (item.Status == true)
                {
                   userChatRooms.Remove(item);
                    count -= 1;
                }
                }


                return Ok(count);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside LeaveGroup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
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
