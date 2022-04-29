using Entities.DataTransferObjects.ResourcesDto;
using Entities.Validator.Creation.Medicines;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Creation.Resources_Request
{
    public class ResourcesValidator: AbstractValidator<Resources_ForCreationDto>
    {
        public ResourcesValidator()
        {

            RuleFor(x => x.Medicines).SetValidator(new MedicinesValidator());


        }
    }
}
