using Datos.Context;
using Datos.Models;
using Datos.DTO;

namespace BlogTecnicaAPI.Utils
{
    public static class PublicacionesUtils
    {
        public static List<PublicacionDto> GetPublicaciones(PruebaTecnicaContext dbContext)
        {
            return dbContext.Publicaciones
                .Select(p => new PublicacionDto
                {
                    IdPublicacion = p.IdPublicacion,
                    Titulo = p.Titulo,
                    Contenido = p.Contenido,
                    FechaCreacion = p.FechaCreacion,
                    FechaModificacion = p.FechaModificacion,
                    AutorUsername = p.IdAutorNavigation.Username,
                    Imagen = p.IdImagenPublicacionNavigation.Imagen != null ? Convert.ToBase64String(p.IdImagenPublicacionNavigation.Imagen) : null
                })
                .ToList();
        }

        public static PublicacionDto? GetPublicacionById(PruebaTecnicaContext dbContext, int id)
        {
            return dbContext.Publicaciones
                .Where(p => p.IdPublicacion == id)
                .Select(p => new PublicacionDto
                {
                    IdPublicacion = p.IdPublicacion,
                    Titulo = p.Titulo,
                    Contenido = p.Contenido,
                    FechaCreacion = p.FechaCreacion,
                    FechaModificacion = p.FechaModificacion,
                    AutorUsername = p.IdAutorNavigation.Username,
                    Imagen = p.IdImagenPublicacionNavigation.Imagen != null ? Convert.ToBase64String(p.IdImagenPublicacionNavigation.Imagen) : null

                })
                .FirstOrDefault();
        }
        public static List<PublicacionDto> GetPublicacionesByUserId(PruebaTecnicaContext dbContext, int idUsuario)
        {
            return dbContext.Publicaciones
                .Where(p => p.IdAutor == idUsuario)
                .Select(p => new PublicacionDto
                {
                    IdPublicacion = p.IdPublicacion,
                    Titulo = p.Titulo,
                    Contenido = p.Contenido,
                    FechaCreacion = p.FechaCreacion,
                    FechaModificacion = p.FechaModificacion,
                    AutorUsername = p.IdAutorNavigation.Username,
                    Imagen = p.IdImagenPublicacionNavigation.Imagen != null ? Convert.ToBase64String(p.IdImagenPublicacionNavigation.Imagen) : null

                })
                .ToList();
        }

        public static (bool success, string message, int? id) CrearPublicacion(
            PruebaTecnicaContext dbContext, CrearPublicacionDto dto, int idAutor)
        {
            var nueva = new Publicacione
            {
                Titulo = dto.Titulo,
                Contenido = dto.Contenido,
                FechaCreacion = DateTime.UtcNow,
                IdAutor = idAutor,
                IdCategoria = dto.IdCategoria,
                IdImagenPublicacion = dto.IdImagenPublicacion
            };

            dbContext.Publicaciones.Add(nueva);
            dbContext.SaveChanges();

            return (true, "Publicación creada correctamente", nueva.IdPublicacion);
        }

        public static (bool success, string message) ModificarPublicacion(
            PruebaTecnicaContext dbContext, int id, CrearPublicacionDto dto, int idAutor)
        {
            var pub = dbContext.Publicaciones.FirstOrDefault(p => p.IdPublicacion == id && p.IdAutor == idAutor);
            if (pub == null) return (false, "No se encontró la publicación o no tienes permisos");

            pub.Titulo = dto.Titulo;
            pub.Contenido = dto.Contenido;
            pub.FechaModificacion = DateTime.UtcNow;

            dbContext.SaveChanges();
            return (true, "Publicación modificada correctamente");
        }

        public static (bool success, string message) EliminarPublicacion(
            PruebaTecnicaContext dbContext, int id, int idAutor)
        {
            var pub = dbContext.Publicaciones.FirstOrDefault(p => p.IdPublicacion == id && p.IdAutor == idAutor);
            if (pub == null) return (false, "No se encontró la publicación o no tienes permisos");

            dbContext.Publicaciones.Remove(pub);
            dbContext.SaveChanges();

            return (true, "Publicación eliminada correctamente");
        }
    }
}
