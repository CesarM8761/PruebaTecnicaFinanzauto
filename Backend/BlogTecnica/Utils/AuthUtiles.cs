using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Datos.Context;
using Datos.Models;
using Microsoft.IdentityModel.Tokens;

namespace GestorAutenticación
{
    public class AuthUtiles
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] salt = sha256.ComputeHash(Encoding.UTF8.GetBytes("pruebatecnicasalt"));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                    sb.Append(salt[i].ToString("x2"));
                }

                return sb.ToString();
            }

        }


        private static readonly string _key = "prueba94tecnica43finanzautoooo12";


        public static string GenerateAuthToken(int userId, string email, int role)
        {
            PruebaTecnicaContext dbcontext = new PruebaTecnicaContext();

            Usuario userData = dbcontext.Usuarios.First(u => u.IdUsuario == userId);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userData.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email,email),
                  new Claim(ClaimTypes.Role, role.ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

