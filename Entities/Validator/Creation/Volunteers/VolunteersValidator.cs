using Back_End.Models.Employees___Dto;
using FluentValidation;

namespace Entities.Validator.Creation.Volunteers
{
    public class VolunteersValidator : AbstractValidator<VolunteersForCreationDto>
    {
        public VolunteersValidator()
        {
            //RuleFor(x => x.Users).SetValidator(new UsersVolunteersValidator());

        }
    }
}
