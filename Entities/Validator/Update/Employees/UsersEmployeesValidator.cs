using Back_End.Entities;
using Back_End.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Entities.Validator.Update
{
    public class UsersEmployeesValidator : AbstractValidator<UsersForUpdateDto>
    {
        CruzRojaContext db = new CruzRojaContext();
        Employees employees = new Employees();
        public UsersEmployeesValidator()
        {
            //************USERS - VALIDATIONS************z


            RuleFor(x => new { x.userID, x.UserDni }).Custom((id, context) =>
            {

                employees = db.Employees.Where(a => a.EmployeeID == id.userID
                                               && a.Users.UserDni == id.UserDni)
                                             .AsNoTracking()
                                             .FirstOrDefault();

                var result = BeUniqueDni(employees, id.UserDni);

                if (result == false)
                {
                    context.AddFailure("El Dni ingresado ya existe en el sistema");
                }
            });


            RuleFor(x => x.UserDni)
                 .NotEmpty().WithMessage("{PropertyName} is required.")
                 .Must(IsValidNumber).WithMessage("{PropertyName} must not have spaces and should be all numbers.")
                 .MaximumLength(16).WithMessage("El Dni debe tener hasta 16 caracteres");

            RuleFor(x => x.UserNewPassword)
           .Length(8, 16).WithMessage("La nueva contraseña debe tener entre 8 a 16 caracteres. Usted ingreso {TotalLength} caracteres")
           .NotEqual(x => x.UserPassword).WithMessage("Debe Ingesar otra Contraseña");


            //Heredo las validaciones de Persons
            RuleFor(x => x.Persons).SetValidator(new PersonsValidator());
        }

        private bool BeUniqueDni(Employees employees, string Dni)
        {
            if (employees == null)
            {
                return new CruzRojaContext().Users
                .AsNoTracking()
                .FirstOrDefault(x => x.UserDni == Dni) == null;
            }

            return true;
        }


        private static bool IsValidNumber(string name)
        {
            return name.All(char.IsNumber);
        }
    }
}
