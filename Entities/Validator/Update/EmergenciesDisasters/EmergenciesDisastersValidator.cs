using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using FluentValidation;
using System;

namespace Entities.Validator.Update.EmergenciesDisasters
{
    public class EmergenciesDisastersValidator : AbstractValidator<EmergenciesDisastersForUpdateDto>
    {
        public EmergenciesDisastersValidator()
        {
            RuleFor(x => x.EmergencyDisasterEndDate).LessThan(p => DateTime.Now).WithMessage("the PropertyName} has not passed yet");
        }
    }
}
