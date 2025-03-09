using AuthService.Data.Models;
using AuthService.DTO;
using AuthService.Services.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly UsuarioService _usuarioService;
        private readonly AuthServiceDbContext _context;

        public AuthController(TokenService tokenService, AuthServiceDbContext context, UsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _context = context;
            _usuarioService = usuarioService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioDTO usuario)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(u => u.Correo == usuario.Correo && u.ContrasenaHash == usuario.ContrasenaHash);

                if (user == null)
                {
                    return Unauthorized(new { mensaje = "Credenciales inválidas" });
                }

                var token = _tokenService.GenerateToken(user.UsuarioId, user.Correo);

                Response.Cookies.Append("auth_token", token, new CookieOptions
                {
                    HttpOnly = true,      
                    Secure = true,      
                    SameSite = SameSiteMode.None , 
                    Expires = DateTime.UtcNow.AddHours(1) 
                });

                return Ok(new
                {
                    User = user.Correo,
                    IdUser = user.UsuarioId,
                    Name = user.PrimerNombre,
                    Message = "Inicio de sesión exitoso"
                });
            }
            catch (Exception)
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }
        }

        [HttpPost("logout")]
        [Authorize] // Asegura que solo usuarios autenticados puedan hacer logout
        public IActionResult Logout()
        {
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { mensaje = "No hay sesión activa" });
            }

            Response.Cookies.Delete("auth_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddYears(-1)
            });

            return NoContent(); 
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            // Verificar si el usuario está autenticado
            if (User?.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "El usuario no está autenticado" });
            }

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var emailClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(emailClaim))
            {
                return Unauthorized(new { message = "No se encontraron datos del usuario en el token" });
            }

            return Ok(new
            {
                UserId = int.Parse(userIdClaim),
                Email = emailClaim
            });
        }



    }
}
