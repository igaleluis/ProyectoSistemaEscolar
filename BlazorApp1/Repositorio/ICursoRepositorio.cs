using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface ICursoRepositorio
    {
        Task<List<Curso>> GetCursos();
        Task<Curso> ObtenerCursoId(int idCurso);
        Task<Curso> CrearCurso(Curso curso);
        Task<Curso> ActualizarCurso(int idCurso, Curso curso);
        Task EliminarCurso(int idCurso);

    }
}
