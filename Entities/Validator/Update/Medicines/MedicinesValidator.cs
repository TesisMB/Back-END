using Entities.DataTransferObjects.Medicines___Dto;
using FluentValidation;
using System;

namespace Entities.Validator.Update.Medicines
{
    public class MedicinesValidator : AbstractValidator<MedicineForUpdateDto>
    {
        public MedicinesValidator()
        {

            RuleFor(x => x.MedicineExpirationDate).LessThan(p => DateTime.Now).WithMessage("the PropertyName} has not passed yet");

            RuleFor(x => x.MedicineAvailability).Must(x => x == false || x == true);

        }
    }
}
