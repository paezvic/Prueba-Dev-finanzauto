using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.data.Models;
using PostService.service.services;

namespace PostService.Controllers
{
    /// <summary>
    /// Controlador para gestionar las publicaciones.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublicacionesController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;

        /// <summary>
        /// Constructor del controlador de publicaciones.
        /// </summary>
        /// <param name="publicacionService">Servicio de publicaciones.</param>
        public PublicacionesController(IPublicacionService publicacionService)
        {
            _publicacionService = publicacionService;
        }

        /// <summary>
        /// Obtiene todas las publicaciones.
        /// </summary>
        /// <returns>Lista de publicaciones.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publicacion>>> GetPublicaciones()
        {
            var publicaciones = await _publicacionService.GetAllAsync();
            return Ok(publicaciones);
        }

        /// <summary>
        /// Obtiene una publicación por su ID.
        /// </summary>
        /// <param name="id">ID de la publicación.</param>
        /// <returns>Publicación encontrada o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Publicacion>> GetPublicacion(int id)
        {
            var publicacion = await _publicacionService.GetByIdAsync(id);
            if (publicacion == null)
            {
                return NotFound(new { mensaje = "Publicación no encontrada" });
            }
            return Ok(publicacion);
        }

        /// <summary>
        /// Crea una nueva publicación.
        /// </summary>
        /// <param name="publicacion">Datos de la nueva publicación.</param>
        /// <returns>Publicación creada.</returns>
        [HttpPost]
        public async Task<ActionResult<Publicacion>> PostPublicacion(Publicacion publicacion)
        {
            var nuevaPublicacion = await _publicacionService.CreateAsync(publicacion);
            return CreatedAtAction(nameof(GetPublicacion), new { id = nuevaPublicacion.PublicacionId }, nuevaPublicacion);
        }

        /// <summary>
        /// Actualiza una publicación existente.
        /// </summary>
        /// <param name="id">ID de la publicación a actualizar.</param>
        /// <param name="publicacion">Datos actualizados de la publicación.</param>
        /// <returns>Publicación actualizada o NotFound si no existe.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacion(int id, [FromBody] Publicacion publicacion)
        {
            var publicacionActualizada = await _publicacionService.UpdateAsync(id, publicacion);
            if (publicacionActualizada == null)
            {
                return NotFound(new { mensaje = "Publicación no encontrada" });
            }
            return Ok(publicacionActualizada);
        }

        /// <summary>
        /// Elimina una publicación por su ID.
        /// </summary>
        /// <param name="id">ID de la publicación.</param>
        /// <returns>NoContent si se elimina correctamente o NotFound si no existe.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var eliminado = await _publicacionService.DeleteAsync(id);
            if (!eliminado)
            {
                return NotFound(new { mensaje = "Publicación no encontrada" });
            }
            return NoContent();
        }
    }
}
