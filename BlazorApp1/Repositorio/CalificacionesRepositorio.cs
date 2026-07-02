using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp1.Repositorio
{
    public class CalificacionesRepositorio : ICalificacionesRepositorio
    {
        ApplicationDbContext _contexto;
        public CalificacionesRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // Implementación del método CrearCalificacion
        public async Task<Calificaciones> CrearCalificacion(Calificaciones calificacion)
        {
            if (calificacion != null)
            {
                calificacion.NotaFinal = calificacion.Zona + calificacion.ExamenFinal;

                await _contexto.AddAsync(calificacion);
                await _contexto.SaveChangesAsync();
                return calificacion;
            }
            else
            {
                throw new ArgumentNullException(nameof(calificacion), "La calificación no puede ser nula.");
            }
        }

        // Implementación del método EditarCalificacion
        public async Task<Calificaciones> EditarCalificacion(int idCalificaciones, Calificaciones calificacion)
        {
            var calificacionDesdeDb = await _contexto.Calificaciones.FirstOrDefaultAsync(c => c.IdCalificacion == idCalificaciones);
            if (calificacionDesdeDb != null)
            {
                calificacionDesdeDb.Zona = calificacion.Zona;
                calificacionDesdeDb.ExamenFinal = calificacion.ExamenFinal;
                calificacionDesdeDb.NotaFinal = calificacionDesdeDb.Zona + calificacionDesdeDb.ExamenFinal;

                await _contexto.SaveChangesAsync();
                return calificacionDesdeDb;
            }
            else
            {
                throw new ArgumentException($"No se encontró una calificación con el ID {idCalificaciones}.", nameof(idCalificaciones));
            }
        }


        // Implementación del método EliminarCalificacion
        public async Task EliminarCalificacion(int idCalificacion)
        {
            var calificacionDesdeDb = await _contexto.Calificaciones.FirstOrDefaultAsync(c => c.IdCalificacion == idCalificacion);
            if(calificacionDesdeDb != null)
            {
                _contexto.Calificaciones.Remove(calificacionDesdeDb);
                await _contexto.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró una calificación con IdCalificacion {idCalificacion}.");
            }
        }

        // Implementación del método ObtenerCalificaciones
        public async Task<List<Calificaciones>> ObtenerCalificaciones()
        {
            return await _contexto.Calificaciones
                .Include(c => c.IdCursoNavigation)
                .Include(c => c.IdEstudianteNavigation)
                .ToListAsync();
        }


        // Implementación del método ObtenerCalificacionesId
        public async Task<Calificaciones> ObtenerCalificacionesId(int idCalificacion)
        {
            var calificacion =  await _contexto.Calificaciones
                .Include(c => c.IdCursoNavigation)
                .Include(c => c.IdEstudianteNavigation)
                .FirstOrDefaultAsync(c => c.IdCalificacion == idCalificacion);
            if (calificacion == null)
            {
                throw new KeyNotFoundException($"No se encontró una calificación con IdCalificacion {idCalificacion}.");
            }
            return calificacion;
        }

        // Implementación del método ObtenerCalificacionesPorEstudiante
        public async Task<List<Calificaciones>> ObtenerCalificacionesPorEstudiante(int idEstudiante)
        {
            return await _contexto.Calificaciones
                .Where(c => c.IdEstudiante == idEstudiante)
                .Include(c => c.IdCursoNavigation)
                .ToListAsync();
        }

        // Implementación del método ObtenerCalificacionesPorCurso
        public async Task<List<Calificaciones>> ObtenerCalificacionesPorCurso(int idCurso)
        {
            return await _contexto.Calificaciones
                .Where(c => c.IdCurso == idCurso)
                .Include(c => c.IdEstudianteNavigation)
                .ToListAsync();
        }
    }
}
