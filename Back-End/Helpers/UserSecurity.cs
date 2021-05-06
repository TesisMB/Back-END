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
        protected UserAuthDto BuildUserAuthObject(Users authUser)
        {
            string constr = @"Server=Localhost; Database=CruzRojaDB - Testing; Trusted_Connection=True;";

            //utilizo la coneccion a la base de datos
            using (SqlConnection con = new SqlConnection(constr))
            {

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
                    ret.IsAuthenticated = true;
                    ret.ID = Convert.ToInt32(rd["ID"]);
                    ret.UserDni = rd["UserDni"].ToString();
                    ret.FirstName = rd["FirstName"].ToString();
                    ret.LastName = rd["LastName"].ToString();
                    ret.Phone = rd["Phone"].ToString();
                    ret.Email = rd["Email"].ToString();
                    ret.Gender = rd["Gender"].ToString();
                    ret.Birthdate = (DateTimeOffset)rd["Birthdate"];
                    ret.Available = Convert.ToBoolean(rd["Available"]); 
                    ret.RoleName = rd["RoleName"].ToString();
                    ret.token = BuildJwtToken(ret); //Devuelvo el token para su posterior utilizacion
                }
            }

            return ret; // se retorno los valores del Usuario logueado
        }


        //Este Metodo va a ser el que almacena los valores en el token para cada usuario que se loguea
        protected string BuildJwtToken(UserAuthDto authUser)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Key));

            //Creo el token con informacion necesaria

            List<Claim> jwtClaims = new List<Claim>();
            {

                //Los valores que el token va a devolver sera el RoleName del Usuario logueado
                jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, authUser.UserDni));
                jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()));

                jwtClaims.Add(new Claim("isAuthenticated",
              authUser.IsAuthenticated.ToString().ToLower()));


                jwtClaims.Add(new Claim(ClaimTypes.Role, authUser.RoleName));
                //Se crea el token con los valores anteriores
                var token = new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: jwtClaims, //Devuel el token con todos los Claim encontrados
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(
                        _settings.MinutesToExpiration),
                    signingCredentials: new SigningCredentials(key,
                    SecurityAlgorithms.HmacSha256)
                    );

                //Se devuelve el token creado con sus respectivos datos
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}