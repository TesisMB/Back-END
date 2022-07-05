using Back_End.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace PDF_Generator.Utility
{
    public static class Resources
    {
        public static string GetHTMLString(IEnumerable<Estates> estates, IEnumerable<Materials> materiales,
                                           IEnumerable<Medicines> medicamentos, IEnumerable<Vehicles> vehiculos,
                                           DateTime dateStart,
                                           DateTime dateEnd)
        {
            string date = dateStart.ToString("dd/MM/yyyy");
            string date2 = dateEnd.ToString("dd/MM/yyyy");
            bool med = false;
            bool mat = false;
            bool veh = false;
            List<bool> matList = new List<bool>();
            List<bool> medList = new List<bool>();
            List<bool> vehList = new List<bool>();
            bool med2 = false;
            bool mat2 = false;
            bool veh2 = false;
            List<bool> matList2 = new List<bool>();
            List<bool> medList2 = new List<bool>();
            List<bool> vehList2 = new List<bool>();


            mat2 = materiales.GetEnumerator().MoveNext();
            matList2.Add(mat2);

            med2 = medicamentos.GetEnumerator().MoveNext();
            medList2.Add(med2);

            veh2 = vehiculos.GetEnumerator().MoveNext();
            vehList2.Add(veh2);

            foreach (var item in estates)

            {
                        mat = item.Materials.GetEnumerator().MoveNext();
                        matList.Add(mat);

                        med = item.Medicines.GetEnumerator().MoveNext();
                        medList.Add(med);

                         veh = item.Vehicles.GetEnumerator().MoveNext();
                         vehList.Add(veh);
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


            if (matList.Contains(true))
            {
                sb.Append($@"       
                                                            <div class='imagen-fondo'>
                                                                        <table class='table'>
                                                                        <thead>
                                                                                <tr>
                                                                                    <th class='logo' colspan='1'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '4' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de recursos</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                        ");

                if (date2 != "01/01/0001")
                {
                    sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
                }
                else
                {
                    sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
                }

                 sb.Append(@$"
                                                        </th>
                                                              </tr>
                                                                      <tr class='fondo'>
                                                                            <th colspan='7'>MATERIALES</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Marca</th>
                                                                              <th>Cantidad</th>
                                                                              <th>Cuidad</th>
                                                                              <th>Dirección</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>");

            }

            foreach (var item in estates)
                foreach(var emp in item.Materials)
                            {    sb.Append($@"  <tr>
                                                              <td> {emp.ID} </td>
                                                              <td> {emp.MaterialName} </td>
                                                              <td> {emp.MaterialBrand} </td>
                                                              <td> {emp.MaterialQuantity} </td>
                                                              <td> {emp.Estates.Locations.LocationCityName} </td>
                                                              <td> {emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} ({emp.Estates.EstateTypes}) </td>
                                                 </tr> 
                                                  ");
                            }

            if (medList.Contains(true))
            {
                sb.Append($@"
                             </tbody>
                                    </table>
                                                            <p style='margin-left: 40%; margin-top: 40%;'>Firma y aclaración<p>
                                </div>

                                  <div class='imagen-fondo'>
                                    <table class='table'>
                                      <thead>
                                        <tr>
                                            <th class='logo' colspan='3'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                            <th colspan= '4' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de recursos</h2></th>
                                            <th colspan= '3' class='fecha'>
                                                <p>Versión: <span>01</span></p>
                                                <p>Aprobado: <span>Gerencia Gral</span></p>
                                       ");


                if (date2 != "01/01/0001")
                {
                    sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
                }
                else
                {
                    sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
                }

            sb.Append(@$"
                                                        </th>
                                                              </tr>
                                                                      <tr class='fondo'>
                                                                                <th colspan='10'>FARMACIA</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Farmaco</th>
                                                                              <th>Laboratorio</th>
                                                                              <th>Peso /        Volumen</th>
                                                                              <th>Fecha de vencimiento</th>
                                                                              <th>Cantidad</th>
                                                                              <th width='20%'>Cuidad</th>
                                                                              <th width='20%'>Dirección</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody> ");

            }


            foreach (var item in estates)
                            foreach (var emp in item.Medicines)
                            {
                                {
                                sb.Append($@"  <tr>
                                                              <td>{emp.ID} </td>
                                                              <td>{emp.MedicineName}</td>
                                                              <td>{emp.MedicineDrug}</td>
                                                              <td>{emp.MedicineLab}</td>
                                                              <td>{emp.MedicineWeight} {emp.MedicineUnits}</td>
                                                              <td>{emp.MedicineExpirationDate.ToString("dd/MM/yyyy")}</td>
                                                              <td>{emp.MedicineQuantity}</td>
                                                              <td>{emp.Estates.Locations.LocationCityName}</td>
                                                              <td>{emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} ({emp.Estates.EstateTypes})</td> 
                                                  </tr> ");
                            }
                        }
            if (vehList.Contains(true))
            {
                sb.Append($@"
                              </tbody>
                                    </table>
                                                            <p style='margin-left: 40%; margin-top: 40%;'>Firma y aclaración<p>
                                    </div>

                                  <div class='imagen-fondo'>
                                    <table class='table'>
                                      <thead>
                                        <tr>
                                            <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                            <th colspan= '4' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de recursos</h2></th>
                                            <th colspan= '2' class='fecha'>
                                                <p>Versión: <span>01</span></p>
                                                <p>Aprobado: <span>Gerencia Gral</span></p>
                                         ");


                if (date2 != "01/01/0001")
                {
                    sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
                }
                else
                {
                    sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
                }

            sb.Append(@$"
                                   </th>
                                        </tr>
                                        <tr class='fondo'>
                                        <th colspan='8'>VEHICULOS</th>
                                        <tr>
                                          <th>#Codigo</th>
                                          <th>Nombre</th>
                                          <th width='15%'>Patente</th>
                                          <th>Tipo</th>
                                          <th>Año</th>
                                          <th width='15%'>Cuidad</th>
                                          <th>Dirección</th>
                                        </tr>
                                      </thead>
                                    <tbody>");


            }





            foreach (var item in estates)
                        {
                        foreach (var emp in item.Vehicles)
                            {
                                  sb.Append($@"  <tr>
                                                      <td> {emp.ID} </td>
                                                      <td> {emp.Brands.BrandName} {emp.Model.ModelName} </td>
                                                      <td> {emp.VehiclePatent} </td>
                                                      <td> {emp.TypeVehicles.Type} </td>
                                                      <td> {emp.VehicleYear} </td>
                                                      <td> {emp.Estates.Locations.LocationCityName} </td>
                                                      <td> {emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} ({emp.Estates.EstateTypes})</td>                    
                                                 </tr> ");
                               }
                         }


            sb.Append(@"
                                 </tbody>
                                        </table>
                                                            <p style='margin-left: 40%; margin-top: 40%;'>Firma y aclaración<p>
                                    </div>

                ");

            if (matList2.Contains(true))
            {
                sb.Append($@"
                                                                 <div class='imagen-fondo'>
                                                                                <table class='table'>
                                                                                  <thead>
                                                                                    <tr>
                                                                                    <th class='logo' colspan='1'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                                                                    <th colspan= '4' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de recursos</h2></th>
                                                                                    <th colspan= '2' class='fecha'>
                                                                                        <p>Versión: <span>01</span></p>
                                                                                        <p>Aprobado: <span>Gerencia Gral</span></p>
                                                                            ");

                    if (date2 != "01/01/0001")
                    {
                        sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
                    }
                    else
                    {
                        sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
                    }

         

                sb.Append(@$"
                                                        </th>
                                                              </tr>
                                                                      <tr class='fondo'>
                                                                                   <th colspan='7'>MATERIALES (SIN STOCK)</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Marca</th>
                                                                              <th>Cantidad</th>
                                                                              <th>Cuidad</th>
                                                                              <th>Dirección</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>");
            }

            

            foreach (var emp in materiales)
                {
                    sb.Append($@"  <tr>
                                                              <td> {emp.ID} </td>
                                                              <td> {emp.MaterialName} </td>
                                                              <td> {emp.MaterialBrand} </td>
                                                              <td> {emp.MaterialQuantity} </td>
                                                              <td> {emp.Estates.Locations.LocationCityName} </td>
                                                              <td> {emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} ({emp.Estates.EstateTypes}) </td>
                                                 </tr> 
                                                  ");
                }


            if (matList2.Contains(true))
            {
                sb.Append($@"
                                                         </tbody>
                                                    </table>
                                                            <p style='margin-left: 40%; margin-top: 40%;'>Firma y aclaración<p>
                                              </div>
                       ");
            }
            if (medList2.Contains(true))
            {
                sb.Append(@$"  
                            <div class='imagen-fondo'>
                                    <table class='table'>
                                      <thead>
                                        <tr>
                                            <th class='logo' colspan='3'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                            <th colspan= '4' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de recursos</h2></th>
                                            <th colspan= '3' class='fecha'>
                                                <p>Versión: <span>01</span></p>
                                                <p>Aprobado: <span>Gerencia Gral</span></p>
                                            ");


                    if (date2 != "01/01/0001")
                    {
                        sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
                    }
                    else
                    {
                        sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
                    }

          

                sb.Append(@$"
                                                        </th>
                                                              </tr>
                                                                      <tr class='fondo'>
                                                                                <th colspan='10'>FARMACIA (SIN STOCK)</th>
                                                                            </tr>
                                                                            <tr>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Farmaco</th>
                                                                              <th>Laboratorio</th>
                                                                              <th>Peso /        Volumen</th>
                                                                              <th>Fecha de vencimiento</th>
                                                                              <th>Cantidad</th>
                                                                              <th width='20%'>Cuidad</th>
                                                                              <th width='20%'>Dirección</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody> ");

            }
            

            foreach (var emp in medicamentos)
                {
                        sb.Append($@"  <tr>
                                                              <td>{emp.ID} </td>
                                                              <td>{emp.MedicineName}</td>
                                                              <td>{emp.MedicineDrug}</td>
                                                              <td>{emp.MedicineLab}</td>
                                                              <td>{emp.MedicineWeight} {emp.MedicineUnits}</td>
                                                              <td>{emp.MedicineExpirationDate.ToString("dd/MM/yyyy")}</td>
                                                              <td>{emp.MedicineQuantity}</td>
                                                              <td>{emp.Estates.Locations.LocationCityName}</td>
                                                              <td>{emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} ({emp.Estates.EstateTypes})</td> 
                                                  </tr> ");
                }

            sb.Append(@"                         </tbody>
                                                         </table>");

            sb.Append(@"
                                                            <p style='margin-left: 40%; margin-top: 40%;'>Firma y aclaración<p>

                                                      </div>
                                                    
                                    </div>");



            if (vehList2.Contains(true))
            {
                sb.Append(@$"

                               <div class='imagen-fondo'>
                                    <table class='table'>
                                      <thead>
                                        <tr>
                                            <th class='logo' colspan='2'><img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'></th>
                                            <th colspan= '4' class='titulo'><h2 class='text-center font-weight-bold'>Reporte de recursos</h2></th>
                                            <th colspan= '2' class='fecha'>
                                                <p>Versión: <span>01</span></p>
                                                <p>Aprobado: <span>Gerencia Gral</span></p>
                                    ");

                if (date2 != "01/01/0001")
                {
                    sb.Append(@$"<p>Periodo: <span>{date} - {date2}</span></p>");
                }
                else
                {
                    sb.Append(@$"<p>Periodo: <span>{date}</span></p>");
                }

          

                sb.Append(@$"
                                   </th>
                                        </tr>
                                        <tr class='fondo'>
                                        <th colspan='8'>VEHICULOS (SIN STOCK)</th>
                                        <tr>
                                          <th>#Codigo</th>
                                          <th>Nombre</th>
                                          <th width='15%'>Patente</th>
                                          <th>Tipo</th>
                                          <th>Año</th>
                                          <th width='15%'>Cuidad</th>
                                          <th>Dirección</th>
                                          <th>Reservado por</th>
                                        </tr>
                                      </thead>
                                    <tbody>");

            }


            foreach (var emp in vehiculos)
                {
                    sb.Append($@"  <tr>
                                                      <td> {emp.ID} </td>
                                                      <td> {emp.Brands.BrandName} {emp.Model.ModelName} </td>
                                                      <td> {emp.VehiclePatent} </td>
                                                      <td> {emp.TypeVehicles.Type} </td>
                                                      <td> {emp.VehicleYear} </td>
                                                      <td> {emp.Estates.Locations.LocationCityName} </td>
                                                      <td> {emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} ({emp.Estates.EstateTypes})</td>                    
                                                      <td> {emp.Employees.Users.Persons.FirstName} {emp.Employees.Users.Persons.LastName}</td>
                                    </tr> ");
                }


            sb.Append(@"
                                 </tbody>
                                        </table>
                ");


            sb.Append($@"
                                                            <p style='margin-left: 40%; margin-top: 40%;'>Firma y aclaración<p>

                                                </div>

                                                </div>

                                               </section>

                                        </body>
                                    </html>");
            return sb.ToString();
        }
    }
}