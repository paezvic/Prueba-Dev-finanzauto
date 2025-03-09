using AuthService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.services
{
    /// <summary>
    /// Interfaz que define los métodos para la gestión de tokens en el sistema.
    /// Permite obtener, crear y eliminar tokens de autenticación.
    /// </summary>
    public interface ITokenService
    {
        public string GenerateToken(int usuarioId, string correo);
    }
}
