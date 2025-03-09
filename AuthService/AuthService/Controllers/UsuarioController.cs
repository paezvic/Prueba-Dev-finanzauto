using AuthService.Data.Models;
using AuthService.Services.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// Permite obtener, crear, actualizar y eliminar usuarios en el sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        /// <summary>
        /// Constructor del controlador de usuario.
        /// </summary>
        /// <param name="usuarioService">Servicio de usuario inyectado.</param>
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en el sistema.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario correspondiente o un error 404 si no existe.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto usuario con la información a registrar.</param>
        /// <returns>El usuario creado con un código de estado 201.</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            var nuevoUsuario = await _usuarioService.CreateAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.UsuarioId }, nuevoUsuario);
        }

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuario">Objeto usuario con la nueva información.</param>
        /// <returns>El usuario actualizado o un error 404 si no se encuentra.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            var usuarioActualizado = await _usuarioService.UpdateAsync(id, usuario);
            if (usuarioActualizado == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return Ok(usuarioActualizado);
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>Código 204 si se elimina, o 404 si no se encuentra.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var eliminado = await _usuarioService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return NoContent();
        }
    }
}
