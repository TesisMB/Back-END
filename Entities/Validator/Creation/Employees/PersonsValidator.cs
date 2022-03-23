using Back_End.Entities;
using Back_End.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Back_End.Validator
{
    public class PersonsValidator : AbstractValidator<PersonForCreationDto>
    {
        public PersonsValidator()
        {
            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .MaximumLength(100).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .MaximumLength(100).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .Must(IsValidNumber).WithMessage("{PropertyName} must not have spaces and should be all numbers.")
           .MaximumLength(12).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required")
           .EmailAddress().WithMessage("A valid email address is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.")
           .Must(BeUniqueEmail).WithMessage("El email ingresado ya existe en el sistema");

            RuleFor(x => x.Gender).NotEmpty().WithMessage("{PropertyName} is required")
           .Must(x => new[] { "M", "F", "O" }.Contains(x))
           .WithMessage("User status should be  either 'M', 'F' or 'O'")
           .MaximumLength(1).WithMessage("The {PropertyName} cannot be more than {MaxLength} character.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(50).WithMessage("The {PropertyName} cannot be more than {MaxLength} characters.");

            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(BeAValidAge).WithMessage("El usuario ingresado debe ser mayor de 18 años")
            .LessThan(p => DateTime.Now).WithMessage("La fecha no puede sobrepasar la fecha actual");
        }

        private bool BeUniqueEmail(string Email)
        {
            return new CruzRojaContext().Persons
                .AsNoTracking()
                .FirstOrDefault(x => x.Email == Email) == null;
        }

        private bool BeAValidAge(DateTime? date)
        {
            if (date.HasValue)
                return (DateTime.Now.Year - date.Value.Year) >= 18;

            return false;
        }

        private bool IsValidNumber(string name)
        {
            return name.All(char.IsNumber);
        }
    }
}
