using AuthService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.services
{
    /// <summary>
    /// Interfaz que define los métodos para la gestión de usuarios en el sistema.
    /// Proporciona operaciones CRUD básicas: obtener, crear, actualizar y eliminar usuarios.
    /// </summary>
    interface IUsuarioService
    {
        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <returns>Una lista de usuarios.</returns>
        Task<IEnumerable<Usuario>> GetAllAsync();

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario correspondiente o null si no existe.</returns>
        Task<Usuario?> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="usuario">Objeto usuario con los datos a registrar.</param>
        /// <returns>El usuario creado.</returns>
        Task<Usuario> CreateAsync(Usuario usuario);

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuario">Objeto usuario con los nuevos datos.</param>
        /// <returns>El usuario actualizado o null si no se encuentra.</returns>
        Task<Usuario?> UpdateAsync(int id, Usuario usuario);

        /// <summary>
        /// Elimina un usuario del sistema por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>True si el usuario fue eliminado, false si no se encontró.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
