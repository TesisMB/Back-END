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
        private CruzRojaContext _cruzRojaContext = new CruzRojaContext();
        public static CruzRojaContext db = new CruzRojaContext();
        public static Resources_RequestResources_Materials_Medicines_Vehicles recursos = null;

        public Resources_RequestRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }


        public async Task<IEnumerable<Resources_Request>> GetAllResourcesRequest()
        {
            var user = UsersRepository.authUser;

            //var collection = _cruzRojaContext.Resources_Requests as IQueryable<Resources_Request>;

            //collection = collection.Where(
            //  a => a.EmergenciesDisasters.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);

            /*if (user.Roles.RoleName == "Encargado de Logistica")
             {
             }
             else
             {
                 return null;
             }*/

            return await FindAll()
                //.Include(i => i.Users)
                .Include(i => i.EmergenciesDisasters)
                //.ThenInclude(i => i.TypesEmergenciesDisasters)
                //.Include(i => i.EmergenciesDisasters.Alerts)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                //   .ThenInclude(i => i.TypeVehicles)
                // .ThenInclude(i => i.Vehicles)

                /*.ThenInclude(i => i.BrandsModels)
                .ThenInclude(i => i.Brands)
                .ThenInclude(i => i.BrandsModels)
                .ThenInclude(i => i.Model)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .Include(i => i.Users.Estates.LocationAddress)
                .Include(i => i.Users.Estates.Locations)
                .Include(i => i.Users.Locations)*/
                .ToListAsync();
        }


        public void CreateResource_Resquest(Resources_Request resources_Request)
        {
            Resources_Request rec = null;


            rec = db.Resources_Requests
                .Where(
                        a => a.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                        && a.FK_UserID == resources_Request.FK_UserID)
                       .FirstOrDefault();


            if (rec != null)
            {
                foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
                {
                    resources_Request.ID = rec.ID;
                    item.FK_Resource_RequestID = rec.ID;
                }
            }

            // Usuario existe entonces puedo actualizar y crear añadir nuevos recursos a la solicitud
            foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                var re = recurso(resources_Request, item);

                //No existe el recurso lo creo
                if (re == null && rec != null)
                {

                    spaceCamelCase(resources_Request);

                    //añado el nuevo item
                    db.Add(item);

                    Stock(resources_Request, item);

                    DeleteResource(resources_Request);

                    SaveAsync();
                }

                //Actualizando recursos - existe la solicitud a esa Emegrnecia de un Usuario especifico
                else if (re != null && rec != null)
                {
                    resources_Request.Reason = rec.Reason;

                    resources_Request.FK_UserID = rec.FK_UserID;

                    resources_Request.Status = rec.Status;

                    resources_Request.FK_EmergencyDisasterID = rec.FK_EmergencyDisasterID;

                    spaceCamelCase(rec);

                    DeleteResource(rec);

                    UpdateResources(resources_Request);

                    DeleteResource(rec);

                    Update(rec);

                    SaveAsync();

                }
            }


            //cuando no exite ningun registro de solicitud se procede a crearla completa
            if (rec == null)
            {
                spaceCamelCase(resources_Request);

                Create(resources_Request);

                UpdateResource_Resquest2(resources_Request);

                DeleteResource(resources_Request);

                SaveAsync();
            }

        }


        private void spaceCamelCase(Resources_Request resources_Request)
        {
            //Falta implementarlos en el PATCH
            if (resources_Request.Reason != null)
            {
                resources_Request.Reason = WithoutSpace_CamelCase.GetCamelCase(resources_Request.Reason);
            }
        }



        //Actualiza los recursos en la solicitud existente y mantengo actualizo el Stock
        public void UpdateResources(Resources_Request resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;

            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                //borrar
                var rec = recurso(resources_Request, resources);

                if (resources.FK_MaterialID != null && rec != null)
                {
                    materials = db.Materials
                        .Where(
                         a => a.ID == resources.FK_MaterialID)
                        .FirstOrDefault();

                    resources.Materials = materials;
                }

                if (resources.FK_MedicineID != null && rec != null)
                {
                    medicines = db.Medicines
                    .Where(
                     a => a.ID == resources.FK_MedicineID)
                    .FirstOrDefault();

                    resources.Medicines = medicines;
                }

                if (resources.FK_VehicleID != null && rec != null)
                {
                    vehicles = db.Vehicles
                   .Where(
                    a => a.ID == resources.FK_VehicleID)
                   .FirstOrDefault();

                    resources.Vehicles = vehicles;
                }


                if (resources.FK_MaterialID != null && rec != null)
                {
                    materials.MaterialQuantity = (materials.MaterialQuantity - resources.Quantity);

                    resources.ID = rec.ID;

                    rec.Quantity = resources.Quantity + rec.Quantity;


                    if (resources.Materials.MaterialQuantity == 0)
                    {
                        materials.MaterialAvailability = false;
                    }


                    MaterialsRepository.status(resources.Materials);
                }

                if (resources.FK_MedicineID != null && rec != null)
                {
                    resources.Medicines.MedicineQuantity = (medicines.MedicineQuantity - resources.Quantity);

                    resources.ID = rec.ID;

                    rec.Quantity = resources.Quantity + rec.Quantity;


                    if (resources.Medicines.MedicineQuantity == 0)
                    {
                        medicines.MedicineAvailability = false;
                    }

                    MedicinesRepository.status(resources.Medicines);


                }


                if (resources.Vehicles != null && rec != null)
                {
                    resources.ID = rec.ID;

                    vehicles.VehicleAvailability = false;

                    VehiclesRepository.status(resources.Vehicles);
                }

            }

        }

        public async Task<Resources_Request> GetResourcesRequestByID(int resource)
        {
            return await FindByCondition(res => res.ID.Equals(resource))
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)
                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)
                .FirstOrDefaultAsync();


        }


        public static ICollection<Resources_RequestResources_Materials_Medicines_Vehicles> valorId()
        {

            ICollection<Resources_RequestResources_Materials_Medicines_Vehicles> recs =

              db.Resources_RequestResources_Materials_Medicines_Vehicles
                    .ToList();


            return recs;

        }




        //Reviso la exitencia de los recursos de la slicitud existente 
        public static Resources_RequestResources_Materials_Medicines_Vehicles recurso(Resources_Request resources_Request, Resources_RequestResources_Materials_Medicines_Vehicles _Resources_RequestResources_Materials_Medicines_Vehicles)
        {

            if (_Resources_RequestResources_Materials_Medicines_Vehicles.FK_MaterialID != null)
            {

                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                               .Where
                                    (
                                    a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                                    && a.FK_MaterialID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_MaterialID
                                    && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                                    && a.Resources_Request.FK_UserID == resources_Request.FK_UserID)
                                     .Include(a => a.Materials)
                                    .FirstOrDefault();
            }
            else

                if (_Resources_RequestResources_Materials_Medicines_Vehicles.FK_MedicineID != null)
            {

                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                               .Where
                                    (

                                    a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                                    && a.FK_MedicineID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_MedicineID
                                    && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                                    && a.Resources_Request.FK_UserID == resources_Request.FK_UserID)
                                     .Include(a => a.Medicines)
                                    .FirstOrDefault();
            }
            else
            {
                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                 .Where
                      (

                      a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                      && a.FK_VehicleID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_VehicleID
                      && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                      && a.Resources_Request.FK_UserID == resources_Request.FK_UserID)
                       .Include(a => a.Vehicles)
                      .FirstOrDefault();
            }



            return recursos;
        }




        public void ActualizarEstado(Resources_Request resources_Request)
        {

            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resources_Request.Status == false)
                {

                    if (resources.FK_VehicleID == null && resources.FK_MedicineID == null && resources.FK_MaterialID != null)
                    {
                        resources.Materials.MaterialQuantity = resources.Materials.MaterialQuantity + resources.Quantity;

                        resources.Materials.MaterialAvailability = true;

                        _cruzRojaContext.Entry(resources.Materials).CurrentValues.SetValues(resources.Materials);
                    }


                    if (resources.FK_VehicleID == null && resources.FK_MaterialID == null && resources.FK_MedicineID != null)
                    {
                        resources.Medicines.MedicineQuantity = resources.Medicines.MedicineQuantity + resources.Quantity;
                        resources.Medicines.MedicineAvailability = true;
                        _cruzRojaContext.Entry(resources.Medicines).CurrentValues.SetValues(resources.Medicines);
                    }

                    if (resources.FK_MaterialID == null && resources.FK_MedicineID == null && resources.FK_VehicleID != null)
                    {
                        resources.Vehicles.VehicleAvailability = true;
                        //Entry le dice al DbContext que cambie el estado de la entidad en modificado
                        //setvalues realiza una copia del objeto pasado con los cambios
                        _cruzRojaContext.Entry(resources.Vehicles).CurrentValues.SetValues(resources.Vehicles);
                    }
                }
            }
        }




        //Crea la solicitud y actualizo Stock
        public void UpdateResource_Resquest2(Resources_Request resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;
            Resources_RequestResources_Materials_Medicines_Vehicles rec = null;


            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {


                //var rec = recurso(resources.FK_Resource_RequestID, resources);

                //Revisar Material si existe en la base de datos
                if (resources.FK_MaterialID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_MaterialID == resources.FK_MaterialID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources_Request.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID)
                        .FirstOrDefault();

                    if (rec == null)
                    {

                        materials = db.Materials
                            .Where(a => a.ID == resources.FK_MaterialID)
                            .FirstOrDefault();

                        resources.Materials = materials;
                    }
                        resources.Materials.MaterialQuantity = resources.Materials.MaterialQuantity - resources.Quantity;

                        if (resources.Materials.MaterialQuantity == 0)
                        {
                            resources.Materials.MaterialAvailability = false;
                        }

                        MaterialsRepository.status(resources.Materials);

                }


                if (resources.FK_MedicineID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_MedicineID == resources.FK_MedicineID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources_Request.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID)
                        .FirstOrDefault();

                    if (rec == null)
                    {
                        medicines = db.Medicines
                           .Where(a => a.ID == resources.FK_MedicineID)
                           .FirstOrDefault();

                        resources.Medicines = medicines;
                    }
                        resources.Medicines.MedicineQuantity = medicines.MedicineQuantity - resources.Quantity;


                        if (resources.Medicines.MedicineQuantity == 0)
                        {
                            resources.Medicines.MedicineAvailability = false;
                        }

                        MedicinesRepository.status(resources.Medicines);

                }

                if (resources.FK_VehicleID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_VehicleID == resources.FK_VehicleID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources_Request.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID)
                        .FirstOrDefault();

                    if (rec == null)
                    {
                        vehicles = db.Vehicles
                           .Where(a => a.ID == resources.FK_VehicleID)
                           .FirstOrDefault();

                        resources.Vehicles = vehicles;
                    }

                        if (resources.Vehicles != null)
                        {
                            resources.Vehicles.VehicleAvailability = false;

                            VehiclesRepository.status(resources.Vehicles);
                        }

                }



            }
        }

        public Resources_RequestResources_Materials_Medicines_Vehicles Stock(Resources_Request resources, Resources_RequestResources_Materials_Medicines_Vehicles resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;
            var db = new CruzRojaContext();
            Resources_RequestResources_Materials_Medicines_Vehicles rec = null;


            foreach (var resource in resources.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resource.FK_MaterialID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                    .Where(a => a.FK_MaterialID == resources_Request.FK_MaterialID
                            && a.FK_Resource_RequestID == resources.ID
                            && a.Resources_Request.FK_UserID == resources.FK_UserID
                            && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                    .FirstOrDefault();

                    if (rec == null)
                    {
                        {
                            materials = db.Materials
                                .Where(a => a.ID == resources_Request.FK_MaterialID)
                                .FirstOrDefault();

                            resources_Request.Materials = materials;

                            resources_Request.Materials.MaterialQuantity = resources_Request.Materials.MaterialQuantity - resources_Request.Quantity;

                            if (resources_Request.Materials.MaterialQuantity == 0)
                            {
                                resources_Request.Materials.MaterialAvailability = false;
                            }

                            MaterialsRepository.status(resources_Request.Materials);
                        }
                    }
                }



                if (resource.FK_MedicineID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_MedicineID == resources_Request.FK_MedicineID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                        .FirstOrDefault();


                    if (rec == null)
                    {
                        medicines = db.Medicines
                          .Where(a => a.ID == resources_Request.FK_MedicineID)
                          .FirstOrDefault();

                        resources_Request.Medicines = medicines;

                        resources_Request.Medicines.MedicineQuantity = medicines.MedicineQuantity - resources_Request.Quantity;


                        if (resources_Request.Medicines.MedicineQuantity == 0)
                        {
                            resources_Request.Medicines.MedicineAvailability = false;
                        }

                        MedicinesRepository.status(resources_Request.Medicines);
                    }
                }




                if (resource.FK_VehicleID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_VehicleID == resources_Request.FK_VehicleID
                                && a.FK_Resource_RequestID == resources_Request.ID
                                && a.Resources_Request.FK_UserID == resources.FK_UserID
                                && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                        .FirstOrDefault();

                    if (rec == null)
                    {
                        vehicles = db.Vehicles
                           .Where(a => a.ID == resources_Request.FK_VehicleID)
                           .FirstOrDefault();

                        resources_Request.Vehicles = vehicles;

                        if (resources_Request.Vehicles != null)
                        {
                            resources_Request.Vehicles.VehicleAvailability = false;

                            VehiclesRepository.status(resources_Request.Vehicles);
                        }

                
                    }
                }

            }

            return resources_Request;
        }




        public Resources_Request DeleteResource(Resources_Request resources_Request)
        {

            foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (item.Materials != null)
                {
                    item.Materials = null;
                }

                if (item.Medicines != null)
                {
                    item.Medicines = null;

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
