using Back_End.Models;
using FluentValidation;
using System;
using System.Linq;

namespace Back_End.Validator
{
    public class PersonsValidator : AbstractValidator<PersonForCreationDto> 
    {
        public PersonsValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(IsValidName).WithMessage("{PropertyName} should be all letters.")
            .MaximumLength(100).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(IsValidName).WithMessage("{PropertyName} should be all letters.")
            .MaximumLength(100).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(IsValidNumber).WithMessage("{PropertyName} should be all numbers.")
            .MaximumLength(12).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("Valido")
            .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(x => new[] { "M", "F" ,"O" }.Contains(x))
            .WithMessage("User status should be  either 'M', 'F' or 'O'")
            .MaximumLength(1).WithMessage("The {PropertyName} cannot be more than {MaxLength} character.");

            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("{PropertyName} is required.")
              .LessThan(p => DateTime.Now).WithMessage("the PropertyName} has not passed yet");
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
