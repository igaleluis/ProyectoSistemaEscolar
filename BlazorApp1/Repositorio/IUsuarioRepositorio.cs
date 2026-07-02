using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface IUsuarioRepositorio
    {
        public Usuario ObtenerPorCorreo(string email);
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarioId(int idUsuario);
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<Usuario> ActualizarUsuario(int idUsuario, Usuario usuario);
        Task EliminarUsuario(int idUsuario);
        Task<Usuario> LoginUsuario(string correo, string contraseña);
        


    }
}
