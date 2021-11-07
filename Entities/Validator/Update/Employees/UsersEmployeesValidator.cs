
using Back_End.Models;
using FluentValidation;


namespace Entities.Validator.Update
{
    public class UsersEmployeesValidator : AbstractValidator<UsersForUpdateDto>
    {
        public UsersEmployeesValidator()
        {
            //************USERS - VALIDATIONS************z

             RuleFor(x => x.UserNewPassword)
            .Length(8, 16).WithMessage("The {PropertyName} must be between 8 and 16 characters. You entered {TotalLength} characters")
            .NotEqual(x => x.UserPassword).WithMessage("Debe Ingesar otra Contraseña");


             //Heredo las validaciones de Persons
             RuleFor(x => x.Persons).SetValidator(new PersonsValidator());
        }
    }
}
