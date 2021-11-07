using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Update.EmergenciesDisasters
{
    public class EmergenciesDisastersValidator: AbstractValidator<EmergenciesDisastersForUpdateDto>
    {
        public EmergenciesDisastersValidator()
        {
             RuleFor(x => x.EmergencyDisasterEndDate).LessThan(p => DateTime.Now).WithMessage("the PropertyName} has not passed yet");
        }
    }
}
