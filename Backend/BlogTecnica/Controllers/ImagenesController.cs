using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Datos.Models;
using Datos.DTO;
using BlogTecnicaAPI.Utils;
using System.Linq;
using Datos.DTO;
using Datos.Context;

namespace BlogTecnicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        PruebaTecnicaContext dbContext = new PruebaTecnicaContext();

        // GET: api/Imagenes
        [HttpGet]
        public ActionResult<IEnumerable<ImagenDTO>> Get()
        {
            var imagenes = dbContext.Imagenes
                .Select(img => new ImagenDTO
                {
                    IdImagen = img.IdImagen,
                    Base64 = img.Imagen != null ? ImagenUtils.BytesToBase64(img.Imagen) : string.Empty
                })
                .ToList();

            return Ok(imagenes);
        }

        // GET: api/Imagenes/5
        [HttpGet("{id}")]
        public ActionResult<ImagenDTO> Get(int id)
        {
            var img = dbContext.Imagenes.Find(id);
            if (img == null) return NotFound();

            var dto = new ImagenDTO
            {
                IdImagen = img.IdImagen,
                Base64 = img.Imagen != null ? ImagenUtils.BytesToBase64(img.Imagen) : string.Empty
            };

            return Ok(dto);
        }

        // POST: api/Imagenes
        [HttpPost]
        [Authorize]
        public ActionResult<ImagenDTO> Post([FromBody] ImagenDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Base64)) return BadRequest();

            var img = new Imagene
            {
                Imagen = ImagenUtils.Base64ToBytes(dto.Base64)
            };

            dbContext.Imagenes.Add(img);
            dbContext.SaveChanges();

            dto.IdImagen = img.IdImagen;
            return CreatedAtAction(nameof(Get), new { id = img.IdImagen }, dto);
        }
    }
}
