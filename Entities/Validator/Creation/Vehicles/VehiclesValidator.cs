using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using FluentValidation;

namespace Entities.Validator.Creation.Vehicles
{
    public class VehiclesValidator : AbstractValidator<VehiclesForCreationDto>
    {
        public VehiclesValidator()
        {
            RuleFor(x => x.VehiclePatent)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(9).WithMessage("The {PropertyName} must be 9 characters. You entered {TotalLength} characters");


           /* RuleFor(x => x.VehicleUtility)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");


            */
        }
    }
}
