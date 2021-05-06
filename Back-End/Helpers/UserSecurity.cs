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
        private JwtSettings _settings = null;
        public UserSecurity(JwtSettings settings)
        {
            _settings = settings;
        }

        UserAuthDto ret = new UserAuthDto();
        Users authUser = null;

        //Esta funcion es para validar la identidad del Usuario que quiere entrar al Sistema
        public UserAuthDto ValidateUser(Users user)
        {

            using (var db = new CruzRojaContext())
            {
                //Al momento de ingresar la Contraseña se la encripta.
                string Pass = user.UserPassword;
                string ePass = Encrypt.GetSHA256(Pass);

                authUser = db.Users.Where(
                    u => u.UserDni == user.UserDni
                    //Luego de encriptar la contraseña se verificar que coincida
                    //con la que se encuentra almacenada en la Base de datos.
                    && u.UserPassword == ePass).FirstOrDefault();
            }

            if (authUser != null)
            {
                ret = BuildUserAuthObject(authUser); //si los datos son correctos se crea el objeto del usuario autentificado
            }

            return ret;
        }




        //Este metodo va permitir poder devolver datos del Usuario logueado
        string constr = @"Server=Localhost; Database=CruzRojaDB - Testing; Trusted_Connection=True;";
        public UserAuthDto BuildUserAuthObject(Users authUser)
        {

            //utilizo la coneccion a la base de datos
            using (SqlConnection con = new SqlConnection(constr))
            {
                var userinfo = new UserAuthDto();

                //Traigo los datos que se encuentran en la tabla de Users en funcion del Usuario logeado
                string sql = string.Format(@" Select a.*,b.RoleName, c.* from Users  a
				inner join Roles b on a.FK_RoleID = b.RoleID
				inner join Persons c on c.ID = a.ID
                Where UserDni= '{0}' and UserPassword= '{1}' ", authUser.UserDni, authUser.UserPassword);


                //cmd va a permitirme poder llevar adelante la consulta a la base de datos
                SqlCommand cmd = new SqlCommand(sql, con);

                //Interpreta las variable string sql
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();


                //Devuelvo Ciertos datos del Usuario que se loguea
                while (rd.Read())
                {
                    //Dichos datos se van a encontrar en la Base de datos
                    userinfo.UserAvailable = Convert.ToBoolean(rd["UserAvailability"]);
                    userinfo.ID = Convert.ToInt32(rd["ID"]);
                    userinfo.UserDni = rd["UserDni"].ToString();
                    userinfo.Status = Convert.ToBoolean(rd["Available"]);
                    userinfo.RoleName = rd["RoleName"].ToString();
                    userinfo.token = BuildJwtToken(userinfo); //Devuelvo el token para su posterior utilizacion
                }
                return userinfo;
            }

        }


        //Este Metodo va a ser el que almacena los valores en el token para cada usuario que se loguea
        protected string BuildJwtToken(UserAuthDto authUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Creo el token con informacion necesaria

            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, authUser.UserDni),
                new Claim("UserID", authUser.ID.ToString()), 
                new Claim("isAuthenticated", authUser.UserAvailable.ToString().ToLower()),
                new Claim(ClaimTypes.Role,authUser.RoleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };



            //Se crea el token con los valores anteriores
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims, //Devuel el token con todos los Claim encontrados
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(
                _settings.MinutesToExpiration),
                signingCredentials: credentials
                );

            //Se devuelve el token creado con sus respectivos datos
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
        
    
}