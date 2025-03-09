using System;
using System.Collections.Generic;

namespace AuthService.Data.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Correo { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}
