using Entities.DataTransferObjects.TypeVehicles___Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Creation.Vehicles
{
    public class TypeVehiclesValidator : AbstractValidator<TypeVehiclesForCreationDto>
    {
        public TypeVehiclesValidator()
        {
            /*RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("The {PropertyName} must be 9 characters. You entered {TotalLength} characters");


            RuleFor(x => x.Model)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");*/
        }
    }
}
