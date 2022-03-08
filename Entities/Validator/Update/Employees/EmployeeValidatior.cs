using Back_End.Models.Employees___Dto;
using FluentValidation;

namespace Entities.Validator.Update
{
    public class EmployeeValidator : AbstractValidator<EmployeeForUpdateDto>
    {
        public EmployeeValidator()
        {

            RuleFor(a => a.Users.UserAvailability).Must(x => x == false || x == true);

            RuleFor(x => x.Users).SetValidator(new UsersEmployeesValidator());

        }
    }
}
