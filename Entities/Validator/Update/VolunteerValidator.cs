using Back_End.Models.Volunteers__Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Update
{
   public class VolunteerValidator : AbstractValidator<VolunteersForUpdatoDto>
    {
        public VolunteerValidator()
        {
            RuleFor(x => x.Users).SetValidator(new UsersEmployeesValidator());

        }
    }
}
