using Entities.DataTransferObjects.Medicines___Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validator.Creation.Medicines
{
   public class MedicinesValidator: AbstractValidator<MedicineForCreationDto>
    {
        public MedicinesValidator()
        {
          RuleFor(x => x.MedicineName)
         .NotEmpty().WithMessage("{PropertyName} is required.")
         .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

            RuleFor(x => x.MedicineQuantity)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(IsPositiveNumber).WithMessage("Must be a valid age");

            RuleFor(x => x.MedicineExpirationDate)
        .NotEmpty().WithMessage("{PropertyName} is required.")
        .GreaterThan(p => DateTime.Now).WithMessage("the {PropertyName} has not passed yet");

         RuleFor(x => x.MedicineLab)
        .NotEmpty().WithMessage("{PropertyName} is required.")
        .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");
           
         RuleFor(x => x.MedicineDrug)
         .NotEmpty().WithMessage("{PropertyName} is required.")
         .MaximumLength(50).WithMessage("The {PropertyName} must be 50 characters. You entered {TotalLength} characters");

        RuleFor(x => x.MedicineWeight)
        .NotEmpty().WithMessage("{PropertyName} is required.")
        .Must(IsPositiveNumber1).WithMessage("Must be a valid age")
        .Must(IsPositiveNumber1).WithMessage("Must be a valid age");
          

            RuleFor(x => x.FK_EstateID)
         .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private bool IsPositiveNumber1(float num)
        {
            if (num > 0)
                return true;

            return false;
        }

        public static bool IsPositiveNumber(int num)
        {
            if (num > 0)
                return true;

            return false;
        }

    }
}
