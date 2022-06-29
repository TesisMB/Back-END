using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace PDF_Generator.Utility
{
    public static class Resource
    {
        public static string GetHTMLString(Estates estates)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");

            var mat = estates.Materials.GetEnumerator().MoveNext();
            var med = estates.Medicines.GetEnumerator().MoveNext();
            var veh = estates.Vehicles.GetEnumerator().MoveNext();

            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                                  <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor' crossorigin='anonymous'>
                                  <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js' integrity='sha384-pprn3073KE6tl6bjs2QrFaJGz5/SUsLqktiwsUTF55Jfv3qYSDhgCecCxMW52nD2' crossorigin='anonymous'></script>
                             </head>

                            <body>

                            <section>
                                    <div class='principal'>
                                        <div class='logo'>
                                            <img src='https://1.bp.blogspot.com/-6cW-1Sw9Hno/YRV4NpAW-fI/AAAAAAAAB28/W0fMEIj41C4NNq2v4xo9JOHbo4otpKKZQCLcBGAsYHQ/s0/Logo.png'>
                                        </div>

                                        <div class='titulo'>
                                            <h2 class='text-center font-weight-bold'>Reporte de recursos</h2>
                                        </div>

                                        <div class='fecha'>
                                            <p>Versión: <span>01</span></p>
                                            <p>Aprobado: <span>Gerencia Gral</span></p>
                                            <p>Fecha: <span>{date}</span></p>
                                        </div>
                                    </div> 
                                      
                             ");

            if (mat)
            {
                sb.Append($@"  
                                      <h4 align='center' style='font-size: 20px; background: #ccc; margin: 0 !important;'>MATERIALES</h4>
                                                                        <table class='table'>
                                                                          <thead>
                                                                            <tr>
                                                                              <th>#</th>
                                                                              <th>#Codigo</th>
                                                                              <th>Nombre</th>
                                                                              <th>Marca</th>
                                                                              <th>Cantidad</th>
                                                                            </tr>
                                                                          </thead>
                                                                        <tbody>

                                                                        ");
            }




            foreach (var emp in estates.Materials)
            {
                sb.Append($@"  <tr>
                                              <th></th>
                                              <td> {emp.ID} </td>
                                              <td> {emp.MaterialName} </td>
                                              <td> {emp.MaterialBrand} </td>
                                              <td> {emp.MaterialQuantity} </td>
                                        </tr> 
                                  ");
            }


            if (med)
            {
                sb.Append($@"
                              </tbody>
                                    </table>
                                        <h4 align='center' style='font-size: 20px; background: #ccc; margin: 0 !important;'>FARMACIA</h4>
                                    <table class='table'>
                                      <thead>
                                        <tr>
                                          <th>#</th>
                                          <th>#Codigo</th>
                                          <th>Nombre</th>
                                          <th>Farmaco</th>
                                          <th>Laboratorio</th>
                                          <th>Peso/Volumen</th>
                                          <th>Fecha de vencimiento</th>
                                          <th>Cantidad</th>
                                        </tr>
                                      </thead>
                                    <tbody> ");

            }
            foreach (var emp in estates.Medicines)
            {
                sb.Append($@"  <tr>
                                              <th></th>
                                              <td> {emp.ID} </td>
                                              <td> {emp.MedicineName}</td>
                                              <td> {emp.MedicineDrug}</td>
                                              <td> {emp.MedicineLab}</td>
                                              <td> {emp.MedicineWeight} {emp.MedicineUnits}</td>
                                              <td> {emp.MedicineExpirationDate.ToString("dd/MM/yyyy")} </td>
                                              <td> {emp.MedicineQuantity} </td>
                                        </tr> ");
            }
            if (veh)
            {

                sb.Append($@"
                              </tbody>
                                    </table>
                                        <h4 align='center' style='font-size: 20px; background: #ccc; margin: 0 !important;'>VEHICULOS</h4>
                                    <table class='table'>
                                      <thead>
                                        <tr>
                                          <th>#</th>
                                          <th>#Codigo</th>
                                          <th>Nombre</th>
                                          <th>Patente</th>
                                          <th>Tipo</th>
                                          <th>Año</th>
                                        </tr>
                                      </thead>
                                    <tbody> ");
            }

            foreach (var emp in estates.Vehicles)
            {
                sb.Append($@"  <tr>
                                              <th></th>
                                              <td> {emp.ID} </td>
                                              <td> {emp.Brands.BrandName} {emp.Model.ModelName} </td>
                                              <td> {emp.VehiclePatent} </td>
                                              <td> {emp.TypeVehicles.Type} </td>
                                              <td> {emp.VehicleYear} </td>
                                        </tr> ");
            }



            sb.Append($@"
                             </tbody>
                                    </table>
                                         </section>
                                        </body>
                                    </html>");
            return sb.ToString();
        }
    }
}