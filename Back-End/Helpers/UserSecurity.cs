using Microsoft.IdentityModel.Tokens;
using Back_End.Entities;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Back_End.Helpers;

namespace Back_End.Entities
{
    public class UserSecurity
    {

        //Esta funcion permitira llevar adelante el token para poder acceder al Sistema.
        public static string GenerateAccessToken(int userID, string RoleName)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
           
            //Los claims me serviran para darle al token informacion adicional para el acceso al Sistema.
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(userID)),
                new Claim(ClaimTypes.Role,RoleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token= new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            //Se devuelve el token creado con sus respectivos datos.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}