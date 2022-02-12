using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back_End.Entities
{
    public class UserSecurity
    {

        //Esta funcion permitira llevar adelante el token para poder acceder al Sistema.
        public static string GenerateAccessToken(int userID, string RoleName)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("superSecretKey@345");

            // var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.Name, Convert.ToString(userID)),
                 new Claim(ClaimTypes.Role, Convert.ToString(RoleName))
                }),

                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            /* var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
             var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

             //Los claims me serviran para darle al token informacion adicional para el acceso al Sistema.
             var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(userID)),
                 new Claim(ClaimTypes.Role,RoleName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

             var token = new JwtSecurityToken(
                 issuer: "http://localhost:5000",
                 audience: "http://localhost:5000",
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(1),
                 signingCredentials: signinCredentials
             );

             //Se devuelve el token creado con sus respectivos datos.
             return new JwtSecurityTokenHandler().WriteToken(token);*/
        }
    }
}