using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class Imagene
{
    public int IdImagen { get; set; }

    public byte[]? Imagen { get; set; }

    public virtual ICollection<PubliImagen> PubliImagens { get; set; } = new List<PubliImagen>();

    public virtual ICollection<Publicacione> Publicaciones { get; set; } = new List<Publicacione>();
}
