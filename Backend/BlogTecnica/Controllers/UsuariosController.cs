using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Datos.DTO;
using Datos.Models;
using BlogTecnicaAPI.Utils;
using GestorAutenticación;
using Datos.Context;

namespace BlogTecnicaAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        PruebaTecnicaContext dbContext = new PruebaTecnicaContext();

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            return Ok(UsuarioUtils.GetUsuarios(dbContext));
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = UsuarioUtils.GetUsuarioById(dbContext, id);
            if (usuario == null) return NotFound("Usuario no encontrado");
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] CrearUsuarioDto nuevo)
        {
            var result = UsuarioUtils.CrearUsuario(dbContext, nuevo);
            if (!result.success) return BadRequest(result.message);
            return Ok(new { mensaje = result.message, result.id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult ModificarUsuario(int id, [FromBody] CrearUsuarioDto modificado)
        {
            var result = UsuarioUtils.ModificarUsuario(dbContext, id, modificado);
            if (!result.success) return NotFound(result.message);
            return Ok(new { mensaje = result.message });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult EliminarUsuario(int id)
        {
            var result = UsuarioUtils.EliminarUsuario(dbContext, id);
            if (!result.success) return NotFound(result.message);
            return Ok(new { mensaje = result.message });
        }

        [HttpPost("auth")]
        public IActionResult Auth([FromBody] AuthDto infoLogin)
        {
            var result = UsuarioUtils.Auth(dbContext, infoLogin);
            if (!result.success) return BadRequest(result.message);
            return Ok(new { token = result.token , idUsuario = result.idUsuario});
        }
    }
}
