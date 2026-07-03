using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface IEstudianteRepositorio
    {
        Task<List<Estudiante>> GetEstudiantes();
        Task<List<Usuario>> GetUsuarioDisponibleEstudiante();
        Task<Estudiante> GetEstudianteId(int idEstudiante);
        Task<Estudiante> GetEstudianteIdUsuario(int idUsuario);
        Task<Estudiante> CrearEstudiante(Estudiante estudiante);
        Task EliminarEstudiante(int idEstudiante);
        Task<List<CursoEstudianteVM>> GetCursosEstudiante(int idEstudiante);

    }
}
