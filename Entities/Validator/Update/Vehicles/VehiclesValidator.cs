using Entities.DataTransferObjects.Vehicles___Dto.Update;
using FluentValidation;

namespace Entities.Validator.Update.Vehicles
{
    public class VehiclesValidator : AbstractValidator<VehiclesForUpdateDto>
    {
        public VehiclesValidator()
        {

            RuleFor(x => x.VehicleUtility)
           .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.VehicleAvailability).Must(x => x == false || x == true);




        }
    }
}
