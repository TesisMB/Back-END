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
        public static string GetHTMLString(IEnumerable<ResourcesRequest> request, string condition, DateTime dateStart, DateTime dateEnd)
        {
            string date = dateStart.ToString("dd/MM/yyyy");
            string date2 = dateEnd.ToString("dd/MM/yyyy");

            List<Materials> matList = new List<Materials>();
            List<Medicines> medList = new List<Medicines>();
            List<Vehicles> vehList = new List<Vehicles>();

            foreach (var e in request)
            {
                foreach (var item in e.Resources_RequestResources_Materials_Medicines_Vehicles)
                {
                    if (item.Materials != null)
                       matList.Add(item.Materials);

                    if (item.Medicines != null)
                        medList.Add(item.Medicines);

                    if (item.Vehicles != null)
                        vehList.Add(item.Vehicles);
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
                                                            <div class='imagen-fondo'>

                                                       <table class='table'>
                                                                 <thead>
                                                                                <tr>
                                                                                    <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '2' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de solicitudes</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                                   

                                                                        ");
            //}


            if (date2 != "01/01/0001")
            {
                sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
            }
            else
            {
                sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
            }


            sb.Append(@$" </th>
                                                                                </tr>
                                                                             <tr class='fondo'>
                                                                            <th colspan='6' style='text-transform: uppercase;'>{condition}s </th>
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
                                                                        <tbody>");

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
                                  ");
            }

            sb.Append($@"  
                                                                   </tr>   
                                                             </tbody>
                                                          </table>
                                                        </div>


");




            if (matList.Count() != 0)
            {
                sb.Append($@"  
                                                            <div class='imagen-fondo'>

                                                       <table class='table'>
                                                                 <thead>
                                                                                <tr>
                                                                                    <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '2' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de solicitudes</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                                   

                                                                        ");
            }

            if (matList.Count() != 0)
            {

            if (date2 != "01/01/0001")
            {
                sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
            }
            else
            {
                sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
            }



            sb.Append($@"

                                                                             <tr class='fondo'>
                                                                                 <th colspan='6'>MATERIALES</th>
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
            }

            sb.Append($@"  <tr> ");



            foreach (var emp in matList)
            {
                    sb.Append($@"  
                                                  <td> {emp.ID} </td>
                                                  <td> {emp.MaterialName} </td>
                                                  <td> {emp.MaterialBrand} </td>
                                                  <td> {emp.MaterialQuantity} </td>
                                            </tr> 
                                      ");
            }


            sb.Append($@"
                              </tbody>
                                    </table>
                                </div> 
");



            if (medList.Count() != 0)
            {
                sb.Append($@"  
                                                            <div class='imagen-fondo'>

                                                       <table class='table'>
                                                                 <thead>
                                                                                <tr>
                                                                                    <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '2' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de solicitudes</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                                   

                                                                        ");
            }


            if (medList.Count() != 0)
            {

            if (date2 != "01/01/0001")
            {
                sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
            }
            else
            {
                sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
            }




                sb.Append($@"

                                                                             <tr class='fondo'>
                                                                                 <th colspan='6'>FARMACIA</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Laboratorio</th>
                                                                              <th>Peso/Volumen</th>
                                                                              <th>Cantidad</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>
                                                                        ");
            }
            sb.Append($@"  <tr> ");



            foreach (var emp in medList)
            {
                sb.Append($@"  
                                                  <td> {emp.ID} </td>
                                                  <td> {emp.MedicineLab} </td>
                                                  <td> {emp.MedicineDrug} </td>
                                                  <td> {emp.MedicineWeight} {emp.MedicineUnits} </td>
                                                  <td> {emp.MedicineQuantity}</td>
                                            </tr> 
                                      ");
            }


            sb.Append($@"
                              </tbody>
                                    </table>
                                </div> 
");


            if (vehList.Count() != 0)
            {
                sb.Append($@"  
                                                            <div class='imagen-fondo'>

                                                       <table class='table'>
                                                                 <thead>
                                                                                <tr>
                                                                                    <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '2' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de solicitudes</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                                   

                                                                        ");
            }

            if (vehList.Count() != 0)
            {

            if (date2 != "01/01/0001")
            {
                sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
            }
            else
            {
                sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
            }




                sb.Append($@"
                                                                             <tr class='fondo'>
                                                                                 <th colspan='6'>VEHICULOS</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Patente</th>
                                                                              <th>Nombre</th>
                                                                              <th>Tipo</th>
                                                                              <th>Año</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>
                                                                        ");
            }
            sb.Append($@"  <tr> ");


            foreach (var emp in vehList)
            {
                sb.Append($@" 
                                                  <td> {emp.ID} </td>
                                                  <td> {emp.VehiclePatent} </td>
                                                  <td> {emp.Brands.BrandName} {emp.Model.ModelName} </td>
                                                  <td> {emp.TypeVehicles.Type} </td>
                                                  <td> {emp.VehicleYear}</td>
                                            </tr> 
                                      ");
            }


            sb.Append($@"
                              </tbody>
                                    </table>
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