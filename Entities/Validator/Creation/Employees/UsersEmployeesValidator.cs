using Back_End.Entities;
using Back_End.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Back_End.Validator
{
    public class UsersEmployeesValidator : AbstractValidator<UsersEmployeesForCreationDto>
    {
        //ctor
        public UsersEmployeesValidator()
        {

            RuleFor(x => x.UserDni)
           .Cascade(CascadeMode.StopOnFirstFailure)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .Must(BeUniqueDni).WithMessage("Dni already exists")
           .Must(IsValidNumber).WithMessage("{PropertyName} must not have spaces and should be all numbers.")
           .Length(8).WithMessage("The {PropertyName} must be 8 characters. You entered {TotalLength} characters");

            RuleFor(x => x.FK_RoleID).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.FK_EstateID).NotEmpty().WithMessage("{PropertyName} is required.");


            //Heredo las validaciones de Persons
            RuleFor(x => x.Persons).SetValidator(new PersonsValidator());
        }

        //Esta funcion me permite verificar que se ingresar en el campo UserDni valores unicos.
        private bool BeUniqueDni(string Dni)
        {
            return new CruzRojaContext().Users
                .AsNoTracking()
                .FirstOrDefault(x => x.UserDni == Dni) == null;
        }


        private static bool IsValidNumber(string name)
        {
            return name.All(char.IsNumber);
        }
    }
}
