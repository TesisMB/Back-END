using Entities.DataTransferObjects.Materials___Dto;
using FluentValidation;


namespace Entities.Validator.Creation.Materials
{
    public class MaterialsValidator : AbstractValidator<MaterialsForCreationDto>
    {
        public MaterialsValidator()
        {
            RuleFor(x => x.MaterialName)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.MaterialMark)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.MaterialQuantity)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.MaterialName)
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.FK_EstateID).NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
