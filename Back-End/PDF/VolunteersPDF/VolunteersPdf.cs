using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Back_End.VolunteersPDF
{
    public class VolunteersPdf
    {
        public static string GetHTMLString(IEnumerable<Volunteers> employee)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string status = string.Empty;

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
                                            <h2 class='text-center font-weight-bold'>Reporte de voluntarios</h2>
                                        </div>

                                        <div class='fecha'>
                                            <p>Versión: <span>01</span></p>
                                            <p>Aprobado: <span>Gerencia Gral</span></p>
                                            <p>Fecha: <span>{date}</span></p>
                                        </div>
                                    </div> 

                ");

            sb.Append(@$"
                                 <table class='table'>
                                        <thead>
                                            <tr>
                                               <th>Dni</th>
                                               <th>Nombre completo</th>
                                               <th>Dirección de trabajo</th>
                                               <th>Disponibilidad</th>
                                            </tr>
                                         </thead>
                                  <tbody>
                                                                         
                ");

            foreach (var emp in employee)
            {


                sb.Append(@$"               <tr class='centrar'>
                                              <td> {emp.Users.UserDni} </td>
                                              <td> {emp.Users.Persons.FirstName} {emp.Users.Persons.LastName} </td>
                                              <td> {emp.Users.Estates.LocationAddress.Address} ({emp.Users.Estates.EstateTypes})</td>
                                 ");
                if (emp.Users.UserAvailability)
                {

                    status = "Disponible";

                    sb.Append($@"                  <td> {status} </td>");
                }
                else
                {
                    status = "No disponible";
                    sb.Append($@"              <td> {status} </td>");
                }
            }

            sb.Append($@"
                                          </tr> 
                                                 </tbody>
                                               </table>
                                                 </section>
                                                </body>
                                            </html>");

            return sb.ToString();
        }
    }
}