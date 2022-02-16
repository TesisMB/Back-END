using Back_End.Entities;
using Back_End.Models;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Resources_RequestValidator : AbstractValidator<ResourcesRequestMaterialsMedicinesVehiclesForCreationDto>
    {
        public static Materials materials = null;
        public static Medicines medicines = null;
        public static Vehicles vehicles = null;
        public static CruzRojaContext db = new CruzRojaContext();
        public static ResourcesRequestMaterialsMedicinesVehicles rec = null;

        public static List<string> Key = new List<string>();
        public static List<Resource> Resources = new List<Resource>();

        public Resources_RequestValidator()
        {

            RuleFor(m => new { m.FK_MaterialID, m.Quantity }).Custom((id, context) =>
            {
                materials = (Materials)db.Materials.Where(x => x.ID == id.FK_MaterialID)
                .AsNoTracking()
                .FirstOrDefault();

                if (materials == null)
                {
                DifferentZero(id.FK_MaterialID);

                }
                     else
                {
                    var resta = materials.MaterialQuantity - id.Quantity;

                    if (materials != null  && resta < 0 || materials.MaterialQuantity  == 0)
                    {
                        Key.Add("Material");

                        Resources.Add(new Resource() { ID = id.FK_MaterialID, Name = materials.MaterialName });

                        context.AddFailure("No hay stock");
                    }

                }
            });


            RuleFor(m => new { m.FK_MedicineID, m.Quantity }).Custom((id, context) =>
            {
                medicines = db.Medicines.Where(x => x.ID == id.FK_MedicineID)
                .AsNoTracking()
                .FirstOrDefault();

                if (medicines == null)
                {
                    DifferentZero2(id.FK_MedicineID);
                }
                else
                {
                    var resta = medicines.MedicineQuantity - id.Quantity;
                    if (medicines != null && resta < 0 || medicines.MedicineQuantity == 0)
                    {

                        Key.Add("Medicine");
                        Resources.Add(new Resource() { ID = id.FK_MedicineID, Name = medicines.MedicineName });

                        context.AddFailure("No hay stock");

                    }
                }
            });


            RuleFor(m => new { m.FK_VehicleID, m.Quantity }).Custom((id, context) =>
            {
                vehicles = db.Vehicles.Where(x => x.ID == id.FK_VehicleID)
                .Include(a => a.BrandsModels)
                .Include(a => a.BrandsModels.Model)
                .Include(a => a.BrandsModels.Brands)
                .AsNoTracking()
                .FirstOrDefault();

                if (vehicles == null)
                {
                    DifferentZero3(id.FK_VehicleID);
                }
                else
                {
                    if (vehicles != null && (vehicles.VehicleAvailability == false))
                    {
                        var vehiculo = vehicles.BrandsModels.Brands.BrandName + " " + vehicles.BrandsModels.Model.ModelName;
                        Key.Add("Vehicle");
                        Resources.Add(new Resource() { ID = id.FK_VehicleID, Name = vehiculo });

                        context.AddFailure("No se en cuentra disponible");
                    }
                }
            });

        }


        public  class Resource
        {
            public  int? ID { get; set; }
            public  string Name { get; set; } // Define al nombre del Campo
        }

 


        public bool DifferentZero(int? id)
        {
            if (id == null)
            {
                return true;
            }

               return false;
        }


        private bool DifferentZero2(int? id)
        {
            if (id == null)
            {
                return true;
            }

            return false;
        
        }


        private bool DifferentZero3(int? id)
        {
            if (id == null)
            {
                return true;
            }

            return false;
        }
    }
}
