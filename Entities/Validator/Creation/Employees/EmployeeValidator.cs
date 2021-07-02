using Back_End.Models;
using Back_End.Validator;
using FluentValidation;


namespace Entities.Validator
{
    public class EmployeeValidator : AbstractValidator<EmployeesForCreationDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Users).SetValidator(new UsersEmployeesValidator());

        }
    }
}
