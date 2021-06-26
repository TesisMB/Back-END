using Back_End.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Validator.Update
{
    public class PersonsValidator : AbstractValidator<PersonsForUpdatoDto>
    {
        public PersonsValidator()
        {

            RuleFor(x => x.Phone).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(IsValidNumber).WithMessage("{PropertyName} should be all numbers.")
            .MaximumLength(12).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("Valido")
            .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");
        }

        private static bool IsValidNumber(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return name.All(char.IsNumber);
        }

        private bool IsValidName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return name.All(char.IsLetter);
        }
    }
}
