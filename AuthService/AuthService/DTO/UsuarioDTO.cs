using AuthService.Data.Models;

namespace AuthService.DTO
{
    public class UsuarioDTO
    {
        public string Correo { get; set; } = null!;

        public string ContrasenaHash { get; set; } = null!;

    }
}
