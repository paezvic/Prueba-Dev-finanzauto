using System;
using System.Collections.Generic;

namespace AuthService.Data.Models;

public partial class Token
{
    public int TokenId { get; set; }

    public int UsuarioId { get; set; }

    public string NuevoToken { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime Expiracion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
