using Back_End.Entities;
using Back_End.Models;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Back_End.Validator
{
    public class UsersValidator : AbstractValidator<UsersForCreationDto>
    {
        //ctor
        public UsersValidator()
        {
            //************USERS - VALIDATIONS************

            //Validaciones para el Dni
            RuleFor(x => x.UserDni).NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(IsValidNumber).WithMessage("{PropertyName} should be all numbers.")
            .Length(8).WithMessage("The {PropertyName} must be 8 characters. You entered {TotalLength} characters");
            //Validaciones para la contraseña
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(8,16).WithMessage("The {PropertyName} must be between 8 and 16 characters. You entered {TotalLength} characters");

            //Validaciones para Rol
            RuleFor(x => x.FK_RoleID).NotEmpty().WithMessage("{PropertyName} is required.");

            //Heredo las validaciones de Persons
            RuleFor(x => x.Persons).NotEmpty().WithMessage("{PropertyName} is required.").SetValidator(new PersonsValidator());
        }


            private static bool IsValidNumber(string name)
        {
            return name.All(char.IsNumber);
        }
    }
}
