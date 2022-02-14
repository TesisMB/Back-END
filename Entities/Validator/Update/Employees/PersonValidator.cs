using Back_End.Models;
using FluentValidation;
using System;
using System.Linq;

namespace Entities.Validator.Update
{
    public class PersonsValidator : AbstractValidator<PersonsForUpdatoDto>
    {
        public PersonsValidator()
        {
            RuleFor(x => x.Phone)
           .Must(IsValidNumber).WithMessage("{PropertyName} should be all numbers.")
           .MaximumLength(12).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Email)
           .EmailAddress().WithMessage("A valid email address is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(stockImage => stockImage.Status).Must(x => x == false || x == true);

            RuleFor(x => x.Address).MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");
        }

        private static bool IsValidNumber(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return name.All(char.IsNumber);
        }
    }
}
