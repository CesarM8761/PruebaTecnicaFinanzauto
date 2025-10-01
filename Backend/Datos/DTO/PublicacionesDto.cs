using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DTO
{
    public class CrearPublicacionDto
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdCategoria { get; set; }
        public int IdImagenPublicacion { get; set; }
    }

    public class PublicacionDto
    {
        public int IdPublicacion { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string AutorUsername { get; set; }
        public int IdCategoria { get; set; }
        public string Imagen { get; set; }


    }
}
