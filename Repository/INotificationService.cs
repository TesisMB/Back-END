using Back_End.Entities;
using Back_End.Models;
using CorePush.Google;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.CharRooms___Dto;
using Entities.Models;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Entities.DataTransferObjects.GoogleNotification;

namespace Repository
{
    public interface INotificationService
    {
        Task<string> SendNotification(string body);
    }

    public class NotificationService //: INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public static IList<Users> users;
        public static Message message = new Message();

        private static CruzRojaContext _cruzRojaContext = new CruzRojaContext();

        public NotificationService(IOptions<FcmNotificationSetting> settings, CruzRojaContext cruzRojaContext)
        {
            _fcmNotificationSetting = settings.Value;
        }

        public static async Task<Message> SendNotification(int userId, string body, int emergnecy)
        {
            string response = string.Empty;

            var user = EmployeesRepository.GetAllEmployeesById(userId);



            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }


            var registrationToken = "dVPHPzBoRgGvMOgInBXbqL:APA91bGV8Au2acf1Kk_KbLM-mqQVaQ5nPD7cmNB79Fl5cqsEpPkjPPjKMcSxqN4crpNy70-4KYpLJuozR8EmIOI5brifTiVSX-FT_cHTjhoDNcQzbdXsWMWACApjZ80MEWzmxcAZt0tk";

            users = _cruzRojaContext.Users
                            .Where(a => a.FK_EstateID == user.FK_EstateID
                                    && a.DeviceToken != null)
                           .Include(a => a.Persons)
                           .Include(a => a.Roles)
                           .Include(a => a.Estates)
                           .ThenInclude(a => a.Locations)
                           .Include(a => a.Volunteers)
                           .AsNoTracking()
                           .ToList();


            if (users != null)
            {
                foreach (var item in users)
                {

                    message = new Message()
                    {
                        Data = new Dictionary<string, string>()
                            {
                                { "alertId", Convert.ToString(emergnecy) },
                            },

                        Token = item.DeviceToken,
                        //Topic = "all",

                        Notification = new Notification()
                        {
                            Title = "Nueva emergencia",
                            Body = body

                        }
                    };

                    response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                }
                    FirebaseApp.DefaultInstance.Delete();
            }
            else
            {
                message = new Message()
                {
                    Data = new Dictionary<string, string>()
                            {
                                { "alertId", Convert.ToString(emergnecy) },
                            },

                    Token = registrationToken,
                    //Topic = "all",

                    Notification = new Notification()
                    {
                        Title = "Nueva emergencia",
                        Body = body

                    }
                };

                response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                FirebaseApp.DefaultInstance.Delete();

            }

            if (message.Data != null)
            {

                foreach (var kvp in message.Data)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }


