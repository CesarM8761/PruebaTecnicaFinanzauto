using System;
using System.Collections.Generic;

namespace Datos.Models;

public partial class Usuario
{
    public string Nombre { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual ICollection<Publicacione> Publicaciones { get; set; } = new List<Publicacione>();
}
