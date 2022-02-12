

using Entities.DataTransferObjects.Resources_Request___Dto;
using FluentValidation;

namespace Entities.Validator.Update.Resources_Request
{
   public class Resources_RequestForUpdateValidator : AbstractValidator<Resource_RequestForUpdateDto>
    {
        public Resources_RequestForUpdateValidator()
        {
            //RuleFor(x => x.Reason).NotEmpty();

            RuleForEach(x => x.Resources_RequestResources_Materials_Medicines_Vehicles).SetValidator(new Resources_RequestValidator());

        }
    }
}
