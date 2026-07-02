using BlazorApp1.Models;

namespace BlazorApp1.Repositorio
{
    public interface ICalificacionesRepositorio
    {
        Task<List<Calificaciones>> ObtenerCalificaciones();
        Task<Calificaciones> ObtenerCalificacionesId(int idCalificacion);
        Task<Calificaciones> CrearCalificacion(Calificaciones calificacion);
        Task<Calificaciones> EditarCalificacion(int idCalificaciones, Calificaciones calificacion);
        Task EliminarCalificacion(int idCalificacion);
    }
}
