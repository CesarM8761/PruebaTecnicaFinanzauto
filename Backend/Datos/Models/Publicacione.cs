using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class Publicacione
{
    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdAutor { get; set; }

    public int? IdImagenPublicacion { get; set; }

    public int IdPublicacion { get; set; }

    public int IdCategoria { get; set; }

    public virtual Usuario IdAutorNavigation { get; set; } = null!;

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Imagene? IdImagenPublicacionNavigation { get; set; }

    public virtual ICollection<PubliImagen> PubliImagens { get; set; } = new List<PubliImagen>();
}
