using Entities.DataTransferObjects.Materials___Dto;
using Entities.Validator.Creation.Medicines;
using FluentValidation;

namespace Entities.Validator.Update.Materials
{
    public class MaterialsValidator : AbstractValidator<MaterialsForUpdateDto>
    {
        public MaterialsValidator()
        {

            RuleFor(x => x.MaterialQuantity)
            .Must(MedicinesValidator.IsPositiveNumber).WithMessage("Must be a valid age");

            RuleFor(x => x.MaterialAvailability).Must(x => x == false || x == true);
        }
    }
}
