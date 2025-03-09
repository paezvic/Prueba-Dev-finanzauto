using System;
using System.Collections.Generic;

namespace PostService.data.Models;

public partial class Publicacion
{
    public int PublicacionId { get; set; }

    public int UsuarioId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }
}
