using PostService.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.service.services
{
    /// <summary>
    /// Interfaz que define las operaciones para la gestión de publicaciones.
    /// </summary>
    public interface IPublicacionService
    {
        /// <summary>
        /// Obtiene todas las publicaciones disponibles.
        /// </summary>
        /// <returns>Lista de publicaciones.</returns>
        Task<IEnumerable<Publicacion>> GetAllAsync();

        /// <summary>
        /// Obtiene una publicación por su ID.
        /// </summary>
        /// <param name="id">ID de la publicación.</param>
        /// <returns>Publicación encontrada o null si no existe.</returns>
        Task<Publicacion?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva publicación.
        /// </summary>
        /// <param name="publicacion">Datos de la publicación.</param>
        /// <returns>Publicación creada.</returns>
        Task<Publicacion> CreateAsync(Publicacion publicacion);

        /// <summary>
        /// Actualiza una publicación existente.
        /// </summary>
        /// <param name="id">ID de la publicación a actualizar.</param>
        /// <param name="publicacion">Datos actualizados de la publicación.</param>
        /// <returns>Publicación actualizada o null si no existe.</returns>
        Task<Publicacion?> UpdateAsync(int id, Publicacion publicacion);

        /// <summary>
        /// Elimina una publicación por su ID.
        /// </summary>
        /// <param name="id">ID de la publicación.</param>
        /// <returns>True si se eliminó correctamente, false si no se encontró.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
