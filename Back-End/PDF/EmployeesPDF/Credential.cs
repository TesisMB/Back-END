using Back_End.Models;
using System;
using System.Text;

namespace Back_End.EmployeesPDF
{
    public static class Credential
    {
        public static string GetHTMLString(Employees employee)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string createdate = employee.Users.Employees.EmployeeCreatedate.ToString("MM/yyyy");
            string status = string.Empty;
            string statusEmer = string.Empty;

            if (employee.Users.UserAvailability)
                status = "Disponible";
            else
                status = "No Dispobile";


            if (employee.Users.Persons.Status)
                statusEmer = "Disponible";
            else
                statusEmer = "No Disponible";



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

                              ");

            sb.Append($@"

                                    <div class='card w-75' style='margin-left: 13%;'>
                                      <div class='card-body datos'>
                                            <div style='margin-left: 8% !important; margin-top: 5%;'> 
                                                    <img src='https://blogger.googleusercontent.com/img/a/AVvXsEjijynSql3j2R4aHKCH77qE62R7PJuR3J5I1XEEbQ2Qft0S_WBylnFPNK1O3WeTNwO688ycor6CwDAU-Yt4N7Jjr2gvP4FVd4TIsrJqvRyNATcUgbPWV_qo4zq47RXGmTPmTpAXLC2RHcueRcryfDJ3mtadS1FbkDm08Zaru0_nC68DAlKuIQNct416'>
                                             </div>
                                                 <div style='margin-top: 7%;'>
                                                     <h5 class='card-title' style='margin-right: 15%; text-transform: uppercase;'>{employee.Users.Persons.LastName}, {employee.Users.Persons.FirstName}</h5>
                                                     <p>{employee.Users.UserDni}</p>
                                                     <p>Sede: {employee.Users.Estates.Locations.LocationCityName}</p>
                                                 </div>
                                            </div>
                                          <div class='datos' style='margin-top: 5%;'>
                                                <div style='margin-left: 22%;'>
                                                    <p >Cargo: {employee.Users.Roles.RoleName}</p>
                                                </div>

                                                <div>
                                                    <p>Válido desde: {createdate}</p>
                                                </div>
                                            </div>
                                              <div class='datos' style='margin-top: 5%;'>
                                                <div style='margin-left: 22%;'>
                                                       <p>Fecha de Ing:  {createdate}</p>
                                                  </div>
                                                <div style='margin-right: 8%;'>
                                                       <p>Sucursal:  {employee.Users.Estates.Locations.LocationCityName}</p>
                                                  </div>
                                              </div>
                                    </div>
                                         </section>
                                        </body>
                                    </html>");
            return sb.ToString();
        }
    }
}