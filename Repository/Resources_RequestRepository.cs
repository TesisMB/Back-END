using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Resources_RequestRepository : RepositoryBase<ResourcesRequest>, IResources_RequestRepository
    {
        private readonly CruzRojaContext _cruzRojaContext = new CruzRojaContext();
        public readonly static CruzRojaContext db = new CruzRojaContext();
        public  static ResourcesRequestMaterialsMedicinesVehicles recursos = null;
        public ResourcesRequest userReq = null;
        private Users user = null;

        public Resources_RequestRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }


        public async Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest(int userId, string Condition, string state)
        {

            //var user = UsersRepository.authUser;

            //user = await EmployeesRepository.GetAllEmployeesById(userId);

            user = await _cruzRojaContext.Users
                .Where(a => a.UserID.Equals(userId))
                .Include(a => a.Roles)
                .Include(a => a.Estates)
                .ThenInclude(a => a.Locations)
                .FirstOrDefaultAsync();

            var collection = _cruzRojaContext.Resources_Requests as IQueryable<ResourcesRequest>;

            //Admin y C.General -> tiene acceso a todo en funcion del departamento

            if (Condition == "Todas")
            {
                collection = collection.Where(
                       a => a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID)
                       .AsNoTracking();

            }

            else if (user.Roles.RoleName == "Coord. General" && Condition != null)
            {
                return GetAllResourcesRequests(user.FK_EstateID, Condition);
            }

            else if (!String.IsNullOrEmpty(Condition) && state == "solicitud" && user.FK_RoleID != 4)
            {
                collection = collection.Where(
                            a => a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID
                            && a.CreatedBy == user.UserID)
                            .OrderByDescending(a => a.ID)
                            .Take(2)
                            .AsNoTracking();
            }
            else if (user.Roles.RoleName == "Enc. De logística")
            {
                //if (user.Roles.RoleName == "Admin" && Condition == null)
                
                    collection = collection.Where(
                                                                  a => a.Condition == Condition
                                                                 && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID)
                                                                .OrderByDescending(a => a.ID)
                                                                .Take(5)
                                                                .AsNoTracking();
            }

            // if (user.Roles.RoleName == "Coord. General" && Condition == null)
            //{
            //    return  GetAllResourcesRequests(user.FK_EstateID, Condition);
            //}


            //Encargado de logistica tiene acceso a las solicitudes pendientes nomas    

            //else if (user.Roles.RoleName == "Enc. De logística")
            //{

            //    collection = collection.Where(
            //                                  a => a.Condition == Condition 
            //                                 && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID)
            //                                .OrderByDescending(a => a.ID)
            //                                .Take(5)
            //                                .AsNoTracking();
            //    }

            //C.Emergencias tiene acceso a solamente el historial de solicitudes



            else if (Condition == "Aceptada" || Condition == "Pendiente" | Condition == "Rechazada")
            {
                collection = collection.Where(
                                            a => a.Condition == Condition
                                            && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID)
                                            .AsNoTracking();
            }
            

            return  collection
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.LocationsEmergenciesDisasters)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
            
                 .ThenInclude(i => i.TypeVehicles)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Brands)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Model)


                .Include(i => i.EmployeeCreated)
                .Include(i => i.EmployeeCreated.Users.Persons)
                .Include(i => i.EmployeeCreated.Users.Roles)
                .Include(i => i.EmployeeCreated.Users.Estates)
                .Include(i => i.EmployeeCreated.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeCreated.Users.Estates.Locations)


                .Include(i => i.EmployeeResponse)
                .Include(i => i.EmployeeResponse.Users.Persons)
                .Include(i => i.EmployeeResponse.Users.Roles)
                .Include(i => i.EmployeeResponse.Users.Estates)
                .Include(i => i.EmployeeResponse.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeResponse.Users.Estates.Locations)
                .ToList();
        }


        public async Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequestPDF(int id, string Condition, int emergency,
                                                                                   DateTime dateConvert,
                                                                                   DateTime dateConvertEnd)
        {

            user = EmployeesRepository.GetAllEmployeesById(id);


            var collection = _cruzRojaContext.Resources_Requests as IQueryable<ResourcesRequest>;

            var dateEnd = Convert.ToDateTime("01/01/0001");

           user = EmployeesRepository.GetAllEmployeesById(id);

            if (id != 0 && emergency == 0 && dateConvertEnd == dateEnd)
            {

                    collection = collection.Where(
                                                      a => a.Condition == Condition
                                                     && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID
                                                     && a.RequestDate >= dateConvert)
                                                     .AsNoTracking();

            }
            else if (id == 0 && emergency != 0 && dateConvertEnd == dateEnd)
            {
                collection = collection.Where(
                                                      a => a.Condition == Condition
                                                     && a.EmergenciesDisasters.EmergencyDisasterID == emergency
                                                      && a.RequestDate >= dateConvert)
                                                     .AsNoTracking();
            }

            else if(id != 0 && emergency != 0 && dateConvertEnd == dateEnd)
            {
                collection = collection.Where(
                                                      a => a.Condition == Condition
                                                               && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID

                                                              //&& a.CreatedBy == id
                                                              && a.EmergenciesDisasters.EmergencyDisasterID == emergency
                                                              && a.RequestDate >= dateConvert)
                                                     .AsNoTracking();
            }

            else if (id != 0 && emergency == 0 && dateConvertEnd != dateEnd)
            {
                user = EmployeesRepository.GetAllEmployeesById(id);

                collection = collection.Where(
                                                  a => a.Condition == Condition
                                               && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID

                                                  //  && a.CreatedBy == user.UserID
                                                 && a.RequestDate >= dateConvert
                                                 && a.RequestDate <= dateConvertEnd)
                                                 .AsNoTracking();

            }
            else if (id == 0 && emergency != 0 && dateConvertEnd != dateEnd)
            {
                collection = collection.Where(
                                                      a => a.Condition == Condition
                                                      && a.EmergenciesDisasters.EmergencyDisasterID == emergency
                                                      && a.RequestDate >= dateConvert
                                                      && a.RequestDate <= dateConvertEnd)
                                                     .AsNoTracking();
            }

            else if (id != 0 && emergency != 0 && dateConvertEnd != dateEnd)
            {
                collection = collection.Where(
                                                      a => a.Condition == Condition
                                                               && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID

                                                              // && a.CreatedBy == id
                                                              && a.EmergenciesDisasters.EmergencyDisasterID == emergency
                                                              && a.RequestDate >= dateConvert
                                                              && a.RequestDate <= dateConvertEnd)
                                                     .AsNoTracking();
            }



            return await collection
                          .Include(i => i.EmergenciesDisasters)
                          .ThenInclude(i => i.TypesEmergenciesDisasters)
                          .Include(i => i.EmergenciesDisasters.LocationsEmergenciesDisasters)

                          .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .ThenInclude(i => i.Materials)

                           .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .ThenInclude(i => i.Medicines)

                          .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .ThenInclude(i => i.Vehicles)

                           .ThenInclude(i => i.TypeVehicles)

                          .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .ThenInclude(i => i.Vehicles)
                          .ThenInclude(i => i.Brands)

                          .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .ThenInclude(i => i.Vehicles)
                          .ThenInclude(i => i.Model)


                          .Include(i => i.EmployeeCreated)
                          .Include(i => i.EmployeeCreated.Users.Persons)
                          .Include(i => i.EmployeeCreated.Users.Roles)
                          .Include(i => i.EmployeeCreated.Users.Estates)
                          .Include(i => i.EmployeeCreated.Users.Estates.LocationAddress)
                          .Include(i => i.EmployeeCreated.Users.Estates.Locations)


                          .Include(i => i.EmployeeResponse)
                          .Include(i => i.EmployeeResponse.Users.Persons)
                          .Include(i => i.EmployeeResponse.Users.Roles)
                          .Include(i => i.EmployeeResponse.Users.Estates)
                          .Include(i => i.EmployeeResponse.Users.Estates.LocationAddress)
                          .Include(i => i.EmployeeResponse.Users.Estates.Locations)
                          .ToListAsync();
        }
        public async Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest(int userId, string Condition)
        {

            //var user = UsersRepository.authUser;
            user =  EmployeesRepository.GetAllEmployeesById(userId);

          

            var collection = _cruzRojaContext.Resources_Requests as IQueryable<ResourcesRequest>;

            //Admin y C.General -> tiene acceso a todo en funcion del departamento
           
            if (user.Roles.RoleName == "Admin")
                {
                    return GetAllResourcesRequests(user.FK_EstateID, Condition);
                }

                else if (user.Roles.RoleName == "Coord. General")
                {
                    return GetAllResourcesRequests(user.FK_EstateID, Condition);
                }


                //Encargado de logistica tiene acceso a las solicitudes pendientes nomas    

                else if (user.Roles.RoleName == "Enc. De logística")
                {

                    collection = collection.Where(
                                                  a => a.Condition == Condition
                                                 && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID)
                                                 .AsNoTracking();
                }

                //C.Emergencias tiene acceso a solamente el historial de solicitudes
                else
                {
                    collection = collection.Where(
                                                a => a.Condition == Condition
                                                && a.EmergenciesDisasters.FK_EstateID == user.FK_EstateID
                                                && a.CreatedBy == user.UserID)
                                                .AsNoTracking();
                 }


            return collection
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.LocationsEmergenciesDisasters)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)

                 .ThenInclude(i => i.TypeVehicles)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Brands)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Model)


                .Include(i => i.EmployeeCreated)
                .Include(i => i.EmployeeCreated.Users.Persons)
                .Include(i => i.EmployeeCreated.Users.Roles)
                .Include(i => i.EmployeeCreated.Users.Estates)
                .Include(i => i.EmployeeCreated.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeCreated.Users.Estates.Locations)


                .Include(i => i.EmployeeResponse)
                .Include(i => i.EmployeeResponse.Users.Persons)
                .Include(i => i.EmployeeResponse.Users.Roles)
                .Include(i => i.EmployeeResponse.Users.Estates)
                .Include(i => i.EmployeeResponse.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeResponse.Users.Estates.Locations)
                .ToList();
        }



        public  IEnumerable<ResourcesRequest> GetAllResourcesRequests(int fK_EstateID, string Condition)
        {
            var collection = _cruzRojaContext.Resources_Requests as IQueryable<ResourcesRequest>;

            collection = collection.Where(a => a.EmergenciesDisasters.FK_EstateID == fK_EstateID
                                        && a.Condition == Condition)
                                    .AsNoTracking();

            return  collection
                .Include(i => i.EmployeeCreated)
                .Include(i => i.EmergenciesDisasters)
                .ThenInclude(i => i.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.LocationsEmergenciesDisasters)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Medicines)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)

                 .ThenInclude(i => i.TypeVehicles)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Brands)

                .Include(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ThenInclude(i => i.Model)


                .Include(i => i.EmployeeCreated)
                .Include(i => i.EmployeeCreated.Users.Persons)
                .Include(i => i.EmployeeCreated.Users.Roles)
                .Include(i => i.EmployeeCreated.Users.Estates)
                .Include(i => i.EmployeeCreated.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeCreated.Users.Estates.Locations)


                 .Include(i => i.EmployeeModified)
                .Include(i => i.EmployeeModified.Users.Persons)
                .Include(i => i.EmployeeModified.Users.Roles)
                .Include(i => i.EmployeeModified.Users.Estates)
                .Include(i => i.EmployeeModified.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeModified.Users.Estates.Locations)


                .Include(i => i.EmployeeResponse)
                .Include(i => i.EmployeeResponse.Users.Persons)
                .Include(i => i.EmployeeResponse.Users.Roles)
                .Include(i => i.EmployeeResponse.Users.Estates)
                .Include(i => i.EmployeeResponse.Users.Estates.LocationAddress)
                .Include(i => i.EmployeeResponse.Users.Estates.Locations)
                .ToList();
        }


        public void CreateResource_Resquest(ResourcesRequest resources_Request)
        {

            ResourcesRequest rec = null;
            var user = EmployeesRepository.GetAllEmployeesById(resources_Request.CreatedBy);


            //Traigo todos los encargados de Logistica para enviarles un email
            //con la solicitudes nuevas o alguna actualizacion 

            List<Users> logsitica = new List<Users>();

            logsitica = db.Users
                .Where(
                        a => a.FK_RoleID == 4
                        && a.FK_EstateID == 20)
                        .Include(a => a.Persons)
                        .Include(a => a.Employees)
                .ToList();


            //var userRequest = ResourcesRequestForCreationDto.UserRequest;

            rec = db.Resources_Requests
                .Where(
                        a => a.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                        && a.CreatedBy == resources_Request.CreatedBy
                        && a.Condition == "Pendiente")
                        .Include(a => a.Resources_RequestResources_Materials_Medicines_Vehicles)
                       .FirstOrDefault();


                if(rec != null)
                {
                    foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {
                        resources_Request.ID = rec.ID;
                        item.FK_Resource_RequestID = rec.ID;
                    }
            }
            else
            {
                if(userReq != null)
                {
                    foreach (var item in userReq.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {
                        resources_Request.ID = userReq.ID;
                        item.FK_Resource_RequestID = userReq.ID;
                    }
                }
            }

            var estado = "";
            var estadoNuevo = "";

            // Usuario existe entonces puedo actualizar y  añadir nuevos recursos a la solicitud
            foreach (var item in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                item.Estado = "Nuevo";
                estado = "Nuevo";
                var re = Recurso(resources_Request, item);

                if (re == null && rec != null)
                {
                    if (resources_Request.Description != null)
                    {
                        rec.Description = resources_Request.Description;
                    }
                    else
                    {
                        resources_Request.Description = rec.Description;
                    }

                    //SpaceCamelCase(resources_Request);

                    resources_Request.RequestDateModified = DateTime.Now;

                    //añado el nuevo item
                    AddRecurso(item);

                    Stock(resources_Request, item);

                    DeleteResource(resources_Request);

                    Update(resources_Request);

                    SaveAsync();
                }

                //Actualizando recursos - existe la solicitud a esa Emegrnecia de un Usuario especifico
                else if (re != null && rec != null)
                {
                    item.Estado = "Actualizar";
                    estadoNuevo = "Actualizar";

                    if (resources_Request.Description != null)
                    {
                         rec.Description = resources_Request.Description;
                    }

                    resources_Request.RequestDateModified = rec.RequestDateModified;


                    resources_Request.CreatedBy = rec.CreatedBy;

                    resources_Request.Status = rec.Status;

                    resources_Request.FK_EmergencyDisasterID = rec.FK_EmergencyDisasterID;

                    SpaceCamelCase(rec);

                    DeleteResource(rec);

                    UpdateResources(item, resources_Request);

                    DeleteResource(rec);

                    sendEmailUpdateResourcesRequest(resources_Request);

                    Update(rec);

                    SaveAsync();

                }
            }

            //Envio de mail
            if (estadoNuevo.Equals("Actualizar") && estado.Equals("Nuevo"))
            {
                sendEmailUpdateResourcesRequest(resources_Request);
            }

            //cuando no exite ningun registro de solicitud se procede a crearla completa
            if (rec == null)
                {
                SpaceCamelCase(resources_Request);
                Create(resources_Request);
                UpdateResource_Resquest2(resources_Request);
                DeleteResource(resources_Request);

                foreach (var item in logsitica)
                {
                    sendResourcesRequest(resources_Request, item);
                }

                SaveAsync();
            }
        }




        private void SpaceCamelCase(ResourcesRequest resources_Request)
        {
            //Falta implementarlos en el PATCH
            if (resources_Request.Reason != null)
            {
                resources_Request.Reason = WithoutSpace_CamelCase.GetWithoutSpace(resources_Request.Reason);
            }
        }



        public void AddRecurso(ResourcesRequestMaterialsMedicinesVehicles res)
        {
            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            cruzRojaContext.Add(res);

            cruzRojaContext.SaveChanges();
        }

        //Actualiza los recursos en la solicitud existente y mantengo actualizo el Stock
        public void UpdateResources(ResourcesRequestMaterialsMedicinesVehicles res, ResourcesRequest resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;

                //borrar
                var rec = Recurso(resources_Request, res);

                if (res.FK_MaterialID != null && rec != null)
                {
                    materials = db.Materials
                        .Where(
                         a => a.ID == res.FK_MaterialID)
                        .FirstOrDefault();

                    res.Materials = materials;
                }

                if (res.FK_MedicineID != null && rec != null)
                {
                    medicines = db.Medicines
                    .Where(
                     a => a.ID == res.FK_MedicineID)
                    .FirstOrDefault();

                    res.Medicines = medicines;
                }

                if (res.FK_VehicleID != null && rec != null)
                {
                    vehicles = db.Vehicles
                   .Where(
                    a => a.ID == res.FK_VehicleID)
                   .FirstOrDefault();

                    res.Vehicles = vehicles;
                }


                if (res.FK_MaterialID != null && rec != null && materials.MaterialQuantity > 0)
                {

                    materials.MaterialQuantity -= res.Quantity;

                    res.ID = rec.ID;

                    rec.Quantity = res.Quantity + rec.Quantity;


                    if (res.Materials.MaterialQuantity == 0)
                    {
                        materials.MaterialAvailability = false;
                    }


                    MaterialsRepository.status(res.Materials);
                }

                if (res.FK_MedicineID != null && rec != null && medicines.MedicineQuantity > 0)
                {
                    res.Medicines.MedicineQuantity = (medicines.MedicineQuantity - res.Quantity);

                    res.ID = rec.ID;

                    rec.Quantity = res.Quantity + rec.Quantity;


                    if (res.Medicines.MedicineQuantity == 0)
                    {
                        medicines.MedicineAvailability = false;
                    }

                    MedicinesRepository.status(res.Medicines);
                }


               if (res.FK_VehicleID != null)
                {
                    res.ID = rec.ID;

                    vehicles.VehicleAvailability = false;

                    VehiclesRepository.status(res.Vehicles);
                }

        }

        public async Task<ResourcesRequest> GetResourcesRequestByID(int resource)
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


        //Reviso la exitencia de los recursos de la slicitud existente 
        public static ResourcesRequestMaterialsMedicinesVehicles Recurso(ResourcesRequest resources_Request, ResourcesRequestMaterialsMedicinesVehicles _Resources_RequestResources_Materials_Medicines_Vehicles)
        {

            if (_Resources_RequestResources_Materials_Medicines_Vehicles.FK_MaterialID != null)
            {

                recursos = db.Resources_RequestResources_Materials_Medicines_Vehicles
                               .Where
                                    (
                                    a => a.FK_Resource_RequestID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_Resource_RequestID
                                    && a.FK_MaterialID == _Resources_RequestResources_Materials_Medicines_Vehicles.FK_MaterialID
                                    && a.Resources_Request.FK_EmergencyDisasterID == resources_Request.FK_EmergencyDisasterID
                                    && a.Resources_Request.CreatedBy == resources_Request.CreatedBy)
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
                                    && a.Resources_Request.CreatedBy == resources_Request.CreatedBy)
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
                      && a.Resources_Request.CreatedBy == resources_Request.CreatedBy)
                       .Include(a => a.Vehicles)
                      .FirstOrDefault();
            }



            return recursos;
        }




        public ResourcesRequest ActualizarEstado(ResourcesRequest resources_Request)
        {

            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resources_Request.Status == false)
                {

                    if (resources.FK_VehicleID == null && resources.FK_MedicineID == null && resources.FK_MaterialID != null)
                    {
                        Materials materials = null;

                        materials = db.Materials
                                    .Where(a => a.ID == resources.FK_MaterialID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                        materials.MaterialQuantity += resources.Quantity;

                        materials.MaterialAvailability = true;

                        MaterialsRepository.status(materials);
                    }


                    if (resources.FK_VehicleID == null && resources.FK_MaterialID == null && resources.FK_MedicineID != null)
                    {

                        Medicines medicines = null;

                        medicines = db.Medicines
                                    .Where(a => a.ID == resources.FK_MedicineID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                        medicines.MedicineQuantity += resources.Quantity;
                        medicines.MedicineAvailability = true;

                        MedicinesRepository.status(medicines);
                    }

                    if (resources.FK_MaterialID == null && resources.FK_MedicineID == null && resources.FK_VehicleID != null)
                    {

                        Vehicles vehicles = null;

                        vehicles = db.Vehicles
                                    .Where(a => a.ID == resources.FK_VehicleID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                        vehicles.VehicleAvailability = true;
                        //Entry le dice al DbContext que cambie el estado de la entidad en modificado
                        //setvalues realiza una copia del objeto pasado con los cambios
                        VehiclesRepository.status(vehicles);
                    }
                }
            }

            return resources_Request;
        }




        //Crea la solicitud y actualizo Stock
        public void UpdateResource_Resquest2(ResourcesRequest resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;


            foreach (var resources in resources_Request.Resources_RequestResources_Materials_Medicines_Vehicles)
            {

                //Revisar Material si existe en la base de datos
                if (resources.FK_MaterialID != null)
                {
                                        materials = db.Materials
                                            .Where(a => a.ID == resources.FK_MaterialID)
                                            .AsNoTracking()
                                            .FirstOrDefault();

                           
                                        materials.MaterialQuantity -= resources.Quantity;


                                        if (materials.MaterialQuantity == 0)
                                        {
                                            materials.MaterialAvailability = false;
                                              sendEmailMaterial(materials);
                                        }

                                        MaterialsRepository.status(materials);
                 }



                    else if (resources.FK_MedicineID != null)
                    {


                    medicines = db.Medicines
                                  .Where(a => a.ID == resources.FK_MedicineID)
                                  .AsNoTracking()
                                  .FirstOrDefault();

                            medicines.MedicineQuantity -= resources.Quantity;


                            if (medicines.MedicineQuantity == 0)
                            {
                                medicines.MedicineAvailability = false;
                                sendEmailMedicine(medicines);
                    }

                    MedicinesRepository.status(medicines);

                    }

                                else if (resources.FK_VehicleID != null)
                                {
                                    vehicles = db.Vehicles
                                                .Where(a => a.ID == resources.FK_VehicleID)
                                                .AsNoTracking()
                                                .FirstOrDefault();


                                        vehicles.VehicleAvailability = false;

                                        resources.Quantity = 1;

                                        VehiclesRepository.status(vehicles);

                                        sendEmailVehicle(vehicles);
                                }
            }
        }


        //Actualizacion de recursos (suma)
        public ResourcesRequestMaterialsMedicinesVehicles Stock(ResourcesRequest resources, ResourcesRequestMaterialsMedicinesVehicles resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;
            var db = new CruzRojaContext();
            ResourcesRequestMaterialsMedicinesVehicles rec = null;


            foreach (var resource in resources.Resources_RequestResources_Materials_Medicines_Vehicles)
            {
                if (resource.FK_MaterialID != null)
                {
                    rec = db.Resources_RequestResources_Materials_Medicines_Vehicles
                    .Where(a => a.FK_MaterialID == resources_Request.FK_MaterialID
                            && a.FK_Resource_RequestID == resources.ID
                            && a.Resources_Request.CreatedBy == resources.CreatedBy
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
                                && a.Resources_Request.CreatedBy == resources.CreatedBy
                                && a.Resources_Request.FK_EmergencyDisasterID == resources.FK_EmergencyDisasterID)
                        .FirstOrDefault();


                    if (rec == null)
                    {
                        medicines = db.Medicines
                          .Where(a => a.ID == resource.FK_MedicineID)
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
                                && a.Resources_Request.CreatedBy == resources.CreatedBy
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

                            resources_Request.Quantity = 1;

                            VehiclesRepository.status(resources_Request.Vehicles);
                        }
                    }
                }
            }

            return resources_Request;
        }


        public ResourcesRequest DeleteResource(ResourcesRequest resources_Request)
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

        public ResourcesRequest UpdateStockDelete(ResourcesRequest resource)
        {
           var update =  ActualizarEstado(resource);

            return update;
        }

        public void AcceptRejectRequest(ResourcesRequest resourcesRequest, int userRequestID)
        {
            userReq = _cruzRojaContext.Resources_Requests
                  .Where(
                          a => a.FK_EmergencyDisasterID == resourcesRequest.FK_EmergencyDisasterID
                          && a.CreatedBy == userRequestID
                          && a.Condition == "Pendiente")
                          .Include(a => a.Resources_RequestResources_Materials_Medicines_Vehicles)
                          .Include(a => a.EmergenciesDisasters)
                          .ThenInclude(a => a.TypesEmergenciesDisasters)
                          .Include(a => a.EmergenciesDisasters)
                          .ThenInclude(a => a.LocationsEmergenciesDisasters)
                          .AsNoTracking()
                         .FirstOrDefault();


            userReq.RequestDateModified = DateTime.Now;
            userReq.AnsweredBy = resourcesRequest.AnsweredBy;
            userReq.Status = resourcesRequest.Status;

            Users user = new Users();
            user = db.Users
                .Where(a => a.UserID == userRequestID)
                .Include(a => a.Persons)
                .FirstOrDefault();


            if (userReq != null)
            {

                if (resourcesRequest.Status == false)
                {
                    ActualizarEstado(userReq);
                    DeleteResource(userReq);

                    userReq.Condition = "Rechazada";
                    userReq.Reason = resourcesRequest.Reason;
                    sendAcceptRejectRequest(user, userReq.Condition, userReq);
                }

                else
                {
                    userReq.Condition = "Aceptada";
                    userReq.Reason = resourcesRequest.Reason;
                    sendAcceptRejectRequest(user, userReq.Condition, userReq);
                }

                //userReq.FK_InCharge = UsersRepository.authUser.UserID;

                Update(userReq);
                SaveAsync();
            }
        }



        public static void sendAcceptRejectRequest(Users user, string condition, ResourcesRequest resourcesRequest)
        {
            string message;
            var sb = new StringBuilder();

            sb.Append($@"  <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>
                                            <p style='margin-left: 20px;'>La solicitud {resourcesRequest.ID} realizada para la emergencia en {resourcesRequest.EmergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName} - {resourcesRequest.EmergenciesDisasters.TypesEmergenciesDisasters.TypeEmergencyDisasterName}, fue
                                                {condition}.
                                            </p>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos de los recursos: 
                                    </p>
                ");

            string materiales = "";
            string medicamentos = "";
            string vehiculos = "";
            string reason = "";

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            var emergency = cruzRojaContext.EmergenciesDisasters.Where(a => a.EmergencyDisasterID.Equals(resourcesRequest.FK_EmergencyDisasterID))
                                            .Include(a => a.TypesEmergenciesDisasters)
                                            .Include(a => a.LocationsEmergenciesDisasters)
                                            .FirstOrDefault();

            var mat = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MaterialID != null);
            var med = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MedicineID != null);
            var veh = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null);

            if (resourcesRequest.Description != null)
            {
                reason = $@"
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                       Razón: {resourcesRequest.Reason}     
                                     </p>";
            }


            if (mat.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Materiales</h6>
                               <table style='border-collapse: collapse; margin-left: 20px;'>
                                   <tr style='padding: 8px;'>
                                       <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Marca</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad solicitada</th>
                                   </tr>
            ");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MaterialID != null))
            {
                var mater = cruzRojaContext.Materials.Where(a => a.ID.Equals(item.FK_MaterialID))
                     .FirstOrDefault();

                sb.Append($@"<tr style='border: 1px solid #000; padding: 8px;'>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialName}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialBrand}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                             </tr>"
                                       );
            }

            if (med.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }


            if (med.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Medicamentos</h6>
                                    <table style='border-collapse: collapse; margin-left: 20px;'>
                                        <tr style='padding: 8px;'>
                                            <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Farmaco</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Laboratorio</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Peso/Volumen</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad solicitada</th>
                                        </tr>");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MedicineID != null))
            {
                var medic = cruzRojaContext.Medicines.Where(a => a.ID.Equals(item.FK_MedicineID))
                              .FirstOrDefault();

                sb.Append($@"
                                           <tr style='border: 1px solid #000; padding: 8px;'>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineName}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineDrug}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineLab}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineWeight} {medic.MedicineUnits}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                                  </tr>
                ");
            }
            if (med.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }


            if (veh.Count() > 0)
            {
                sb.Append($@"
                                 <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Vehiculos</h6>
                                <table style='border-collapse: collapse; margin-left: 20px;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Patente</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Nombre</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Tipo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Año</th>
                                    </tr>
            ");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null))
            {


                var vehicles = cruzRojaContext.Vehicles.Where(a => a.ID.Equals(item.FK_VehicleID))
                               .Include(a => a.Brands)
                               .Include(a => a.Model)
                               .Include(a => a.TypeVehicles)
                               .FirstOrDefault();
                sb.Append($@"
                                   <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName} {vehicles.Model.ModelName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.TypeVehicles.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>
                                     ");
            }



            if (veh.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }



            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null))
            {


                var vehicles = cruzRojaContext.Vehicles.Where(a => a.ID.Equals(item.FK_VehicleID))
                               .Include(a => a.Brands)
                               .Include(a => a.Model)
                               .Include(a => a.TypeVehicles)
                               .FirstOrDefault();

                vehiculos = $@"
                               <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Vehiculos</h6>
                                <table style='border-collapse: collapse; margin-left: 20px;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Patente</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Nombre</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Tipo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Año</th>
                                    </tr>

                                   <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName} {vehicles.Model.ModelName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.TypeVehicles.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>
                                        </table>
                    ";

            }

            sb.Append(@$"
                                {reason}

                                            <a style='color: white;
                                                text-align: center;
                                            display: block;
                                                background: rgb(189, 45, 45);
                                            text-decoration: none;
                                                border-radius: 0.4rem;
                                                 width: 33%;
                                                 margin-top: 2rem;
                                                margin-bottom: 2rem;
                                                padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>


                                             <p style='margin-top: 2rem; margin-left: 20px;'>
                                                    Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                                </p>
                                                <p style='margin-top: 2rem; margin-left: 20px;'>
                                                    Gracias.
                                                </p>
                                                <p style='margin-left: 20px;'>
                                                    El equipo de SICREYD.
                                                </p>
                                        </div>
            ");
                                            

                                              



        var msg = new Mail(new string[] { user.Persons.Email }, "Estado de la solicitud de recursos", $@"{sb.ToString()}");

            EmailSender.SendEmail(msg);

        }


        //Envio mail (Nueva solicitud)
        public static void sendResourcesRequest(ResourcesRequest resourcesRequest, Users user2)
        {
            string message;
            string materiales = "";
            string medicamentos = "";
            string vehiculos = "";
            string reason = "";

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            var emergency = cruzRojaContext.EmergenciesDisasters.Where(a => a.EmergencyDisasterID.Equals(resourcesRequest.FK_EmergencyDisasterID))
                                            .Include(a => a.TypesEmergenciesDisasters)
                                            .Include(a => a.LocationsEmergenciesDisasters)
                                            .FirstOrDefault();

            var mat = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MaterialID != null);
            var med = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MedicineID != null);
            var veh = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null);

            if (resourcesRequest.Description != null)
            {
                reason = $@"
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                       Razón de la solicitud: {resourcesRequest.Description}     
                                     </p>";
            }


            var sb = new StringBuilder();
            sb.Append($@" 
                               <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style = 'margin-left: 20px;'> Se realizo una nueva solicitud para la alerta
                                                #{emergency.EmergencyDisasterID}
                                                { emergency.LocationsEmergenciesDisasters.LocationCityName}
                                    </p>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>
                ");

            if (mat.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Materiales</h6>
                               <table style='border-collapse: collapse; margin-left: 20px;'>
                                   <tr style='padding: 8px;'>
                                       <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Marca</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad solicitada</th>
                                   </tr>
            ");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MaterialID != null))
            {
                var mater = cruzRojaContext.Materials.Where(a => a.ID.Equals(item.FK_MaterialID))
                     .FirstOrDefault();

                sb.Append($@"<tr style='border: 1px solid #000; padding: 8px;'>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialName}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialBrand}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                             </tr>"
                                       );
            }

            if (med.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }


            if (med.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Medicamentos</h6>
                                    <table style='border-collapse: collapse; margin-left: 20px;'>
                                        <tr style='padding: 8px;'>
                                            <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Farmaco</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Laboratorio</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Peso/Volumen</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad solicitada</th>
                                        </tr>");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_MedicineID != null))
            {
                var medic = cruzRojaContext.Medicines.Where(a => a.ID.Equals(item.FK_MedicineID))
                              .FirstOrDefault();

                sb.Append($@"
                                           <tr style='border: 1px solid #000; padding: 8px;'>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineName}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineDrug}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineLab}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineWeight} {medic.MedicineUnits}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                                  </tr>
                ");
            }
            if (med.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }


            if (veh.Count() > 0)
            {
                sb.Append($@"
                                 <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Vehiculos</h6>
                                <table style='border-collapse: collapse; margin-left: 20px;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Patente</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Nombre</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Tipo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Año</th>
                                    </tr>
            ");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null))
            {


                var vehicles = cruzRojaContext.Vehicles.Where(a => a.ID.Equals(item.FK_VehicleID))
                               .Include(a => a.Brands)
                               .Include(a => a.Model)
                               .Include(a => a.TypeVehicles)
                               .FirstOrDefault();
                sb.Append($@"
                                   <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName} {vehicles.Model.ModelName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.TypeVehicles.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>
                                     ");
            }



            if (veh.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }



            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null))
            {


                var vehicles = cruzRojaContext.Vehicles.Where(a => a.ID.Equals(item.FK_VehicleID))
                               .Include(a => a.Brands)
                               .Include(a => a.Model)
                               .Include(a => a.TypeVehicles)
                               .FirstOrDefault();

                vehiculos = $@"
                               <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Vehiculos</h6>
                                <table style='border-collapse: collapse; margin-left: 20px;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Patente</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Nombre</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Tipo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Año</th>
                                    </tr>

                                   <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName} {vehicles.Model.ModelName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.TypeVehicles.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>
                                        </table>
                    ";

            }

            sb.Append($@"
                                {reason}

                                 <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>
                                    
        
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                    </p>
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>
");







            var msg = new Mail(new string[] { user2.Persons.Email }, "Solicitud de recursos", $@"{sb.ToString()}");

            EmailSender.SendEmail(msg);

            //Email.Send(
            //    to: user2.Persons.Email,
            //    subject: "Solicitud de recursos",
            //    html: $@"{message}"
            //    );
        }


        //Envio mail (Actualizacion de solicitud)
        public static void sendEmailUpdateResourcesRequest(ResourcesRequest resourcesRequest)
        {
            string message;
            string materiales = "";
            string medicamentos = "";
            string vehiculos = "";
            string reason = "";

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            var emergency = cruzRojaContext.EmergenciesDisasters.Where(a => a.EmergencyDisasterID.Equals(resourcesRequest.FK_EmergencyDisasterID))
                                            .Include(a => a.TypesEmergenciesDisasters)
                                            .Include(a => a.LocationsEmergenciesDisasters)
                                            .FirstOrDefault();

            var mat = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                            .Where(x => x.FK_MaterialID != null && x.Estado == "Nuevo");

            var matMod = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                            .Where(x => x.FK_MaterialID != null && x.Estado == "Actualizar");

            var medMod = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                  .Where(x => x.FK_MedicineID != null && x.Estado == "Actualizar");

            var med= resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                 .Where(x => x.FK_MedicineID != null && x.Estado == "Nuevo");

            var veh = resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null);

       


            if (resourcesRequest.Description != null)
            {
                reason = $@"
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                       Razón de la solicitud: {resourcesRequest.Description}     
                                     </p>";
            }


            var sb = new StringBuilder();
            sb.Append($@" 
                               <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style = 'margin-left: 20px;'> Se realizo una nueva actualización a la solicitud para la alerta
                                                #{emergency.EmergencyDisasterID}
                                                { emergency.LocationsEmergenciesDisasters.LocationCityName}
                                    </p>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos de los recursos solicitados: 
                                    </p>
                ");
            
            //Actualizaciones materiales
            if (matMod.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Materiales actualizados</h6>
                               <table style='border-collapse: collapse; margin-left: 20px;'>
                                   <tr style='padding: 8px;'>
                                       <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Marca</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad anterior solicitada</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad nueva solicitada</th>
                                   </tr>
            ");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                     .Where(x => x.FK_MaterialID != null && x.Estado == "Actualizar"))
            {
                var mater = cruzRojaContext.Materials.Where(a => a.ID.Equals(item.FK_MaterialID))
                     .AsNoTracking()
                     .FirstOrDefault();


                var me = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_Resource_RequestID == item.FK_Resource_RequestID
                        && a.FK_MaterialID == item.FK_MaterialID)
                        .Include(a => a.Materials)
                     .AsNoTracking()
                        .FirstOrDefault();

                var materials = cruzRojaContext.Materials.Where(a => a.ID.Equals(me.FK_MaterialID))
                            .FirstOrDefault();


                sb.Append($@"<tr style='border: 1px solid #000; padding: 8px;'>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialName}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialBrand}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{materials.MaterialQuantity}</td>
                                             </tr>"
                                       );
            }

            if (matMod.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }

            if (mat.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Materiales nuevos</h6>
                               <table style='border-collapse: collapse; margin-left: 20px;'>
                                   <tr style='padding: 8px;'>
                                       <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Marca</th>
                                       <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad solicitada</th>
                                   </tr>
            ");
            }

            //nuevos materiales
            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                    .Where(x => x.FK_MaterialID != null && x.Estado == "Nuevo"))
            {
                var mater = cruzRojaContext.Materials.Where(a => a.ID.Equals(item.FK_MaterialID))
                     .AsNoTracking()
                     .FirstOrDefault();


                sb.Append($@"<tr style='border: 1px solid #000; padding: 8px;'>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialName}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{mater.MaterialBrand}</td>
                                               <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                             </tr>"
                                       );
            }

            if (mat.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }



            //actualizaciones de medicamentos
            if (medMod.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Medicamentos actualizados</h6>
                                    <table style='border-collapse: collapse; margin-left: 20px;'>
                                        <tr style='padding: 8px;'>
                                            <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Farmaco</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Laboratorio</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Peso/Volumen</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad anterior solicitada</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad nueva solicitada</th>
                                        </tr>");
            }



            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                         .Where(x => x.FK_MedicineID != null && x.Estado.Equals("Actualizar")))
            {
                var medic = cruzRojaContext.Medicines.Where(a => a.ID.Equals(item.FK_MedicineID))
                              .FirstOrDefault();

                var me = db.Resources_RequestResources_Materials_Medicines_Vehicles
                        .Where(a => a.FK_Resource_RequestID == item.FK_Resource_RequestID
                        && a.FK_MedicineID == item.FK_MedicineID)
                        .Include(a => a.Medicines)
                        .AsNoTracking()
                        .FirstOrDefault();

                var medicam = cruzRojaContext.Medicines.Where(a => a.ID.Equals(me.FK_MedicineID))
                            .FirstOrDefault();

                sb.Append($@"
                                           <tr style='border: 1px solid #000; padding: 8px;'>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineName}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineDrug}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineLab}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineWeight} {medic.MedicineUnits}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{me.Medicines.MedicineQuantity}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                                  </tr>
                ");
            }

            if (medMod.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }




            if (med.Count() > 0)
            {

                sb.Append($@" <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Medicamentos nuevos</h6>
                                    <table style='border-collapse: collapse; margin-left: 20px;'>
                                        <tr style='padding: 8px;'>
                                            <th style='border: 1px solid #000; padding: 8px;'>Nombre</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Farmaco</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Laboratorio</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Peso/Volumen</th>
                                            <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Cantidad solicitada</th>
                                        </tr>");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                                        .Where(x => x.FK_MedicineID != null && x.Estado.Equals("Nuevo")))
            {
                var medic = cruzRojaContext.Medicines.Where(a => a.ID.Equals(item.FK_MedicineID))
                              .FirstOrDefault();

                sb.Append($@"
                                           <tr style='border: 1px solid #000; padding: 8px;'>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineName}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineDrug}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineLab}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{medic.MedicineWeight} {medic.MedicineUnits}</td>
                                                    <td style='border: 1px solid #000; padding: 8px;'>{item.Quantity}</td>
                                                  </tr>
                ");
            }
           
            if (med.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }


            if (veh.Count() > 0)
            {
                sb.Append($@"
                                 <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Vehiculos nuevos</h6>
                                <table style='border-collapse: collapse; margin-left: 20px;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Patente</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Nombre</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Tipo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Año</th>
                                    </tr>
            ");
            }

            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles
                                                             .Where(x => x.FK_VehicleID != null && x.Estado == "Nuevo"))
            {


                var vehicles = cruzRojaContext.Vehicles.Where(a => a.ID.Equals(item.FK_VehicleID))
                               .Include(a => a.Brands)
                               .Include(a => a.Model)
                               .Include(a => a.TypeVehicles)
                               .FirstOrDefault();
                sb.Append($@"
                                   <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName} {vehicles.Model.ModelName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.TypeVehicles.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>
                                     ");
            }

            if (veh.Count() > 0)
            {
                sb.Append($@" </table>
                    ");
            }



            foreach (var item in resourcesRequest.Resources_RequestResources_Materials_Medicines_Vehicles.Where(x => x.FK_VehicleID != null))
            {


                var vehicles = cruzRojaContext.Vehicles.Where(a => a.ID.Equals(item.FK_VehicleID))
                               .Include(a => a.Brands)
                               .Include(a => a.Model)
                               .Include(a => a.TypeVehicles)
                               .FirstOrDefault();

                vehiculos = $@"
                               <h6 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 18px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px; margin-bottom: 5px; margin-top: 40px; margin-left: 20px;'>Vehiculos nuevos</h6>
                                <table style='border-collapse: collapse; margin-left: 20px;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Patente</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Nombre</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Tipo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Año</th>
                                    </tr>

                                   <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.Brands.BrandName} {vehicles.Model.ModelName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.TypeVehicles.Type}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>
                                        </table>
                    ";

            }

            sb.Append($@"
                                {reason}

                                 <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>
                                    
        
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                    </p>
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>
");




            var msg = new Mail(new string[] { "yoelsoadasd@gmail.com" }, "Actulización de solicitud de recursos", $@"{sb.ToString()}");

            EmailSender.SendEmail(msg);

            //Email.Send(
            //    to: user2.Persons.Email,
            //    subject: "Solicitud de recursos",
            //    html: $@"{message}"
            //    );
        }

        //Envio de mail sin stock
        public static void sendEmailMaterial(Materials materials)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            string message = "";
            string messageFinal = "";


            var mat = cruzRojaContext.Materials.Where(x => x.ID.Equals(materials.ID))
                                                  .AsNoTracking()
                                                  .FirstOrDefault();

            var coordinadoraGeneral = cruzRojaContext.Users.Where(x => x.FK_EstateID.Equals(2)
                                                                   && x.FK_RoleID == 2)
                                                                  .Include(a => a.Persons)
                                                                  .AsNoTracking()
                                                                  .ToList();

            var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(materials.FK_EstateID))
                                                                .Include(a => a.LocationAddress)
                                                                .Include(a => a.Locations)
                                                                .AsNoTracking()
                                                                .FirstOrDefault();

            var status = materials.MaterialDonation ? "Si" : "No";

            messageFinal = $@"

                                    <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>
                                    
        
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                    </p>
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>

                              ";



            message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{materials.MaterialName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Marca</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{materials.MaterialBrand}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>¿es donación?</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>

                                    </table>
                                    
                                    {messageFinal}
                                ";

            foreach (var item in coordinadoraGeneral)
            {
                var msg = new Mail(new string[] { item.Persons.Email }, $"Material sin stock #{materials.ID}", $@"{message}");

                EmailSender.SendEmail(msg);
            }
        }
        public static void sendEmailMedicine(Medicines medicines)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            string message = "";
            string messageFinal = "";


            var mat = cruzRojaContext.Medicines.Where(x => x.ID.Equals(medicines.ID))
                                                  .AsNoTracking()
                                                  .FirstOrDefault();

            var coordinadoraGeneral = cruzRojaContext.Users.Where(x => x.FK_EstateID.Equals(2)
                                                                   && x.FK_RoleID == 2)
                                                                  .Include(a => a.Persons)
                                                                  .AsNoTracking()
                                                                  .ToList();

            var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(medicines.FK_EstateID))
                                                                .Include(a => a.LocationAddress)
                                                                .Include(a => a.Locations)
                                                                .AsNoTracking()
                                                                .FirstOrDefault();

            var status = medicines.MedicineDonation ? "Si" : "No";

            messageFinal = $@"

                                    <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>
                                    
        
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                    </p>
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>

                              ";



            message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Nombre</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicines.MedicineName}</td>
                                              </tr>

                                               <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Farmaco</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicines.MedicineDrug}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Laboratorio</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicines.MedicineLab}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Peso - Volumen</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicines.MedicineWeight}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Unidad de medida</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicines.MedicineUnits}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>¿es donación?</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descripción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{medicines.MedicineUtility}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>
                                    </table>

                                    {messageFinal}
                                ";

            foreach (var item in coordinadoraGeneral)
            {
                var msg = new Mail(new string[] { item.Persons.Email }, $"Medicamento sin stock #{medicines.ID}", $@"{message}");

                EmailSender.SendEmail(msg);
            }
        }
        public static void sendEmailVehicle(Vehicles vehicles)
        {

            CruzRojaContext cruzRojaContext = new CruzRojaContext();

            var estates = cruzRojaContext.Estates.Where(x => x.EstateID.Equals(vehicles.FK_EstateID))
                                                                .Include(a => a.LocationAddress)
                                                                .Include(a => a.Locations)
                                                                .AsNoTracking()
                                                                .FirstOrDefault();

            var veh = cruzRojaContext.TypeVehicles.Where(x => x.ID.Equals(vehicles.Fk_TypeVehicleID))
                                                               .AsNoTracking()
                                                               .FirstOrDefault();


            var coordinadoraGeneral = cruzRojaContext.Users.Where(x => x.FK_EstateID.Equals(vehicles.FK_EstateID)
                                                                   && x.FK_RoleID == 2)
                                                                  .Include(a => a.Persons)
                                                                  .AsNoTracking()
                                                                  .ToList();

            var brandModel = cruzRojaContext.Vehicles.Where(x => x.ID.Equals(vehicles.ID))
                                                               .Include(a => a.Brands)
                                                               .Include(a => a.Model)
                                                               .AsNoTracking()
                                                               .FirstOrDefault();

            var messageFinal = $@"
                                    <a style='color: white;
                                    text-align: center;
                                display: block;
                                    background: rgb(189, 45, 45);
                                text-decoration: none;
                                    border-radius: 0.4rem;
                                     width: 33%;
                                     margin-top: 2rem;
                                    margin-bottom: 2rem;
                                    padding: 15px; cursor: pointer; margin-left: 10rem;' href='https://calm-dune-0fef6d210.2.azurestaticapps.net/'>Ir a SICREYD</a>
                                    
        
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Este mensaje fue enviado automáticamente por el Sistema. Por favor no responda a este mensaje.
                                    </p>
                                    <p style='margin-top: 2rem; margin-left: 20px;'>
                                        Gracias.
                                    </p>
                                    <p style='margin-left: 20px;'>
                                        El equipo de SICREYD.
                                    </p>
                            </div>

                              ";


            var status = vehicles.VehicleDonation ? "Si" : "No";

           var  message = $@"
                                <div style='margin-top: 1.7rem; text-align: center;'>
                                    <img src='https://www.cruzroja.org.ar/newDesign/wp-content/uploads/2019/01/favicon1.png' style='
                                    width: 30px;
                                    border-radius: 50%;
                                    padding: 8px;
                                    border: 1px solid #000;'>

                                    <h1 style='font-size: 24px;
                                    font-weight: normal;
                                    font-size: 24px;
                                    font-weight: normal; margin: 0;
                                    margin-top: 5px;'>SICREYD</h1>
                                </div>
                                     <div style=' width: 512px;
                                    padding: 25px;
                                    border-radius: 8px;
                                    border: 1px solid #ccc;
                                    margin: 0 auto;'>
                                    <p style='margin-left: 20px;'>
                                    A continuación se describen los datos del material: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Marca</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{brandModel.Brands.BrandName}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Modelo</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{brandModel.Model.ModelName}</td>
                                              </tr>

                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Patente</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehiclePatent}</td>
                                              </tr>

                                               <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Año</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleYear}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Utilidad</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleUtility}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Tipo de rodado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{veh.Type}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Descripción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{vehicles.VehicleDescription}</td>
                                              </tr>

                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Sucursal</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{estates.LocationAddress.Address} {estates.LocationAddress.NumberAddress} {estates.EstateTypes.ToUpper()}</td>
                                              </tr>

                                    </table>

                                    {messageFinal}
                                ";

            foreach (var item in coordinadoraGeneral)
            {
                var msg = new Mail(new string[] { item.Persons.Email }, $"Vehiculo sin stock #{vehicles.ID}", $@"{message}");

                EmailSender.SendEmail(msg);
            }
        }


    }

}
