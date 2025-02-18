﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Back_End.Controllers
{
    public class BaseApiController : Controller
    {
        protected IActionResult HandleException(Exception ex, string msg)
        {
            IActionResult result;

            //Se crea una nueva excepcion generica con un mensaje
            result = StatusCode(StatusCodes.Status500InternalServerError, new Exception(msg, ex));

            return result;
        }
    }
}