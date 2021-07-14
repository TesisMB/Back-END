using Entities.DataTransferObjects.Materials___Dto;
using FluentValidation;

namespace Entities.Validator.Update.Materials
{
    public class MaterialsValidator : AbstractValidator<MaterialsForUpdateDto>
    {
        public MaterialsValidator()
        {

            RuleFor(x => x.MaterialQuantity)
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");
            
            RuleFor(x => x.MaterialAvailability).Must(x => x == false || x == true);
        }
    }
}
