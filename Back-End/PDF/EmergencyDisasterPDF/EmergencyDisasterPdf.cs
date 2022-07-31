using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.EmergencyDisasterPDF
{
    public static class EmergencyDisasterPdf
    {
        public static string GetHTMLString(EmergenciesDisasters emergency)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            //string birthdate = emergency.Users.Persons.Birthdate.ToString("dd/MM/yyyy");
            //string status = string.Empty;
            //string statusEmer = string.Empty;

            //if (employee.Users.UserAvailability)
            //    status = "Disponible";
            //else
            //    status = "No Dispobile";


            //if (employee.Users.Persons.Status)
            //    statusEmer = "Disponible";
            //else
            //    statusEmer = "No Disponible";



            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                                <link rel = 'stylesheet' href = 'https://use.fontawesome.com/releases/v5.15.3/css/all.css'
                                      integrity = 'sha384-SZXxX4whJ79/gErwcOYf+zWLeJdY/qpuqC4cAa9rOGUstPomtqpuNWT9wdPEn2fk' crossorigin = 'anonymous'>
                                <link href = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css' rel = 'stylesheet'>
                                <script src = 'https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/js/bootstrap.bundle.min.js'></script>
                            </head>

                            <body>

                            <section>
                                    <div class='principal'>
                                        <div class='logo'>
                                            <img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'>
                                        </div>

                                        <div class='titulo'>
                                            <h2 class='text-center font-weight-bold'>Informe del incidente</h2>
                                        </div>

                                        <div class='fecha'>
                                            <p></p>
                                            <p>Fecha: <span>{date}</span></p>
                                            <p></p>
                                        </div>
                                    </div> 


                           
                                ");




            sb.Append($@"
                           <div class='datos'> 
                                 <div style='border: 1px solid #ccc;'>
                                    <p style='text-align: center; margin: 0 !important; border: 1px solid #ccc;' >DIRECCIÓN/UBICACIÓN</p>
                                    <p>{emergency.LocationsEmergenciesDisasters.LocationCityName}</p>
                                </div>

                                    <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important; border: 1px solid #ccc;'>OCURRENCIA</p>
                                    <p>{emergency.EmergencyDisasterStartDate}</p>
                                </div>

                                  <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important; border: 1px solid #ccc;'>ESTADO DE LA EMERGENCIA</p>
                                    <p>{emergency.Alerts.AlertDegree}</p>
                                </div>
                            </div>
                            ");


            sb.Append($@"
                             <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important;'>TIPO DE EMERGENCIA - DESCRIPCIÓN DE LA EMERGENCIA</p>
                                </div>

                           <div class='datos'> 
                                <div style='border: 1px solid #ccc; '>
                                    <p>{emergency.TypesEmergenciesDisasters.TypeEmergencyDisasterName}</p>
                                </div>
                            
                                 <div style='border: 1px solid #ccc;'>
                                    <p>{emergency.EmergencyDisasterInstruction}</p>
                                </div>
                            </div>
                            ");




            sb.Append($@"

                                <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important;'>RECURSOS INVOLUCRADOS / TIPO (MATERIAL, FARMACIA, VEHICULO)</p>
                                </div>

                            <div class='datos'> 
                                    <div style='border: 1px solid #ccc; '>
                                                <p style='margin: 0 !important; border: 1px solid #ccc;'>NOMBRE</p>
                                    </div>
                                    <div style='border: 1px solid #ccc; '>
                                               <p style='margin: 0 !important; border: 1px solid #ccc;'>CANTIDAD</p>
                                    </div>
                            </div>
               ");


            foreach (var item in emergency.Resources_Requests)
            {

                if (item.Condition == "Aceptada")
                {

                    foreach (var item2 in item.Resources_RequestResources_Materials_Medicines_Vehicles)
                    {


                        if (item2.Materials != null)
                            sb.Append($@"
                                  <div class='datos'> 
                                     <div>
                                         <p style='border: 1px solid #ccc; margin: 0 !important;'>{item2.Materials.MaterialName}</p>
                                     </div>
                                     <div>
                                          <p style='border: 1px solid #ccc; margin: 0 !important;'>{item2.Quantity}</p>
                                     </div>
                            </div>

                    ");

                        if (item2.Medicines != null)
                            sb.Append($@"
                                  <div class='datos'> 
                                            <div>
                                             <p style='border: 1px solid #ccc; margin: 0 !important;'>{item2.Medicines.MedicineName}</p>
                                             </div>
                                         <div>
                                              <p style='border: 1px solid #ccc; margin: 0 !important;'>{item2.Quantity}</p>
                                         </div>
                                     </div>
                    ");

                        if (item2.Vehicles != null)
                            sb.Append($@"
                                  <div class='datos'> 
                                            <div>
                                             <p style='border: 1px solid #ccc; margin: 0 !important;'>{item2.Vehicles.Brands.BrandName} {item2.Vehicles.Model.ModelName}</p>
                                             </div>
                                         <div>
                                              <p style='border: 1px solid #ccc; margin: 0 !important;'>{item2.Quantity}</p>
                                         </div>
                                     </div>
                    ");
                    }
                }
            }


            sb.Append($@"
                                <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important;'>PERSONAS INVOLUCRADAS</p>
                                </div>

                           <div class='datos'> 
                                <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>LEGAJO</p>
                                </div>
                                <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>NOMBRE</p>
                                </div>

                                <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>ROL</p>
                                </div>

                            </div>

                            ");


            foreach (var item in emergency.ChatRooms.UsersChatRooms)
            {

                if (item != null)
                {



                    sb.Append($@"
                                  <div class='datos'> 
                                     <div>
                                         <p style='border: 1px solid #ccc; margin: 0 !important;'>{item.Users.UserDni}</p>
                                     </div>
                                     <div>
                                          <p style='border: 1px solid #ccc; margin: 0 !important;'>{item.Users.Persons.FirstName} {item.Users.Persons.LastName}</p>
                                     </div>
                                     <div>
                                          <p style='border: 1px solid #ccc; margin: 0 !important;'>{item.Users.Roles.RoleName}</p>
                                     </div>
                            </div>

                    ");
                }
            }






            sb.Append($@"
                                <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important;'>DAÑOS</p>
                                </div>

                           <div class='datos'> 
                                <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important;'>PERSONAS</p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>ASISTIDAS</p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>FALLECIDAS</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>EVACUADAS</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>AFECTADAS</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>RECUPERADAS</p>
                                </div>



                                 <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important;'>Nª</p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>{emergency.Victims.AssistedPeople}</p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>{emergency.Victims.NumberDeaths}</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>{emergency.Victims.EvacuatedPeople}</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>{emergency.Victims.NumberAffected}</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>{emergency.Victims.RecoveryPeople}</p>
                                </div>

                                 <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important;'>LOCALIDADES</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>AFECTADAS</p>
                                    <p style='margin: 0 !important;'>BARRIOS</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>AFECTADOS</p>
                                    <p style='margin: 0 !important;'>FAMILIAS</p>
                                    <p style='margin: 0 !important; border: 1px solid #ccc;'>AFECTADOS</p>
                                </div>

                                 <div style='border: 1px solid #ccc;'>
                                    <p style='margin: 0 !important;'>Nª</p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>{emergency.Victims.AffectedLocalities}</p>
                                   <p style='margin: 0 !important;'></p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>{emergency.Victims.AffectedNeighborhoods}</p>
                                   <p style='margin: 0 !important;'></p>
                                    <p style='border: 1px solid #ccc; margin: 0 !important;'>{emergency.Victims.NumberFamiliesAffected}</p>
                                </div>



                                
                            </div>

                                  <div style='border: 1px solid #ccc;'>
                                        <p style='text-align: center; margin: 0 !important;'>MONTO ESTIMADO DE DAÑOS MATERIALES ${emergency.Victims.MaterialsDamage}</p>
                                </div>
                            ");


            sb.Append($@"
                                        <p style='text-align: center; margin-bottom: 5%; margin-top: 2%;'>RESPONSABLE</p>
                                        
                           <div class='datos'> 

                            <div>
                                        <p>ENCARGADO: {emergency.EmployeeIncharge.Users.Persons.FirstName} {emergency.EmployeeIncharge.Users.Persons.LastName}</p>
                            </div>
                            <div>
                                        <p>LEGAJO: {emergency.EmployeeIncharge.Users.UserDni}</p>
                            </div>
                            <div>
                                        <p>__________________________________________</p>
                                        <p>FIRMA</p>
                            </div>
                            </div>

                                         ");



            sb.Append($@"

                                         </section>
                                        </body>
                                    </html>");
            return sb.ToString();
        }
    }
}