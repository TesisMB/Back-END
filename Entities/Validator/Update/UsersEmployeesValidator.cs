using Back_End.Entities;
using Back_End.Helpers;
using Back_End.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
namespace Entities.Validator.Update
{
    public class UsersEmployeesValidator : AbstractValidator<UsersForUpdateDto>
    {
        public UsersEmployeesValidator()
        {
            //************USERS - VALIDATIONS************z
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("{PropertyName} is required.")
            //.Must(BeUniqueUrl).WithMessage("Contraseña Incorrecta")
            .Length(8, 16).WithMessage("The {PropertyName} must be between 8 and 16 characters. You entered {TotalLength} characters");


            RuleFor(x => x.UserNewPassword).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(x => x.UserPassword).WithMessage("Debe Ingesar otra Contraseña");


            //Validaciones para Rol
            RuleFor(x => x.FK_RoleID).NotEmpty().WithMessage("{PropertyName} is required.");

            //Heredo las validaciones de Persons
            RuleFor(x => x.Persons).SetValidator(new PersonsValidator());
        }


        private bool BeUniqueUrl(string Pass)
        {
            var ePass = Encrypt.GetSHA256(Pass);

            if (new CruzRojaContext().Users.FirstOrDefault(x => x.UserPassword != ePass) == null)
            {
                return false;
            }

            return true;
        }
        
        private static bool IsValidNumber(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return name.All(char.IsNumber);
        }
    }
}*/
