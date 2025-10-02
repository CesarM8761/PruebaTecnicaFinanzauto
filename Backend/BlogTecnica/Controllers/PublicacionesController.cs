using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Datos.DTO;
using BlogTecnicaAPI.Utils;
using System.Security.Claims;
using Datos.Context;

namespace BlogTecnicaAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        PruebaTecnicaContext dbContext = new PruebaTecnicaContext();

        [HttpGet]
        public IActionResult Get()
        {
            var publicaciones = PublicacionesUtils.GetPublicaciones(dbContext);
            return Ok(publicaciones);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pub = PublicacionesUtils.GetPublicacionById(dbContext, id);
            if (pub == null) return NotFound("Publicación no encontrada");
            return Ok(pub);
        }
        [HttpGet("Usuario/{idUsuario}")]
        public IActionResult PublicacionesPorUsuario(int idUsuario)
        {
            var pub = PublicacionesUtils.GetPublicacionesByUserId(dbContext, idUsuario);
            if (pub == null) return NotFound("Publicación no encontrada");
            return Ok(pub);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CrearPublicacionDto dto)
        {
            var idAutor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = PublicacionesUtils.CrearPublicacion(dbContext, dto, idAutor);
            if (!result.success) return BadRequest(result.message);
            return Ok(new { result.message, idPublicacion = result.id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] CrearPublicacionDto dto)
        {
            var idAutor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = PublicacionesUtils.ModificarPublicacion(dbContext, id, dto, idAutor);
            if (!result.success) return BadRequest(result.message);
            return Ok(new { result.message });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var idAutor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = PublicacionesUtils.EliminarPublicacion(dbContext, id, idAutor);
            if (!result.success) return BadRequest(result.message);
            return Ok(new { result.message });
        }
    }
}
