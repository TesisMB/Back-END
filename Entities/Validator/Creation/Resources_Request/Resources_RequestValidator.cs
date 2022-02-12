using Back_End.Entities;
using Back_End.Models;
using Entities.DataTransferObjects.Resources_RequestResources_Materials_Medicines_Vehicles___Dto;
using Entities.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Entities
{
    public class Resources_RequestValidator : AbstractValidator<Resources_RequestResources_Materials_Medicines_VehiclesForCreationDto>
    {
        public static Materials materials = null;
        public static Medicines medicines = null;
        public static Vehicles vehicles = null;
        public static CruzRojaContext db = new CruzRojaContext();
        public static Resources_RequestResources_Materials_Medicines_Vehicles rec = null;

        public Resources_RequestValidator()
        {

            RuleFor(m => new { m.FK_MaterialID, m.Quantity }).Custom((id, context) =>
            {
                materials = db.Materials.Where(x => x.ID == id.FK_MaterialID).FirstOrDefault();


                if (materials == null)
                {
                DifferentZero(id.FK_MaterialID);

                }
                     else
                {
                    if (materials != null  && ((materials.MaterialQuantity - id.Quantity) < 0))
                    {
                        context.AddFailure("Id: " + id.FK_MaterialID + " - Material: " + materials.MaterialName);
                    }
              
                }
            });


            RuleFor(m => new { m.FK_MedicineID, m.Quantity }).Custom((id, context) =>
            {
                medicines = db.Medicines.Where(x => x.ID == id.FK_MedicineID).FirstOrDefault();

                if (medicines == null)
                {
                    DifferentZero2(id.FK_MedicineID);
                }
                else
                {
                    if (medicines != null && (medicines.MedicineQuantity - id.Quantity < 0))
                    {

                        context.AddFailure("Id: " + id.FK_MedicineID + " - Medicine: " + medicines.MedicineName);
                    }
                }
            });


            RuleFor(m => new { m.FK_VehicleID, m.Quantity }).Custom((id, context) =>
            {
                vehicles = db.Vehicles.Where(x => x.ID == id.FK_VehicleID)
                .Include(a => a.BrandsModels)
                .Include(a => a.BrandsModels.Model)
                .Include(a => a.BrandsModels.Brands)

                .FirstOrDefault();

                if (vehicles == null)
                {
                    DifferentZero3(id.FK_VehicleID);
                }
                else
                {
                    if (vehicles != null && (vehicles.VehicleAvailability == false))
                    {
                        context.AddFailure("Id: " + id.FK_VehicleID + " - Vehcile: " + vehicles.BrandsModels.Brands.BrandName + " " + vehicles.BrandsModels.Model.ModelName);
                    }
                }
            });


            
        }


        public bool DifferentZero(string? id)
        {
            if (id == "")
            {
                return true;
            }

               return false;
        }


        private bool DifferentZero2(string? id)
        {
            if (id == "")
            {
                return true;
            }

            return false;
        
        }


        private bool DifferentZero3(string? id)
        {
            if (id == "")
            {
                return true;
            }

            return false;
        }
    }
}
