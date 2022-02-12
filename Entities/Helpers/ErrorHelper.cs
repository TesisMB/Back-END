using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Helpers
{
    public class ErrorHelper
    {
        public static ResponseObject Response(int StatusCode, string Message)
        {
            return new ResponseObject()
            {
                Type = "C", //Custom
                StatusCode = StatusCode,
                Message = Message
            };
        }


        //ModelStateDictionary => contiene todos los mensajes de Error
        public static List<ModelErrors> GetModelStateErrors(ModelStateDictionary Model)
        {
            return Model.Select(x => new ModelErrors() { Type = "M", Key = x.Key, Messages = x.Value.Errors.Select(y => y.ErrorMessage).ToList() }).ToList();
        }


        public static List<ModalErrorResourcesStock> GetModelStateErrorsResourcesStock(ModelStateDictionary Model)
        {
            return Model.Select(x => new ModalErrorResourcesStock() { Type = "M", Key = x.Key, Messages = x.Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault()}).ToList();
        }

        public class ResponseObject
        {
            public string Type { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }

        public class ModalErrorResourcesStock
        {
            public string Type { get; set; }
            public string Key { get; set; } // Define al nombre del Campo
            public string Messages { get; set; }
        }

        public class ModelErrors
        {
            public string Type { get; set; }
            public string Key { get; set; } // Define al nombre del Campo
            public List<string> Messages { get; set; }
        }

    }
}
