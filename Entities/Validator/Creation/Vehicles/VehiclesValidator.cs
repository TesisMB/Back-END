using Back_End.Entities;
using Entities.DataTransferObjects.Vehicles___Dto.Creation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Entities.Validator.Creation.Vehicles
{
    public class VehiclesValidator : AbstractValidator<VehiclesForCreationDto>
    {
        public VehiclesValidator()
        {
            RuleFor(x => x.VehiclePatent)
            .Must(BeUniqueDni).WithMessage("Esta patente ya existe en el sistema");


        }

        private bool BeUniqueDni(string patente)
        {
                return new CruzRojaContext().Vehicles
                    .AsNoTracking()
                    .FirstOrDefault(x => x.VehiclePatent == patente) == null;
        }


    }
}
