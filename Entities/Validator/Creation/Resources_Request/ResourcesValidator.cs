using Back_End.Entities;
using Entities.DataTransferObjects.ResourcesDto;
using Entities.Validator.Creation.Medicines;
using Entities.Validator.Creation.Vehicles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Validator.Creation.Resources_Request
{
    public class ResourcesValidator: AbstractValidator<Resources_ForCreationDto>
    {
        public ResourcesValidator()
        {

            RuleFor(x => x.ID)
                  .Must(BeUniqueDni).WithMessage("Este codigo ya existe en el sistema");

            RuleFor(x => x.Medicines).SetValidator(new MedicinesValidator());
            RuleFor(x => x.Vehicles).SetValidator(new VehiclesValidator());
        }


        private bool BeUniqueDni(string codigo)
        {
            string codigoID = codigo.Substring(0, 2);

            if(codigoID == "MA")
                return new CruzRojaContext().Materials
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ID == codigo) == null;
            
            else
                if (codigoID == "ME")
                    return new CruzRojaContext().Medicines
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ID == codigo) == null;
            else
                return new CruzRojaContext().Vehicles
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ID == codigo) == null;
        }
    }
}
