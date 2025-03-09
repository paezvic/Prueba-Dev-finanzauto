using AuthService.Data.Models;
using AuthService.Services;
using AuthService.Services.services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Services.Providers
{
    /// <summary>
    /// Servicio que gestiona las operaciones CRUD para la entidad Usuario.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly AuthServiceDbContext _context;

        /// <summary>
        /// Constructor del servicio de usuarios.
        /// </summary>
        /// <param name="context">Contexto de base de datos para acceder a los usuarios.</param>
        public UsuarioService(AuthServiceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en el sistema.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario encontrado o null si no existe.</returns>
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto usuario con la información a registrar.</param>
        /// <returns>El usuario creado.</returns>
        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.UtcNow;
            usuario.Activo = true;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuario">Objeto usuario con los nuevos datos.</param>
        /// <returns>El usuario actualizado o null si no se encuentra.</returns>
        public async Task<Usuario?> UpdateAsync(int id, Usuario usuario)
        {
            var existingUsuario = await _context.Usuarios.FindAsync(id);
            if (existingUsuario == null)
                return null;

            existingUsuario.PrimerNombre = usuario.PrimerNombre;
            existingUsuario.SegundoNombre = usuario.SegundoNombre;
            existingUsuario.Correo = usuario.Correo;
            existingUsuario.ContrasenaHash = usuario.ContrasenaHash;
            existingUsuario.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingUsuario;
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>True si el usuario fue eliminado, false si no se encontró.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
