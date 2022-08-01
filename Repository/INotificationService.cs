using Back_End.Entities;
using Back_End.Models;
using CorePush.Google;
using Entities.DataTransferObjects;
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

            //}
            //else
            //{
            //    users = _cruzRojaContext.Users
            //              .Where(a => a.FK_EstateID == user.FK_EstateID
            //                      && a.DeviceToken != null
            //                      && a.UserID == userId)
            //             .Include(a => a.Persons)
            //             .Include(a => a.Roles)
            //             .Include(a => a.Estates)
            //             .ThenInclude(a => a.Locations)
            //             .Include(a => a.Volunteers)
            //             .AsNoTracking()
            //             .ToList();


            //    if (users != null)
            //    {
            //        foreach (var item in users)
            //        {

            //            message = new Message()
            //            {
            //                Data = new Dictionary<string, string>()
            //                {
            //                    { "alertId", "242" },
            //                },

            //                Token = item.DeviceToken,
            //                //Topic = "all",

            //                Notification = new Notification()
            //                {
            //                    Title = "Mensaje nuevo",
            //                    Body = body

            //                }
            //            };

            //            response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;

            //        }
            //    }
            //    else
            //    {
            //        message = new Message()
            //        {
            //            Data = new Dictionary<string, string>()
            //                {
            //                    { "alertId", "242" },
            //                },

            //            Token = registrationToken,
            //            //Topic = "all",

            //            Notification = new Notification()
            //            {
            //                Title = "Mensaje nuevo",
            //                Body = body

            //            }
            //        };

            //        response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
            //    }
            //}



            foreach (var kvp in message.Data)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }


            return message;
                    
            //ResponseModel response = new ResponseModel();

            //string serverKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCweMAddIhCamMd\nNHfKwVBAA92zGS4mFx6bLw1kusFcGbRDyBH6clYUT5ShA6If2f8i48SKHYnhumgJ\nTZYToEpF1evVqAkl+X3Oi6Qni+k/26MV8s+Q9cvcwN6VvZ36ImcWjgQwU41dlD9j\nt5xFgL0wpEfp8peU1QTn6plntnNSII51uQtVku6d1HHCbZubv3eewmAceAiCFkRA\nEUzGvQ0MpHEcy9gPUvtKP0IX9Zj1JOmQXXSOCZnuc/7bEzSgD+IlhUkIwCrGl8UM\nzyiGG5YD2/12A2LbokXiMPtspqAQcKWdY10o1ynn5FmTDDkm7ZxjlSxlz2NOOnwN\nZzLqy0ErAgMBAAECggEATV3i/Abm5hc2NMuX6TAvOqF3RNm5PcHNDUPnn02xD+vE\n+CJyn1YZHvZ0ttKc9VHhZS5uMA8bL/dFPS7iqOARL+TFa/oraJak7TSTVzMjo3dr\nXGgDUA4yvilgbE2hQNRsVZsVgwpeY7RmxhTtUhtK1lRvosVUZ0+swvA7KRj4Hfyj\nZb6rnUCzcx/AEfu5IqRlJiyjsUaaBKsYCR64T0XhxAcRuaQj1c5b3VeJCm4uoWgH\ntJwanM6heF6UH/LwEr+MIKDnGjtHe1/jTADh11WzqftLvhcz6uRu0Zjw9nr0edaJ\n+SBcGnnffiStBYHQrGJZcTuznpZDE3mtNIW3C0pdIQKBgQD3123KL1pDSGlQB5og\nqNw42mEuePqkYmcIaW0gGo5OxoOvYmlj6LDwP1DxK615rJhxnz9pgE2BSH+p/9J9\noFMhZJ5ETamnR8nCcwEwGGQm9yeJ94l0h3YpGkWYTRaM2wjOuGOcWT0RI/KS9xDI\n1SD9ouwEF+gdE6FZFVP16KhLRQKBgQC2R+KIbez22xMSvuQPgndmu2Hs+sBB6okq\nDWUdeOnFa6YtGqqcaAxFLHklrKv5psrBvvlXWaLg88qFjaxq9ubsuzn9d9CJoymv\n9Aru+2yQTeYwViQ8N/FKqrJyobhAEmqMuzVhRXHgqxognaHJFBR9OCaHwz+KAAkT\nKTtfWybprwKBgH+j2089g+cS6+njgGVBkelVzqb4d10hsJ6MbT8Tbibz5e31aQGj\nXTzd3vnV3MnFMd2Sfj3/besfQ2Bx+B3q6+VOxJO4y57zQd17DSmP5kSLFTng2lHB\nUgFpHl6JlQuF4stT+zkSXHjvYoZ9548G9K6rsZGKHmibK4WNjmgfIk1hAoGBAKR/\n6yhES/3bZzMboqV8kFTd9lnvUWIrTu3seLxISnAn3igGUMImBCzJHeuqEOmUZJbE\nyjDSa/OnD7XHTTqa53vs20CryD7uSMjJ7LQPosH4CyzEEJ9nvRItSi25VJY+CHpI\n2LuX+FVRFJsqpr7YqET5T4xl4AmiTo5EP5imok9xAoGBAJMc4zvoM7lQwo0PF3bf\nLtWnFGJdiVC/MBQSOTcL7qPgW71bX6qnzgN+YRckojr/ar7tUxVMFp/fLuUNt5Rq\nPMkDKBQeH/ZAEVKhKxOwe4lkwIBHCfTuBErgeGfLkM8A7vOC5Bq0Kder4Fu8kIjq\nEs+BlHnRknjaLjmEHkFOgR3l";
            //string senderId = "796438959253";
            //try
            //{
            //    if (notificationModel.IsAndroiodDevice)
            //    {
            //        /* FCM Sender (Android Device) */
            //        FcmSettings settings = new FcmSettings()
            //        {
            //            SenderId = senderId,
            //            ServerKey = serverKey
            //        };

            //        HttpClient httpClient = new HttpClient();

            //        string authorizationKey = string.Format("key={0}", serverKey);
            //        string deviceToken = "dVPHPzBoRgGvMOgInBXbqL:APA91bGV8Au2acf1Kk_KbLM-mqQVaQ5nPD7cmNB79Fl5cqsEpPkjPPjKMcSxqN4crpNy70-4KYpLJuozR8EmIOI5brifTiVSX-FT_cHTjhoDNcQzbdXsWMWACApjZ80MEWzmxcAZt0tk";

            //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
            //        httpClient.DefaultRequestHeaders.Accept
            //                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        DataPayload dataPayload = new DataPayload();
            //        dataPayload.Title = notificationModel.Title;
            //        dataPayload.Body = notificationModel.Body;

            //        GoogleNotification notification = new GoogleNotification();
            //        notification.Data = dataPayload;
            //        notification.Notification = dataPayload;

            //        var fcm = new FcmSender(settings, httpClient);
            //        var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

            //        if (fcmSendResponse.IsSuccess())
            //        {
            //            response.IsSuccess = true;
            //            response.Message = "Notification sent successfully";
            //            return response;
            //        }
            //        else
            //        {
            //            response.IsSuccess = false;
            //            response.Message = fcmSendResponse.Results[0].Error;
            //            return response;
            //        }
            //    }
            //    else
            //    {
            //        /* Code here for APN Sender (iOS Device) */
            //        //var apn = new ApnSender(apnSettings, httpClient);
            //        //await apn.SendAsync(notification, deviceToken);
            //    }
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    response.IsSuccess = false;
            //    response.Message = "Something went wrong";
            //    return response;
            //}
        }
    }
}