using Back_End.Models.Volunteers__Dto;
using FluentValidation;

namespace Entities.Validator.Update.Volunteers
{
    public class VolunteersValidator : AbstractValidator<VolunteersForUpdatoDto>
    {
        public VolunteersValidator()
        {
            RuleFor(x => x.Users).SetValidator(new UsersEmployeesValidator());

        }
    }
}
