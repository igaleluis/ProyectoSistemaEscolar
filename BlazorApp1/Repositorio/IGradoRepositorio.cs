using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface IGradoRepositorio
    {
        Task<List<Grado>> GetGrados();
        Task<Grado> GetGradoId(int idGrado);
        Task<Grado> CrearGrado(Grado grado);
        Task<Grado> ActualizarGrado(int idGrado, Grado grado);
        Task EliminarGrado(int idGrado);
    }
}
