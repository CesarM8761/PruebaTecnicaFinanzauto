using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class PubliImagen
{
    public int IdImagen { get; set; }

    public int IdPublicacion { get; set; }

    public int Posicion { get; set; }

    public virtual Imagene IdImagenNavigation { get; set; } = null!;

    public virtual Publicacione IdPublicacionNavigation { get; set; } = null!;
}
