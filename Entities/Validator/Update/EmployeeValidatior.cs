using Back_End.Models.Employees___Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Update
{
    public class EmployeeValidator : AbstractValidator<EmployeeForUpdateDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Users).SetValidator(new UsersEmployeesValidator());

        }
    }
}
