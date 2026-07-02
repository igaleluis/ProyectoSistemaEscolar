using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface IEstudianteRepositorio
    {
        Task<List<Estudiante>> GetEstudiantes();
        Task<Estudiante> GetEstudianteId(int idEstudiante);
        Task<Estudiante> CrearEstudiante(Estudiante estudiante);
        Task<Estudiante> ActualizarEstudiante(int idEstudiante, Estudiante estudiante);
        Task EliminarEstudiante(int idEstudiante);


    }
}
