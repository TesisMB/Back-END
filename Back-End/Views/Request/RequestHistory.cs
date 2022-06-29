using Back_End.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PDF_Generator.Utility
{
    public static class RequestHistory
    {
        public static string GetHTMLString(IEnumerable<ResourcesRequest> request)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            List<Materials> matList = new List<Materials>();
            List<Medicines> medList = new List<Medicines>();
            List<Vehicles> vehList = new List<Vehicles>();

            foreach (var e in request)
            {
                foreach (var item in e.Resources_RequestResources_Materials_Medicines_Vehicles)
                {
                    if (item.Materials != null)
                       matList.Add(item.Materials);

                    //medList.Add(item.Medicines);
                    //vehList.Add(item.Vehicles);
                }
            }

            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                                  <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor' crossorigin='anonymous'>
                                  <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js' integrity='sha384-pprn3073KE6tl6bjs2QrFaJGz5/SUsLqktiwsUTF55Jfv3qYSDhgCecCxMW52nD2' crossorigin='anonymous'></script>
                             </head>

                            <body>

                            <section>
                                      
                             ");

            //if (mat)
            //{
                sb.Append($@"  
                                                       <table class='table'>
                                                                 <thead>
                                                                                <tr>
                                                                                    <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '2' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de solicitudes</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                                        <p>Fecha: <span>{date}</span></p>                                        
                                                                                    </th>
                                                                                </tr>
                                                                             <tr class='fondo'>
                                                                            <th colspan='6'>RECHAZADAS</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Legajo</th>
                                                                              <th>Número de emergencia</th>
                                                                              <th>Tipo de emergencia</th>
                                                                              <th>Solicitante</th>
                                                                              <th>Encargado</th>
                                                                              <th>Fecha de creación</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>

                                                                        ");
            //}



            //SOLICITUD
            foreach (var item in request)
            {
                sb.Append($@"  <tr>
                                              <td>{item.ID}</td>
                                              <td>{item.EmergenciesDisasters.EmergencyDisasterID}</td>
                                              <td>{item.EmergenciesDisasters.TypesEmergenciesDisasters.TypeEmergencyDisasterName}</td>
                                              <td>{item.EmployeeCreated.Users.Persons.FirstName} {item.EmployeeCreated.Users.Persons.LastName}</td>
                                              <td>Raul Fernandez</td>
                                              <td>{item.RequestDate.ToString("dd/MM/yyyy")} </td>
                                        </tr> 
                                     </tbody>
                                  </table>
                                  ");
            }

            sb.Append($@"  
                                                                    <table class='table'>
                                                                          <thead>
                                                                             <tr class='fondo'>
                                                                                 <th colspan='6'>PEDIDO</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Marca</th>
                                                                              <th>Cantidad</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>

                                                                        ");


            foreach (var emp in matList)
            {
                    sb.Append($@"  <tr>
                                                  <td> {emp.ID} </td>
                                                  <td> {emp.MaterialName} </td>
                                                  <td> {emp.MaterialBrand} </td>
                                                  <td> {emp.MaterialQuantity} </td>
                                            </tr> 
                                      ");
            }


            sb.Append($@"

                                         </section>
                                        </body>
                                    </html>");
            return sb.ToString();
        }
    }
}