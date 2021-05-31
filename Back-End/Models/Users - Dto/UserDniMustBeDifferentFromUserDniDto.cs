using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Users_Dto
{
    public class UserDniMustBeDifferentFromUserDniDto: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var User = (UsersForCreationDto)validationContext.ObjectInstance;

            using (var db = new CruzRojaContext())


                if (db.Users.Any(a => a.UserDni == User.UserDni))
                {
                    return new ValidationResult
                    (ErrorMessage);

                }

            return ValidationResult.Success;
        }
    }
}