            return message;
                    
        }

        public static Message SendNotificationJoinGroup(int userId, UsersChatRoomsJoin_LeaveGroupDto userChatRoom)
        {
            string response = string.Empty;

            var user = EmployeesRepository.GetAllEmployeesById(userId);



            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }


            var registrationToken = "dVPHPzBoRgGvMOgInBXbqL:APA91bGV8Au2acf1Kk_KbLM-mqQVaQ5nPD7cmNB79Fl5cqsEpPkjPPjKMcSxqN4crpNy70-4KYpLJuozR8EmIOI5brifTiVSX-FT_cHTjhoDNcQzbdXsWMWACApjZ80MEWzmxcAZt0tk";

            users = _cruzRojaContext.Users
                            .Where(a => a.FK_EstateID.Equals(user.FK_EstateID)
                                    && a.FK_RoleID.Equals(2)
                                    && a.DeviceToken != null)
                           .Include(a => a.Persons)
                           .Include(a => a.Roles)
                           .Include(a => a.Estates)
                           .ThenInclude(a => a.Locations)
                           .Include(a => a.Volunteers)
                           .AsNoTracking()
                           .ToList();


            if (users != null)
            {
                foreach (var item in users)
                {

                    message = new Message()
                    {
                        Data = new Dictionary<string, string>()
                            {
                                { "JoinGroup", Convert.ToString(userChatRoom.FK_ChatRoomID) },
                            },

                        Token = item.DeviceToken,
                        //Topic = "all",

                        Notification = new Notification()
                        {
                            Title = "Nueva solicitud",
                            Body = $"El voluntario {item.Persons.LastName}, {item.Persons.FirstName} quiere participar de la alerta #{userChatRoom.FK_ChatRoomID}"

                        }
                    };

                    response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                }
                FirebaseApp.DefaultInstance.Delete();
            }
            else
            {
                message = new Message()
                {
                    Data = new Dictionary<string, string>()
                            {
                                { "JoinGroup", Convert.ToString(userChatRoom.FK_ChatRoomID) },
                            },

                    Token = registrationToken,
                    //Topic = "all",

                    Notification = new Notification()
                    {
                        Title = "Nueva solicitud",
                        Body = $"NADA"

                    }
                };

                response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                FirebaseApp.DefaultInstance.Delete();

            }



            if (message.Data != null)
            {

                foreach (var kvp in message.Data)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }

            return message;

        }

        public static async Task<Message> SendNotificationChat(int userId, string messages, int chatRoomID)
        {
            string response = string.Empty;

            var user = EmployeesRepository.GetAllEmployeesById(userId);



            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }


            var registrationToken = "dVPHPzBoRgGvMOgInBXbqL:APA91bGV8Au2acf1Kk_KbLM-mqQVaQ5nPD7cmNB79Fl5cqsEpPkjPPjKMcSxqN4crpNy70-4KYpLJuozR8EmIOI5brifTiVSX-FT_cHTjhoDNcQzbdXsWMWACApjZ80MEWzmxcAZt0tk";

            users = _cruzRojaContext.Users
                            .Where(a => a.DeviceToken != null &&
                                   a.UsersChatRooms.Any(a => a.FK_ChatRoomID == chatRoomID)
                                   && a.UserID != userId)
                           .Include(a => a.Persons)
                           .Include(a => a.Roles)
                           .Include(a => a.Estates)
                           .ThenInclude(a => a.Locations)
                           .Include(a => a.Volunteers)
                           .AsNoTracking()
                           .ToList();


            var chat = _cruzRojaContext.ChatRooms
                    .Where(a => a.ID == chatRoomID)
                    .Include(a => a.EmergenciesDisasters)
                    .ThenInclude(a => a.LocationsEmergenciesDisasters)
                    .FirstOrDefault();

            if (users != null)
            {
                foreach (var item in users)
                {

                    message = new Message()
                    {
                        Data = new Dictionary<string, string>()
                            {
                                { "chatId", Convert.ToString(chatRoomID) },
                            },

                        Token = item.DeviceToken,
                        //Topic = "all",

                        Notification = new Notification()
                        {
                            Title = chat.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName,
                            Body = messages

                        }
                    };

                    response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                }
                    FirebaseApp.DefaultInstance.Delete();
            }
            else
            {
                message = new Message()
                {
                    Data = new Dictionary<string, string>()
                            {
                                { "chatId", Convert.ToString(chatRoomID) },
                            },

                    Token = registrationToken,
                    //Topic = "all",

                    Notification = new Notification()
                    {
                        Title = chat.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName,
                        Body = messages

                    }
                };

                response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                FirebaseApp.DefaultInstance.Delete();

            }


            if (message.Data != null)
            {

                foreach (var kvp in message.Data)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }

            }

            return message;

        }


        public static async Task<Message> SendNotificationAcceptRejectRequestChat(int userId, int chatRoomId, bool status)
        {
            string response = string.Empty;

            var user = EmployeesRepository.GetAllEmployeesById(userId);



            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }


            var registrationToken = "dVPHPzBoRgGvMOgInBXbqL:APA91bGV8Au2acf1Kk_KbLM-mqQVaQ5nPD7cmNB79Fl5cqsEpPkjPPjKMcSxqN4crpNy70-4KYpLJuozR8EmIOI5brifTiVSX-FT_cHTjhoDNcQzbdXsWMWACApjZ80MEWzmxcAZt0tk";

            users = _cruzRojaContext.Users
                            .Where(a => a.UserID.Equals(userId)
                                    && a.DeviceToken != null)
                           .Include(a => a.Persons)
                           .Include(a => a.Roles)
                           .Include(a => a.Estates)
                           .ThenInclude(a => a.Locations)
                           .Include(a => a.Volunteers)
                           .AsNoTracking()
                           .ToList();

            var estado = status == true ? "aprobada" : "rechazada";


            if (users != null)
            {
                foreach (var item in users)
                {

                    message = new Message()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            { "chatRoomId", Convert.ToString(chatRoomId) },
                        },

                        Token = item.DeviceToken,
                        //Topic = "all",

                        Notification = new Notification()
                        {
                            Title = "Su solicitud fue resuelta",
                            Body =  $"La solicitud para la alerta #{chatRoomId} fue {estado}"
                }
            };

                    response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                }
                FirebaseApp.DefaultInstance.Delete();
            }
            else
            {
                message = new Message()
                {
                    Data = new Dictionary<string, string>()
                            {
                                { "chatRoomId", Convert.ToString(chatRoomId) },
                            },

                    Token = registrationToken,
                    //Topic = "all",

                    Notification = new Notification()
                    {
                        Title = "Su solicitud fue resuelta",
                        Body = $"La solicitud para la alerta {chatRoomId} fue {estado}"

                    }
                };

                response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
                FirebaseApp.DefaultInstance.Delete();

            }



            if (message.Data != null)
            {

                foreach (var kvp in message.Data)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }

            }

            return message;

        }

    }
}