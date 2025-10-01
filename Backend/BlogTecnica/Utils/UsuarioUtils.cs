using Datos.Context;
using Datos.DTO;
using Datos.Models;
using GestorAutenticación;

namespace BlogTecnicaAPI.Utils
{
    public static class UsuarioUtils
    {
        public static IEnumerable<object> GetUsuarios(PruebaTecnicaContext dbContext)
        {
            return dbContext.Usuarios
                .Select(u => new { u.IdUsuario, u.Nombre, u.Username, u.Email })
                .ToList();
        }

        public static object? GetUsuarioById(PruebaTecnicaContext dbContext, int id)
        {
            return dbContext.Usuarios
                .Where(u => u.IdUsuario == id)
                .Select(u => new { u.IdUsuario, u.Nombre, u.Username, u.Email })
                .FirstOrDefault();
        }

        public static (bool success, string message, int? id) CrearUsuario(PruebaTecnicaContext dbContext, CrearUsuarioDto dto)
        {
            if (dbContext.Usuarios.Any(u => u.Email == dto.Email))
                return (false, "Ya existe un usuario con ese correo", null);

            var nuevo = new Usuario
            {
                Nombre = dto.Nombre,
                Username = dto.Username,
                Email = dto.Email,
                Password = AuthUtiles.HashPassword(dto.Password)
            };

            dbContext.Usuarios.Add(nuevo);
            dbContext.SaveChanges();

            return (true, "Usuario creado correctamente", nuevo.IdUsuario);
        }


        public static (bool success, string message) ModificarUsuario(PruebaTecnicaContext dbContext, int id, CrearUsuarioDto modificado)
        {
            var usuario = dbContext.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            if (usuario == null) return (false, "Usuario no encontrado");

            usuario.Nombre = modificado.Nombre;
            usuario.Username = modificado.Username;
            usuario.Email = modificado.Email;

            if (!string.IsNullOrEmpty(modificado.Password))
                usuario.Password = AuthUtiles.HashPassword(modificado.Password);

            dbContext.SaveChanges();
            return (true, "Usuario modificado correctamente");
        }

        public static (bool success, string message) EliminarUsuario(PruebaTecnicaContext dbContext, int id)
        {
            var usuario = dbContext.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            if (usuario == null) return (false, "Usuario no encontrado");

            dbContext.Usuarios.Remove(usuario);
            dbContext.SaveChanges();

            return (true, "Usuario eliminado correctamente");
        }

        public static (bool success, string? token, int? idUsuario, string? message) Auth(PruebaTecnicaContext dbContext, AuthDto infoLogin)
        {
            var usuarioExistente = dbContext.Usuarios.FirstOrDefault(u => u.Email == infoLogin.Email);
            if (usuarioExistente == null) return (false, null, null, "El usuario indicado no existe");

            if (usuarioExistente.Password == AuthUtiles.HashPassword(infoLogin.Password))
            {
                var token = AuthUtiles.GenerateAuthToken(usuarioExistente.IdUsuario, usuarioExistente.Email, 1);
                return (true, token, usuarioExistente.IdUsuario, null);
            }
            return (false, null,null, "Credenciales inválidas");
        }
    }
}
