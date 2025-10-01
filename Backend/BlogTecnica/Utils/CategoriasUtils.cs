using Datos.Context;
using Datos.Models;

namespace BlogTecnicaAPI.Utils
{
    public static class CategoriasUtils
    {
        public static List<Categoria> ObtenerCategorias(PruebaTecnicaContext dbContext)
        {
            return dbContext.Categorias.ToList();
        }

        public static Categoria? ObtenerCategoriaPorId(PruebaTecnicaContext dbContext, int id)
        {
            return dbContext.Categorias.FirstOrDefault(c => c.IdCategoria == id);
        }
    }

    public class CrearCategoriaDto
    {
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
