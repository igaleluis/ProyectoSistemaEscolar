using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface IMaestroRepositorio
    {
        Task<List<Maestro>> GetMaestros();
        
        Task<List<Usuario>> GetUsuarioDisponibleMaestro();
        Task<Maestro> GetMaestroId(int idMaestro);
        Task<Maestro> GetMaestroPorIdUsuario(int idUsuario);
        Task<Maestro> CrearMaestro(Maestro maestro);
        Task EliminarMaestro(int idMaestro);
    }
}
