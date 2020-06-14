using Back_End.DbContexts;
using Back_End.Dto;
using Back_End.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Back_End.Helpers
{
    public class UserSecurity
    {
        private JwtSettings _settings = null;
        public UserSecurity(JwtSettings settings)
        {
            _settings = settings;
        }

        public UserAuthDto ValidateUser(UserLoginDto user)
        {
            UserAuthDto ret = new UserAuthDto();
            Users authUser = null;

            //se conecta a la base de datos para verificar las datos del usuario en cuestion
            using (var db = new CruzRojaContext())
            {
                authUser = db.Users.Where(
                    u => u.UserDni.ToLower() == user.UserDni.ToLower()
                    && u.UserPassword == user.UserPassword).FirstOrDefault();
            }

            if (authUser != null)
            {
                ret = BuildUserAuthObject(authUser); //si los datos son correctos se crea el objeto del usuario autentificado
            }

            return ret; //retornamos el valor de este objeto
        }

        //metodo para crear un objeto del usuario que inicio session
        protected UserAuthDto BuildUserAuthObject(Users authUser)
        {
            UserAuthDto ret = new UserAuthDto(); // si ingreso aca es porque esta autenticado el usuario


            ret.UserName = authUser.UserDni; // retorno el nombre del usuario con el cual se esta accediendo al sistema
            ret.IsAuthenticated = true;

            ret.Permissions = GetUserClaim(authUser); // se retorno la lista de permisos

            ret.BearerToken = BuildJwtToken(ret);

            return ret; // se retorno el objeto del usuario autenticado
        }

        //Metodo para obtener los permisos de un usuario especificado
        protected List<Permissions> GetUserClaim(Users authUser)
        {
            List<Permissions> list = new List<Permissions>();

            try
            {
                using (var db = new CruzRojaContext())
                {
                    list = db.Permissions.Where(
                        u => u.IdRole == u.IdRole).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Exception trying to retrieve user claims.", ex);
            }

            return list;
        }

        //Este Metodo va a ser el que almacena los valores para el token para cada usuario
        protected string BuildJwtToken(UserAuthDto authUser)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Key));

            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,
                authUser.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()));

            jwtClaims.Add(new Claim("isAuthenticated",
                authUser.IsAuthenticated.ToString().ToLower()));

            //se obtiene cada uno del permisos con su valor
            foreach (var claim in authUser.Permissions)
            {
                jwtClaims.Add(new Claim(claim.PermissionType, claim.PermissionValue));
            }

            //Se crea el token con los valores anteriores
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: jwtClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(
                    _settings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256)
                );

            //Se devuelve el token creado
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}