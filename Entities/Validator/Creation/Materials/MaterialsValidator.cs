using Entities.DataTransferObjects.Materials___Dto;
using Entities.Validator.Creation.Medicines;
using FluentValidation;


namespace Entities.Validator.Creation
{
    public class MaterialsValidator : AbstractValidator<MaterialsForCreationDto>
    {
        public MaterialsValidator()
        {
            RuleFor(x => x.MaterialName)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.MaterialBrand)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

             RuleFor(x => x.MaterialQuantity)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(MedicinesValidator.IsPositiveNumber).WithMessage("Must be a valid age");


            RuleFor(x => x.MaterialName)
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");
            RuleFor(x => x.FK_EstateID).NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
