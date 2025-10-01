using Microsoft.AspNetCore.Mvc;
using Datos.Models;
using BlogTecnicaAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Datos.Context;

namespace BlogTecnicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        PruebaTecnicaContext dbContext = new PruebaTecnicaContext(); 
        [HttpGet]
        public IActionResult GetCategorias()
        {
            var categorias = CategoriasUtils.ObtenerCategorias(dbContext);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoria(int id)
        {
            var categoria = CategoriasUtils.ObtenerCategoriaPorId(dbContext, id);
            if (categoria == null) return NotFound("Categoría no encontrada");
            return Ok(categoria);
        }
    }
}
