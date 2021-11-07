using Back_End.Models;
using Back_End.Validator;
using FluentValidation;


namespace Entities.Validator
{
    public class EmployeesValidator : AbstractValidator<EmployeesForCreationDto>
    {
        public EmployeesValidator()
        {
            RuleFor(x => x.Users).SetValidator(new UsersEmployeesValidator());
        }
    }
}
