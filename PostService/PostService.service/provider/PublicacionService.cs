using Microsoft.EntityFrameworkCore;
using PostService.data.Models;
using PostService.service.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.service.provider
{
    /// <summary>
    /// Servicio para gestionar las operaciones CRUD de las publicaciones.
    /// </summary>
    public class PublicacionService : IPublicacionService
    {
        private readonly PublicacionesDbContext _context;

        /// <summary>
        /// Constructor del servicio de publicaciones.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public PublicacionService(PublicacionesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las publicaciones disponibles.
        /// </summary>
        /// <returns>Lista de publicaciones.</returns>
        public async Task<IEnumerable<Publicacion>> GetAllAsync()
        {
            return await _context.Publicacions.ToListAsync();
        }

        /// <summary>
        /// Obtiene una publicación por su ID.
        /// </summary>
        /// <param name="id">ID de la publicación.</param>
        /// <returns>Publicación encontrada o null si no existe.</returns>
        public async Task<Publicacion?> GetByIdAsync(int id)
        {
            return await _context.Publicacions.FindAsync(id);
        }

        /// <summary>
        /// Crea una nueva publicación y la guarda en la base de datos.
        /// </summary>
        /// <param name="publicacion">Datos de la nueva publicación.</param>
        /// <returns>Publicación creada.</returns>
        public async Task<Publicacion> CreateAsync(Publicacion publicacion)
        {
            publicacion.FechaCreacion = DateTime.UtcNow;
            publicacion.FechaActualizacion = DateTime.UtcNow;

            _context.Publicacions.Add(publicacion);
            await _context.SaveChangesAsync();
            return publicacion;
        }

        /// <summary>
        /// Actualiza una publicación existente.
        /// </summary>
        /// <param name="id">ID de la publicación a actualizar.</param>
        /// <param name="publicacion">Datos actualizados de la publicación.</param>
        /// <returns>Publicación actualizada o null si no existe.</returns>
        public async Task<Publicacion?> UpdateAsync(int id, Publicacion publicacion)
        {
            var existingPublicacion = await _context.Publicacions.FindAsync(id);
            if (existingPublicacion == null)
                return null;

            existingPublicacion.Titulo = publicacion.Titulo;
            existingPublicacion.Contenido = publicacion.Contenido;
            existingPublicacion.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingPublicacion;
        }

        /// <summary>
        /// Elimina una publicación por su ID.
        /// </summary>
        /// <param name="id">ID de la publicación.</param>
        /// <returns>True si se eliminó correctamente, false si no se encontró.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var publicacion = await _context.Publicacions.FindAsync(id);
            if (publicacion == null)
                return false;

            _context.Publicacions.Remove(publicacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
