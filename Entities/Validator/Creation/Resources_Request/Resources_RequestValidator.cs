using Back_End.Entities;
using Entities.DataTransferObjects.Resources_Request___Dto;
using FluentValidation;
using System.Linq;

namespace Entities.Validator.Creation.Resources_Request
{
    public class Resources_RequestValidator: AbstractValidator<Resources_RequestForCreationDto>
    {
        public Resources_RequestValidator()
        {
            RuleFor(x => x.Resources.Resources_Materials.Quantity)
            .Must(DifferentZero).WithMessage("Error"); 
     
            RuleFor(x => x.Resources.Resources_Medicines.Quantity)
            .Must(DifferentZero).WithMessage("Error");

        }


        public bool DifferentZero(int quantity)
        {
            if(quantity > 0)
            {
            return true;
            }
            return false;
        }

    }
}
