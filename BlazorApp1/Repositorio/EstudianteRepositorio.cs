using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp1.Repositorio

{
    public class EstudianteRepositorio : IEstudianteRepositorio
    {
        ApplicationDbContext _contexto;
        public EstudianteRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // Implementación del método CrearEstudiante
        public async Task<Estudiante> CrearEstudiante(Estudiante estudiante)
        {
            if (estudiante != null)
            {
                await _contexto.Estudiantes.AddAsync(estudiante);
                await _contexto.SaveChangesAsync();
                return estudiante;
            }
            else
            {
                throw new ArgumentNullException(nameof(estudiante), "El estudiante no puede ser nulo.");
            }
        }

        // Implementación del método EliminarEstudiante
        public async Task EliminarEstudiante(int idEstudiante)
        {
            var estudianteDesdeDb = await _contexto.Estudiantes.FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);
            if (estudianteDesdeDb != null)
            {
                _contexto.Estudiantes.Remove(estudianteDesdeDb);
                await _contexto.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró un estudiante con IdEstudiante {idEstudiante}.");
            }
        }

        // Implementación del método GetEstudianteId
        public async Task<Estudiante> GetEstudianteId(int idEstudiante)
        {
            var estudiante = await _contexto.Estudiantes
                .Include(e => e.IdUsuarioNavigation)
                .Include(e => e.IdGradoNavigation)
                    .ThenInclude(g => g.Cursos)
                .Include(e => e.Calificaciones)
                    .ThenInclude(c => c.IdCursoNavigation)
                .FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);

            if (estudiante == null)
                throw new KeyNotFoundException($"No se encontró el estudiante");

            return estudiante;
        }

        //Implementación del método GetEstudiantes
        public async Task<List<Estudiante>> GetEstudiantes()
        {
            return await _contexto.Estudiantes
                .Include(e => e.IdUsuarioNavigation)
                .Include(e => e.IdGradoNavigation)
                .ToListAsync();
        }

        //Implementacion metodo getusuarios segun estudiantes
        public async Task<List<Usuario>> GetUsuarioDisponibleEstudiante()
        {
            return await _contexto.Usuarios
                .Where(u => u.Rol == "Estudiante" &&
                !_contexto.Estudiantes.Any(e => e.IdUsuario == u.IdUsuario)
                ).ToListAsync();
        }

        
    }
}
