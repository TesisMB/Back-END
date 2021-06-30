using Back_End.Entities;
using Back_End.Models;
using FluentValidation;
using System;
using System.Linq;

namespace Back_End.Validator
{
    public class UsersEmployeesValidator : AbstractValidator<UsersEmployeesForCreationDto>
    {
        //ctor
        public UsersEmployeesValidator()
        {
            //************USERS - VALIDATIONS************

            //Validaciones para el Dni
            RuleFor(x => x.UserDni).NotEmpty().WithMessage("{PropertyName} is required.")
             .Must(IsValidNumber).WithMessage("{PropertyName} should be all numbers.")
              .Must(BeUniqueUrl).WithMessage("Dni already exists")

             .Length(8).WithMessage("The {PropertyName} must be 8 characters. You entered {TotalLength} characters");
            //Validaciones para la contraseña

            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(8, 16).WithMessage("The {PropertyName} must be between 8 and 16 characters. You entered {TotalLength} characters");

            //Validaciones para Rol
            RuleFor(x => x.FK_RoleID).NotEmpty().WithMessage("{PropertyName} is required.");





            //Heredo las validaciones de Persons
            RuleFor(x => x.Persons).NotEmpty().WithMessage("{PropertyName} is required.").SetValidator(new PersonsValidator());
        }



        private bool BeUniqueUrl(string Dni)
        {
            return new CruzRojaContext().Users.FirstOrDefault(x => x.UserDni == Dni) == null;
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
