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
             RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(100).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

             RuleFor(x => x.LastName)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(100).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

             RuleFor(x => x.Phone)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .Must(IsValidNumber).WithMessage("{PropertyName} must not have spaces and should be all numbers.")
            .MaximumLength(12).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

             RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

             RuleFor(x => x.Gender).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(x => new[] { "M", "F", "O" }.Contains(x))
            .WithMessage("User status should be  either 'M', 'F' or 'O'")
            .MaximumLength(1).WithMessage("The {PropertyName} cannot be more than {MaxLength} character.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");
            
            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("{PropertyName} is required.")
           .LessThan(p => DateTime.Now).WithMessage("the PropertyName} has not passed yet");
        }


        private bool IsValidNumber(string name)
        {
            return name.All(char.IsNumber);
        }
    }
}
