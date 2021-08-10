using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }

    }
}
