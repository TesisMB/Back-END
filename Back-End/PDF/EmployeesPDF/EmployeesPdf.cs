﻿using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Back_End.EmployeesPDF
{
    public static class EmployeesPdf
    {
        public static string GetHTMLString(IEnumerable<Users> employee)
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
                                            <h2 class='text-center font-weight-bold'>Reporte de empleados</h2>
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
                                               <th>Cargo</th>
                                               <th>Dirección de trabajo</th>
                                               <th>Disponibilidad</th>
                                            </tr>
                                         </thead>
                                  <tbody>
                                                                         
                ");

            foreach (var emp in employee)
            {


            sb.Append(@$"               <tr>
                                              <td> {emp.UserDni} </td>
                                              <td> {emp.Persons.FirstName} {emp.Persons.LastName}</td>
                                              <td> {emp.Roles.RoleName} </td>
                                              <td> {emp.Estates.LocationAddress.Address} {emp.Estates.LocationAddress.NumberAddress} </td>

                                 ");
                        if (emp.UserAvailability)
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