using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Creation.EmergenciesDisasters
{
    public class EmergenciesDisastersValidator: AbstractValidator<EmergenciesDisastersForCreationDto>
    {
        public EmergenciesDisastersValidator()
        {

            RuleFor(x => x.FK_LocationID).NotEmpty().WithMessage("{PropertyName} is required.");
           
            RuleFor(x => x.FK_TypeEmergencyID).NotEmpty().WithMessage("{PropertyName} is required.");
            
            RuleFor(x => x.FK_AlertID).NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
