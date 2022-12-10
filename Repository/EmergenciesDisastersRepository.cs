using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.EmergenciesDisasters___Dto;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmergenciesDisastersRepository : RepositoryBase<EmergenciesDisasters>, IEmergenciesDisastersRepository
    {
        private readonly CruzRojaContext _cruzRojaContext = new CruzRojaContext();

        public EmergenciesDisastersRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {
            _cruzRojaContext = cruzRojaContext;

        }


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisasters(int userId)
        {

            //var user = UsersRepository.authUser;
            var user = EmployeesRepository.GetAllEmployeesById(userId);


            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            if (user.Roles.RoleName != "Coord. General" && user.Roles.RoleName != "Admin")
            {
                return await GetAllEmergenciesDisastersFilter(userId);
            }
            else
            {
                collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID && a.EmergencyDisasterEndDate == null);
            }

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.LocationsEmergenciesDisasters)
                 .Include(a => a.EmployeeModified)
                 .OrderBy(a => a.EmergencyDisasterStartDate)
                .ToListAsync();
        }


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilter(int userId, string limit)
        {
            //var user = UsersRepository.authUser;

            var user =  EmployeesRepository.GetAllEmployeesById(userId);

            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;
            if (string.IsNullOrEmpty(limit))
            {
                collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID);

            }
            else {

                collection = collection
                    .OrderByDescending(a => a.EmergencyDisasterID)
                    .Take(2)
                    .AsNoTracking();

            }


            //Falta filtrar unicamente los recursos solamente aceptados

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)

                .Include(i => i.Resources_Requests)
                .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)

                 .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)


                 .ThenInclude(a => a.Brands)

                     .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles.TypeVehicles)


                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Medicines)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.DateMessage)

                 .ThenInclude(a => a.Messages)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Persons)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.DateMessage)
                 .ThenInclude(a => a.Messages)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Volunteers)

                 .Include(a => a.Victims)

                 .Include(a => a.VolunteersLocationVolunteersEmergenciesDisasters)

                  .Include(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)

                 .Include(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)
                 .Include(a => a.ChatRooms.UsersChatRooms)
                 .OrderByDescending(a => a.EmergencyDisasterID)
                .ToListAsync();

        }


        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersWithourFilterApp()
        {

            var user = UsersRepository.authUser;

            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            //Falta filtrar unicamente los recursos solamente aceptados
            collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID);

            return await collection
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)


                 .Include(a => a.ChatRooms)
                 .Include(a => a.ChatRooms.UsersChatRooms)

                 //.Include(a => a.ChatRooms.UsersChatRooms)
                 //  .ThenInclude(i => i.Users)
                 //.ThenInclude(i => i.Persons)

                 //.Include(a => a.ChatRooms)
                 //.Include(a => a.ChatRooms.UsersChatRooms)
                 //.ThenInclude(i => i.Users)
                 //.ThenInclude(i => i.Roles)

                 .OrderBy(i => i.EmergencyDisasterStartDate)
                .ToListAsync();

        }


        public async Task<EmergenciesDisasters> GetEmergencyDisasterWithDetails(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
        
                .Include(i => i.TypesEmergenciesDisasters)
                .Include(i => i.Alerts)
                .Include(i => i.LocationsEmergenciesDisasters)
                .Include(i => i.EmployeeIncharge)
                .ThenInclude(i => i.Users)
                .ThenInclude(i => i.Persons)
                .Include(i => i.EmployeeIncharge.Users.Roles)

                .Include(i => i.Resources_Requests)
                .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                .ThenInclude(i => i.Materials)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)

                 .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles)


                 .ThenInclude(a => a.Brands)

                     .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Vehicles.TypeVehicles)


                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Medicines)


                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.DateMessage)
                 .ThenInclude(a => a.Messages)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Volunteers)
                 .Include(a => a.ChatRooms.UsersChatRooms)


                 //  .ThenInclude(a => a.UsersChatRooms)
                .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Persons)

                 .Include(a => a.ChatRooms)
                 .ThenInclude(a => a.UsersChatRooms)
                 .ThenInclude(a => a.Users)
                 .ThenInclude(a => a.Roles)

                 //.Include(a => a.ChatRooms)
                 //.ThenInclude(a => a.UsersChatRooms)
                 //.ThenInclude(a => a.Users.Volunteers)

                 //.Include(a => a.ChatRooms)
                 //.ThenInclude(a => a.Messages)

                 .Include(a => a.Victims)

                 .Include(a => a.VolunteersLocationVolunteersEmergenciesDisasters)

                  .Include(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)

                 .Include(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeCreated)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeCreated.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)
                 .Include(a => a.ChatRooms.UsersChatRooms)


           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmergenciesDisasters>> GetAllEmergenciesDisastersFilter(int userId)
        {
            var user = EmployeesRepository.GetAllEmployeesById(userId);

            //var user = UsersRepository.authUser;
            var collection = _cruzRojaContext.EmergenciesDisasters as IQueryable<EmergenciesDisasters>;

            collection = collection.Where(a => a.FK_EstateID == user.FK_EstateID && a.EmergencyDisasterEndDate == null);


            return await collection
                 .Include(i => i.TypesEmergenciesDisasters)
                 .Include(i => i.Alerts)
                 .Include(i => i.LocationsEmergenciesDisasters)
                 .Include(i => i.EmployeeIncharge)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeIncharge.Users.Roles)

                 .Include(i => i.Resources_Requests)
                 .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                 .ThenInclude(i => i.Materials)

                  .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Vehicles)

                  .ThenInclude(a => a.Model)

                  .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Vehicles)


                  .ThenInclude(a => a.Brands)

                   .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Vehicles.TypeVehicles)


                  .Include(i => i.Resources_Requests)
                  .ThenInclude(i => i.Resources_RequestResources_Materials_Medicines_Vehicles)
                  .ThenInclude(i => i.Medicines)

                  .Include(i => i.EmployeeModified)
                 .ThenInclude(i => i.Users)
                 .ThenInclude(i => i.Persons)
                 .Include(i => i.EmployeeModified.Users.Roles)
                 .Include(a => a.ChatRooms.UsersChatRooms)

                 .ToListAsync();
        }


        public async Task<EmergenciesDisasters> GetEmergencyDisasterById(int emergencydisasterId)
        {
            return await FindByCondition(emergdis => emergdis.EmergencyDisasterID.Equals(emergencydisasterId))
              .Include(a => a.Victims)
              .FirstOrDefaultAsync();

        }
        public void CreateEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            SpaceCamelCase(emergencyDisaster);

            Create(emergencyDisaster);

            sendEmail(emergencyDisaster, "Nueva");
        }

        private void SpaceCamelCase(EmergenciesDisasters emergencyDisaster)
        {
            if (emergencyDisaster.EmergencyDisasterInstruction != null)
            {
                emergencyDisaster.EmergencyDisasterInstruction = WithoutSpace_CamelCase.GetWithoutSpace(emergencyDisaster.EmergencyDisasterInstruction);
            }
        }

        public void UpdateEmergencyDisaster(EmergenciesDisasters emergencyDisaster, JsonPatchDocument<EmergenciesDisastersForUpdateDto> _emergencyDisaster,
                                            EmergenciesDisastersForUpdateDto emergenciesDisastersAnterior)
        {
            SpaceCamelCase(emergencyDisaster);

            Update(emergencyDisaster);
            sendEmail(emergencyDisaster, "Modificación de", _emergencyDisaster, emergenciesDisastersAnterior);
        }

        public void DeleteEmergencyDisaster(EmergenciesDisasters emergencyDisaster)
        {
            Delete(emergencyDisaster);
        }


        public static void sendEmail(EmergenciesDisasters emergenciesDisasters, string estado, 
                                    JsonPatchDocument<EmergenciesDisastersForUpdateDto> _emergencyDisaster = null,
                                    EmergenciesDisastersForUpdateDto emergenciesDisastersAnterior = null)
        {
            var user = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters.Fk_EmplooyeeID);
            var userCreate = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters.CreatedBy);

            CruzRojaContext cruzRojaContext = new CruzRojaContext();


            var coordinadoraGeneral = cruzRojaContext.Users.Where(x => x.FK_EstateID.Equals(user.FK_EstateID)
                                                                   && x.FK_RoleID == 2)
                                                                  .Include(a => a.Persons)
                                                                  .AsNoTracking()
                                                                  .ToList();

            var alerta = cruzRojaContext.Alerts.Where(x => x.AlertID.Equals(emergenciesDisasters.FK_AlertID))
                                                            .AsNoTracking()
                                                            .FirstOrDefault();

            var Type = cruzRojaContext.TypesEmergenciesDisasters.Where(x => x.TypeEmergencyDisasterID.Equals(emergenciesDisasters.FK_TypeEmergencyID))
                                                .AsNoTracking()
                                                .FirstOrDefault();

            string message;
            string messageFinal;


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

            if (estado.Equals("Nueva"))
            {
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
                                    A continuación se describen los datos de la alerta: 
                                    </p>

                               <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Responsable</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{user.Persons.LastName}, {user.Persons.FirstName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Estado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{alerta.AlertDegree}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Tipo de alerta</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{Type.TypeEmergencyDisasterName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                               <td style='border: 1px solid #000; padding: 8px;'>Ubicación</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.LocationsEmergenciesDisasters.LocationCityName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Localidades afectadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.AffectedLocalities}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Personas asistidas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.AssistedPeople}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad de afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.NumberAffected}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                            <td style='border: 1px solid #000; padding: 8px;'>Familias afectadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.NumberFamiliesAffected}</td>


                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Daños materiales</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.MaterialsDamage}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Barrios afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.AffectedNeighborhoods}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                            <td style='border: 1px solid #000; padding: 8px;'>Personas evacuadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.EvacuatedPeople}</td>


                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Daños materiales</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.NumberDeaths}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Barrios afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.RecoveryPeople}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                            <td style='border: 1px solid #000; padding: 8px;'>Personas evacuadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.EvacuatedPeople}</td>
                                    </table>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Creado: {userCreate.Persons.LastName}, {userCreate.Persons.FirstName}
                                    </p>
                                    
                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de inicio: {emergenciesDisasters.EmergencyDisasterStartDate.ToString("dd/MM/yyyy hh:mm")}
                                    </p>

                                    {messageFinal}
                                ";
            }
            else if(estado == "Modificación de" && emergenciesDisasters.EmergencyDisasterEndDate.Equals(null))
            {
                string responsable = "";
                string instruction = "";
                string alert = "";
                string victim1 = "";
                string victim2 = "";
                string victim3 = "";
                string victim4 = "";
                string victim5 = "";
                string victim6 = "";
                string victim7 = "";
                string victim8 = "";
                string victim9 = "";

                foreach (var item in _emergencyDisaster.Operations.ToList())
                {
                    var name =  item.path.Substring(1);
                    var value = item.value;
                    CruzRojaContext cruzRojaContext2 = new CruzRojaContext();
                    var emergenciesDisasters1 = cruzRojaContext.EmergenciesDisasters.Where(x => x.EmergencyDisasterID.Equals(emergenciesDisasters.EmergencyDisasterID))
                                                .Include(a => a.Victims)
                                                .Include(a => a.Alerts)
                                                .AsNoTracking()
                                               .FirstOrDefault();

                    if (name.Equals("Fk_EmplooyeeID"))
                    {
                        var empleado = EmployeesRepository.GetAllEmployeesById(Convert.ToInt32(item.value));
                        var empleadoAnterior = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters1.Fk_EmplooyeeID);

                        responsable = $@"
                                            <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Responsable</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{empleadoAnterior.Persons.LastName}, {empleadoAnterior.Persons.FirstName}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{empleado.Persons.LastName}, {empleado.Persons.FirstName}</td>
                                              </tr>";
                    }

                    if (name.Equals("emergencyDisasterInstruction"))
                    {
                        var DescAnterior = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters1.Fk_EmplooyeeID);

                        instruction = $@"  <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Instrucción</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.EmergencyDisasterInstruction}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";


                    }

                    if (name.Equals("FK_AlertID"))
                    {
                        var status = cruzRojaContext.Alerts.Where(x => x.AlertID == Convert.ToInt32(item.value))
                                                                        .AsNoTracking()
                                                                        .FirstOrDefault();

                        alert = $@"        <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Estado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Alerts.AlertDegree}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{status.AlertDegree}</td>
                                              </tr>";


                    }

                    if (name.Equals("victims/affectedLocalities"))
                    {

                        victim1 = $@"  <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Localidades afectadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.AffectedLocalities}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/numberDeaths"))
                    {

                        victim2 = $@" <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Personas fallecidas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.NumberDeaths}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/numberAffected"))
                    {

                        victim3 = $@" <tr style='border: 1px solid #000;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad de afectadoS</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.NumberAffected}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/numberFamiliesAffected"))
                    {

                        victim4 = $@" <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Familias afectadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.NumberFamiliesAffected}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/materialsDamage"))
                    {

                        victim5 = $@" <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Daños materiales</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.MaterialsDamage}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/evacuatedPeople"))
                    {

                        victim6 = $@" <tr style='border: 1px solid #000;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Personas evacuadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.EvacuatedPeople}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/affectedNeighborhoods"))
                    {

                        victim7 = $@" <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Barrios afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.AffectedNeighborhoods}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";

                    }

                    if (name.Equals("victims/assistedPeople"))
                    {

                        victim8 = $@" <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Pesonas asistidas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.AssistedPeople}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }

                    if (name.Equals("victims/recoveryPeople"))
                    {

                        victim9 = $@" <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Pesonas recuperadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters1.Victims.RecoveryPeople}</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{item.value}</td>
                                              </tr>";
                    }
                }

                var modificado = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters.ModifiedBy);

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
                                    A continuación se listan los campos modificados:
                                    </p>
                                 <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor anterior</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor actual</th>
                                    </tr>
                                    {responsable}
                                    {instruction}
                                    {alert}
                                    {victim1}
                                    {victim2}
                                    {victim3}
                                    {victim4}
                                    {victim5}
                                    {victim6}
                                    {victim7}
                                    {victim8}
                                    {victim9}
                                    </table>
                                  <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Modificado por: {modificado.Persons.LastName}, {modificado.Persons.FirstName}
                                         </p>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de modificación: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}
                                    </p>
                                    {messageFinal}
                                ";
            }


            else
            {
                var EmergD = cruzRojaContext.EmergenciesDisasters.Where(x => x.EmergencyDisasterID.Equals(emergenciesDisasters.EmergencyDisasterID))
                                                      .Include(x => x.LocationsEmergenciesDisasters)
                                                      .AsNoTracking()
                                                      .FirstOrDefault();
                estado = "Finalización de";
                var modificado = EmployeesRepository.GetAllEmployeesById(emergenciesDisasters.ModifiedBy);

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
                                    A continuación se describen los datos finales de la alerta: 
                                    </p>
                                   <table style='border-collapse: collapse; margin:auto;'>
                                    <tr style='padding: 8px;'>
                                        <th style='border: 1px solid #000; padding: 8px;'>Nombre del atributo</th>
                                        <th style='text-align: start; border: 1px solid #000; padding: 8px;'>Valor</th>
                                    </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Responsable</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{user.Persons.LastName}, {user.Persons.FirstName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Estado</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{alerta.AlertDegree}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                               <td style='border: 1px solid #000; padding: 8px;'>Ubicación</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{EmergD.LocationsEmergenciesDisasters.LocationCityName}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Localidades afectadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.AffectedLocalities}</td>
                                              </tr>

                                             <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Personas asistidas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.AssistedPeople}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Cantidad de afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.NumberAffected}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                            <td style='border: 1px solid #000; padding: 8px;'>Familias afectadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.NumberFamiliesAffected}</td>


                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Daños materiales</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.MaterialsDamage}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Barrios afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.AffectedNeighborhoods}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                            <td style='border: 1px solid #000; padding: 8px;'>Personas evacuadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.EvacuatedPeople}</td>


                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Daños materiales</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.NumberDeaths}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                                <td style='border: 1px solid #000; padding: 8px;'>Barrios afectados</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.RecoveryPeople}</td>
                                              </tr>
                                              <tr style='border: 1px solid #000; padding: 8px;'>
                                            <td style='border: 1px solid #000; padding: 8px;'>Personas evacuadas</td>
                                                <td style='border: 1px solid #000; padding: 8px;'>{emergenciesDisasters.Victims.EvacuatedPeople}</td>
                                    </table>

                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Finalizado por: {modificado.Persons.LastName}, {modificado.Persons.FirstName}
                                    </p>
                                    
                                    <p style='margin-left: 20px; margin-top: 1.5rem; font-weight: normal;'>
                                        - Fecha de Finalización: {DateTime.Now.ToString("dd/MM/yyyy hh:mm")}
                                    </p>
                                    
                                    {messageFinal}
                                ";
                      }



            foreach (var item in coordinadoraGeneral)
                {
                    var msg = new Mail(new string[] { item.Persons.Email }, $"{estado} alerta #{emergenciesDisasters.EmergencyDisasterID}", $@"{message}");

                    EmailSender.SendEmail(msg);
                }
        
        }

     }

}
