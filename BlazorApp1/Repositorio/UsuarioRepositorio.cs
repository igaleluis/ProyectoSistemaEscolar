using BlazorApp1.Data;
using BlazorApp1.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp1.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        //Constructor para inyectar el contexto de la base de datos
        public UsuarioRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // Implementación del método ActualizarUsuario
        public async Task<Usuario> ActualizarUsuario(int idUsuario, Usuario usuario)
        {
            var usuarioDesdeDb = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuarioDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el Id {idUsuario}");
            }
            else
            {
                bool existeCorreo = await _contexto.Usuarios
                    .AnyAsync(u => u.Correo == usuario.Correo
                    && u.IdUsuario != idUsuario);
                if (existeCorreo)
                {
                    throw new Exception("El correo ya está registrado.");
                }
                else
                {
                    usuarioDesdeDb.Nombre = usuario.Nombre;
                    usuarioDesdeDb.Apellido = usuario.Apellido;
                    usuarioDesdeDb.FechaNacimiento = usuario.FechaNacimiento;
                    usuarioDesdeDb.Telefono = usuario.Telefono;
                    usuarioDesdeDb.Correo = usuario.Correo;
                    usuarioDesdeDb.Rol = usuario.Rol;
                    usuarioDesdeDb.Estado = usuario.Estado;
                    // Solo actualizamos la contraseña si se proporciona una nueva
                    if (!string.IsNullOrWhiteSpace(usuario.Contraseña))
                    {
                        usuarioDesdeDb.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                    }

                    await _contexto.SaveChangesAsync();
                    return usuarioDesdeDb;
                }
                
            }
            
        }


        // Implementación del método CrearUsuario
        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                bool existeCorreo = await _contexto.Usuarios
                   .AnyAsync(u => u.Correo == usuario.Correo);
                if (existeCorreo)
                {
                    throw new Exception("El correo ya está registrado.");
                }
                else
                {
                    usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                    await _contexto.Usuarios.AddAsync(usuario);
                    await _contexto.SaveChangesAsync();
                    return usuario;
                }
                    

            }
            else
            {
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
            }
        }

        // Implementación del método EliminarUsuario
        public async Task EliminarUsuario(int idUsuario)
        {
            var usuarioDesdeDb = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuarioDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el Id {idUsuario}");
            }
            else
            {

                _contexto.Remove(usuarioDesdeDb);
                await _contexto.SaveChangesAsync();
            }

        }

        // Implementación del método GetUsuarioId
        public async Task<Usuario> GetUsuarioId(int idUsuario)
        {
            var usuarioDesdeDb = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuarioDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el Id {idUsuario}");
            }
            else
            {
                return usuarioDesdeDb;
            }

        }

        // Implementación del método GetUsuarios
        public Task<List<Usuario>> GetUsuarios()
        {
            return  _contexto.Usuarios.ToListAsync();
        }


        // Implementación del método LoginUsuario
        public async Task<Usuario> LoginUsuario(string correo, string contraseña)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                bool valido = BCrypt.Net.BCrypt.Verify(contraseña, usuario.Contraseña);
                if (!valido)
                {
                    return null;
                }

                return usuario;
            }
        }

        //Implementación del método CambiarContraseña
        public async Task CambiarContraseña(string correo, string nuevaContraseña)
        {
            var usuario = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null) {
                throw new Exception("No existe un usuario con ese correo.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(nuevaContraseña))
                {
                    throw new Exception("La nueva contraseña no puede estar vacía.");
                }
                else
                {
                    usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);

                    await _contexto.SaveChangesAsync();
                }
            }
                            
        }
    }
}
