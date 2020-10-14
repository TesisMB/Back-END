using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Users___Dto
{
    public class UserDniMustBeDifferentFromUserDniDto : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Course = (UsersForCreationDto)validationContext.ObjectInstance;

            using (var db = new CruzRojaContext())
            
            if (db.Users.Any(a => a.UserDni == Course.UserDni))
                {
                    return new ValidationResult
                    (ErrorMessage,
                        new[] { nameof(UsersForCreationDto) });

                }


            return ValidationResult.Success;
        }

    }
}
