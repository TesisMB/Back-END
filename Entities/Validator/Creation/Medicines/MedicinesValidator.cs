using Entities.DataTransferObjects.Medicines___Dto;
using FluentValidation;
using System;

namespace Entities.Validator.Creation.Medicines
{
    public class MedicinesValidator : AbstractValidator<MedicineForCreationDto>
    {
        public MedicinesValidator()
        {

            RuleFor(x => x.MedicineExpirationDate)
        .NotEmpty().WithMessage("{PropertyName} is required.")
            //.GreaterThan(p => DateTime.Now.ToString()).WithMessage("the {PropertyName} has not passed yet")
            .Must(BeAValidDay).WithMessage("El dia ingresado no es valido")
            .Must(BeAValidMonth).WithMessage("El Año ingresado no es valido");

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

        }


        private bool BeAValidMonth(string date)
        {
            int dateTime = Convert.ToInt32(DateTime.Now.Year);


            //string variable = date.Substring(3);
            string variable = date[3..];

            int dateInt = Convert.ToInt32(variable);

            if (dateInt <= 2030 && dateInt > 0 && dateInt >= dateTime)
            {
                return true;
            }
            return false;
        }


        private bool BeAValidDay(string date)
        {

            string variable = date.Substring(0, 2);

            int dateInt = Convert.ToInt32(variable);

            if(dateInt <= 12 && dateInt > 0)
            {
                return true;
            }
            return false;
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
