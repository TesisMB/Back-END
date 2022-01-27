using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using FluentValidation;

namespace Entities.Validator.Creation.Resources_Request
{
    public class Resources_RequestValidator : AbstractValidator<Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto>
    {
        public Resources_RequestValidator()
        {
            RuleFor(x => x.Quantity)
            .Must(DifferentZero).WithMessage("Error");
        }

        public bool DifferentZero(int? quantity)
        {
            if (quantity > 0)
            {
                return true;
            }
            return false;
        }

    }
}
