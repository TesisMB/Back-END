using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Resources_RequestRepository : RepositoryBase<Resources_Request>, IResources_RequestRepository
    {
        private CruzRojaContext _cruzRojaContext;
        public Resources_RequestRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }


        public async Task<IEnumerable<Resources_Request>> GetAllResourcesRequest()
        {
            var user = UsersRepository.authUser;

            var collection = _cruzRojaContext.Resources_Requests as IQueryable<Resources_Request>;

                  collection = collection.Where(
                      a => a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

             /*if (user.Roles.RoleName == "Encargado de Logistica")
              {
              }
              else
              {
                  return null;
              }*/

            return await collection
                //.Include(i => i.Users)
                .Include(i => i.EmergenciesDisasters)
                //.ThenInclude(i => i.TypesEmergenciesDisasters)
                //.Include(i => i.EmergenciesDisasters.Alerts)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.TypeVehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.BrandsModels)
                .ThenInclude(i => i.Brands)
                .ThenInclude(i => i.BrandsModels)
                .ThenInclude(i => i.Model)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Resources_Materials)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Resources_Medicines)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .Include(i => i.Users.Estates.LocationAddress)
                .Include(i => i.Users.Estates.Locations)
                .Include(i => i.Users.Locations)
                .ToListAsync();
        }
        public void CreateResource_Resquest(Resources_Request resources_Request)
        {
            spaceCamelCase(resources_Request);
            Create(resources_Request);
            UpdateResources(resources_Request);
        }

        private void spaceCamelCase(Resources_Request resources_Request)
        {
            //Falta implementarlos en el PATCH
            if (resources_Request.Reason != null)
            {
                resources_Request.Reason = WithoutSpace_CamelCase.GetCamelCase(resources_Request.Reason);
            }
        }

        public void UpdateResources(Resources_Request resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;

            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                var db = new CruzRojaContext();

                if (resources.Resources_Materials != null)
                {
                    materials = db.Materials
                        .Include(i => i.Resources_Materials)
                        .Where(
                         a => a.ID == resources.Resources_Materials.FK_MaterialID)
                        .FirstOrDefault();

                    resources.Resources_Materials.Materials = materials;

                }

                if (resources.Resources_Medicines != null)
                {
                    medicines = db.Medicines
                    .Include(i => i.Resources_Medicines)
                    .Where(
                     a => a.ID == resources.Resources_Medicines.FK_MedicineID)
                    .FirstOrDefault();

                    resources.Resources_Medicines.Medicines = medicines;
                }

                if (resources.FK_VehiclesID != null)
                {
                    vehicles = db.Vehicles
                   .Where(
                    a => a.ID == resources.FK_VehiclesID)
                   .FirstOrDefault();

                    resources.Vehicles = vehicles;
                }


                if (resources.Resources_Materials != null)
                {
                    resources.Resources_Materials.Materials.MaterialQuantity = materials.MaterialQuantity - resources.Resources_Materials.Quantity;

                    if (resources.Resources_Materials.Materials.MaterialQuantity == 0)
                    {
                        resources.Resources_Materials.Materials.MaterialAvailability = false;
                    }
                }

                if (resources.Resources_Medicines != null)
                {
                    resources.Resources_Medicines.Medicines.MedicineQuantity = medicines.MedicineQuantity - resources.Resources_Medicines.Quantity;

                    if (resources.Resources_Medicines.Medicines.MedicineQuantity == 0)
                    {
                        resources.Resources_Medicines.Medicines.MedicineAvailability = false;
                    }
                }


                if (resources.Vehicles != null)
                {
                    resources.Vehicles.VehicleAvailability = false;
                }
                //Update(resources);
                //SaveAsync();
            }
        }

        public async Task<Resources_Request> GetResourcesRequestByID(int resource)
        {
            return await FindByCondition(res => res.ID.Equals(resource))
                .Include(i => i.Users)
                .Include(i => i.EmergenciesDisasters)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Resources_Materials)
                .ThenInclude(i => i.Materials)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Resources_Medicines)
                .ThenInclude(i => i.Medicines)
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.Alerts)
                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .ThenInclude(i => i.Locations)
                .Include(i => i.Users.Locations)
                .FirstOrDefaultAsync();
        }

        public void UpdateResource_Resquest(Resources_Request resources_Request)
        {
            Update(resources_Request);
        }


        public void UpdateResource_Resquest2(Resources_Request resources_Request)
        {

            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resources_Request.Status == false)
                {

                    if (resources.Vehicles == null && resources.Resources_Medicines == null && resources.Resources_Materials != null)
                    {
                        resources.Resources_Materials.Materials.MaterialQuantity = resources.Resources_Materials.Materials.MaterialQuantity + resources.Resources_Materials.Quantity;
                        resources.Resources_Materials.Materials.MaterialAvailability = true;
                        _cruzRojaContext.Entry(resources.Resources_Materials.Materials).CurrentValues.SetValues(resources.Resources_Materials.Materials);
                    }


                    if (resources.Vehicles == null && resources.Resources_Materials == null && resources.Resources_Medicines != null)
                    {
                        resources.Resources_Medicines.Medicines.MedicineQuantity = resources.Resources_Medicines.Medicines.MedicineQuantity + resources.Resources_Medicines.Quantity;
                        resources.Resources_Medicines.Medicines.MedicineAvailability = true;
                        _cruzRojaContext.Entry(resources.Resources_Medicines.Medicines).CurrentValues.SetValues(resources.Resources_Medicines.Medicines);
                    }

                    if (resources.Resources_Materials == null && resources.Resources_Medicines == null && resources.Vehicles != null)
                    {
                        resources.Vehicles.VehicleAvailability = true;
                        //Entry le dice al DbContext que cambie el estado de la entidad en modificado
                        //setvalues realiza una copia del objeto pasado con los cambios
                        _cruzRojaContext.Entry(resources.Vehicles).CurrentValues.SetValues(resources.Vehicles);
                    }
                }

            }
            Update(resources_Request);
        }

        public Resources_Request Stock(Resources_Request resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;

            resources_Request.FK_UserID = 2;

            var db = new CruzRojaContext();

            foreach (var resource in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resource.Resources_Materials != null)
                {
                    materials = db.Materials
                     .Where(i => i.ID == resource.Resources_Materials.FK_MaterialID
                     // &&
                     //   (i.MaterialQuantity - resource.Resources_Materials.Quantity) >= 0
                     )
                     .FirstOrDefault();

                    resource.Resources_Materials.Materials = materials;

                    if (materials == null)
                    {
                        resource.Resources_Materials = null;
                    }

                }

                if (resource.Resources_Medicines != null)
                {
                    medicines = db.Medicines
                        .Where(i => i.ID == resource.Resources_Medicines.FK_MedicineID
                           //  &&
                           //(i.MedicineQuantity - resource.Resources_Medicines.Quantity) >= 0
                           )
                        .FirstOrDefault();

                    resource.Resources_Medicines.Medicines = medicines;

                    if (medicines == null)
                    {
                        resource.Resources_Medicines = null;
                    }
                }

                if (resource.FK_VehiclesID != null)
                {
                    vehicles = db.Vehicles
                        .Where(i => i.ID == resource.FK_VehiclesID
                           //  &&
                           //(i.MedicineQuantity - resource.Resources_Medicines.Quantity) >= 0
                           )
                        .FirstOrDefault();

                    resource.Vehicles = vehicles;

                    if (vehicles == null)
                    {
                        resource.Vehicles = null;
                    }
                }
            }
            return resources_Request;
        }

        public Resources_Request DeleteResource(Resources_Request resources_Request)
        {

            foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (item.Resources_Materials != null)
                {
                    item.Resources_Materials.Materials = null;
                }

                if (item.Resources_Medicines != null)
                {
                    item.Resources_Medicines.Medicines = null;
                }

                if (item.Vehicles != null)
                {
                    item.Vehicles = null;
                }
            }

            return resources_Request;

        }
    }
}
