

using Entities.DataTransferObjects.Resources_Request___Dto;
using FluentValidation;

namespace Entities.Validator.Creation.Resources_Request
{
    public class Resource_RequesForUpdateValidator : AbstractValidator<Resources_RequestForCreationDto>
    {
        public Resource_RequesForUpdateValidator()
        {

           RuleForEach(x => x.Resources_RequestResources_Materials_Medicines_Vehicles).SetValidator(new Resources_RequestValidator());

        }
    }
}
