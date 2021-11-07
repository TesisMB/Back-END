using Back_End.Entities;
using Back_End.Models;
using Contracts.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class Resources_RequestRepository: RepositoryBase<Resources_Request>, IResources_RequestRepository
    {
        private CruzRojaContext _cruzRojaContext;
        public Resources_RequestRepository(CruzRojaContext cruzRojaContext) :base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;
        }


        public async Task<IEnumerable<Resources_Request>> GetAllResourcesRequest()
        {
            var user = UsersRepository.authUser;

            var collection = _cruzRojaContext.Resources_Requests as IQueryable<Resources_Request>;

            if(user.Roles.RoleName == "Encargado de Logistica")
            {
                collection = collection.Where(
                    a => a.Users.Estates.Locations.LocationDepartmentName == user.Estates.Locations.LocationDepartmentName);
            }
            else
            {
                return null;
            }

            return await collection
                .Include(i => i.EmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.TypesEmergenciesDisasters)
                .Include(i => i.EmergenciesDisasters.Alerts)
                .Include(i => i.Users)
                .Include(i => i.Users.Persons)
                .Include(i => i.Users.Roles)
                .Include(i => i.Users.Estates)
                .ThenInclude(i => i.Locations)
                .Include(i => i.Users.Locations)
                .Include(i => i.Resources)
                .Include(i => i.Resources.Resources_Medicines)
                .ThenInclude(i => i.Medicines)
                .Include(i => i.Resources.Resources_Materials)
                .ThenInclude(i => i.Materials)
                .Include(i => i.Resources.Resources_Vehicles)
                .ThenInclude(i => i.Vehicles)
                .ToListAsync();
        }
        public void CreateResource_Resquest(Resources_Request resources_Request)
        {

            Create(resources_Request);

            //SaveAsync();
           //StatusResources(resources_Request);

            UpdateResources(resources_Request);
        }

       /* public void StatusResources(Resources_Request resources_Request)
        {

            using (var db = new CruzRojaContext())
                resources_Request = db.Resources_Requests
                    .Include(i => i.Resources.Resources_Materials)
                    .Include(i => i.Resources.Resources_Medicines)
                    .Include(i => i.Resources.Resources_Medicines.Medicines)
                    .Include(i => i.Resources.Resources_Materials.Materials)
                    .Where(
                    a => a.Resources.Resources_Materials.Materials.MaterialQuantity >= 0
                    && a.Resources.Resources_Medicines.Medicines.MedicineQuantity >= 0)
                    .FirstOrDefault();
        }*/


        public void UpdateResources (Resources_Request resources_Request)
        {
            Materials materials = null;
            Medicines medicines = null;
            Vehicles vehicles = null;

            var db = new CruzRojaContext();
                materials = db.Materials
                    .Include(i => i.Resources_Materials)
                    .Where(
                     a => a.ID == resources_Request.Resources.Resources_Materials.FK_MaterialID)
                    .FirstOrDefault();  

                medicines = db.Medicines
                    .Include(i => i.Resources_Medicines)
                    .Where(
                     a => a.ID == resources_Request.Resources.Resources_Medicines.FK_MedicineID)
                    .FirstOrDefault();

               vehicles = db.Vehicles
                  .Include(i => i.Resources_Vehicles)
                  .Where(
                   a => a.ID == resources_Request.Resources.Resources_Vehicles.FK_VehicleID)
                  .FirstOrDefault();
           

            resources_Request.Resources.Resources_Materials.Materials = materials;
            resources_Request.Resources.Resources_Medicines.Medicines = medicines;
            resources_Request.Resources.Resources_Vehicles.Vehicles = vehicles;


            if (materials != null)
            {
                resources_Request.Resources.Resources_Materials.Materials.MaterialQuantity = materials.MaterialQuantity - resources_Request.Resources.Resources_Materials.Quantity;

                if (materials.MaterialQuantity == 0)
                {
                   resources_Request.Resources.Resources_Materials.Materials.MaterialAvailability = false;
                }
            }

            if (medicines != null || medicines.MedicineQuantity != 0)
            {
                resources_Request.Resources.Resources_Medicines.Medicines.MedicineQuantity = medicines.MedicineQuantity - resources_Request.Resources.Resources_Medicines.Quantity;

                if (medicines.MedicineQuantity == 0)
                {
                    resources_Request.Resources.Resources_Medicines.Medicines.MedicineAvailability = false;
                }
            }


            if (vehicles != null)
            {
                resources_Request.Resources.Resources_Vehicles.Vehicles.VehicleAvailability = false;          
            }

            /*La instancia del tipo de entidad ... no se puede rastrear porque ya se está rastreando otra instancia de este tipo con la misma clave*/

            //var group = _cruzRojaContext.Materials.First(g => g.ID == materials.ID);
            _cruzRojaContext.Entry(materials).CurrentValues.SetValues(materials);
            // await _cruzRojaContext.SaveChangesAsync();

            //var group1 = _cruzRojaContext.Medicines.First(g => g.ID == medicines.ID);
            _cruzRojaContext.Entry(medicines).CurrentValues.SetValues(medicines);
            //Entry le dice al DbContext que cambie el estado de la entidad en modificado
            //setvalues realiza una copia del objeto pasado con los cambios
            //Update(resources_Request);
            _cruzRojaContext.Entry(vehicles).CurrentValues.SetValues(vehicles);
            //SaveAsync();
        }

        public async Task<Resources_Request> GetResourcesRequestByID(int resource)
        {
            return await FindByCondition(res => res.ID.Equals(resource))
                .Include(i => i.Resources.Resources_Materials)
                .Include(i => i.Resources.Resources_Medicines)
                .Include(i => i.Resources.Resources_Vehicles)
                .Include(i => i.Resources.Resources_Materials.Materials)
                .Include(i => i.Resources.Resources_Medicines.Medicines)
                .Include(i => i.Resources.Resources_Vehicles.Vehicles)
                .FirstOrDefaultAsync();
        }

        public void UpdateResource_Resquest(Resources_Request resources_Request)
        {
            Update(resources_Request);
        }
        

        public void UpdateResource_Resquest2(Resources_Request resources_Request)
        {
            if (resources_Request.Resources.Resources_Request.Status == false)

            {
                if (resources_Request.Resources.Resources_Materials.Materials != null)
                {
                    resources_Request.Resources.Resources_Materials.Materials.MaterialQuantity = resources_Request.Resources.Resources_Materials.Materials.MaterialQuantity + resources_Request.Resources.Resources_Materials.Quantity;
                }

                if (resources_Request.Resources.Resources_Medicines.Medicines != null)
                {
                    resources_Request.Resources.Resources_Medicines.Medicines.MedicineQuantity = resources_Request.Resources.Resources_Medicines.Medicines.MedicineQuantity + resources_Request.Resources.Resources_Medicines.Quantity;
                }

                if (resources_Request.Resources.Resources_Vehicles.Vehicles != null)
                {
                    resources_Request.Resources.Resources_Vehicles.Vehicles.VehicleAvailability = true;
                }

                //var group = _cruzRojaContext.Materials.First(g => g.ID == materials.ID);
                _cruzRojaContext.Entry(resources_Request.Resources.Resources_Materials.Materials).CurrentValues.SetValues(resources_Request.Resources.Resources_Materials.Materials);
                // await _cruzRojaContext.SaveChangesAsync();

                //var group1 = _cruzRojaContext.Medicines.First(g => g.ID == medicines.ID);
                _cruzRojaContext.Entry(resources_Request.Resources.Resources_Medicines.Medicines).CurrentValues.SetValues(resources_Request.Resources.Resources_Medicines.Medicines);
                //Entry le dice al DbContext que cambie el estado de la entidad en modificado
                //setvalues realiza una copia del objeto pasado con los cambios
                //Update(resources_Request);
                _cruzRojaContext.Entry(resources_Request.Resources.Resources_Vehicles.Vehicles).CurrentValues.SetValues(resources_Request.Resources.Resources_Vehicles.Vehicles);
            }
              
            Update(resources_Request);
        }

    }
}
