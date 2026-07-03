using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models;
using BlazorApp1.Data;

namespace BlazorApp1.Repositorio
{
    public class CursoRepositorio : ICursoRepositorio
    {
        ApplicationDbContext _contexto;
        public CursoRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // Implementación del método ActualizarCurso
        public async Task<Curso> ActualizarCurso(int idCurso, Curso curso)
        {
            var cursoDesdeDb = await _contexto.Cursos.FirstOrDefaultAsync(c => c.IdCurso == idCurso);
            if (cursoDesdeDb != null)
            {
                cursoDesdeDb.Nombre = curso.Nombre;
                cursoDesdeDb.IdMaestro = curso.IdMaestro;
                await _contexto.SaveChangesAsync();
                return cursoDesdeDb;
            }
            else
            {
                throw new Exception($"Curso con id {idCurso} no encontrado ");
            }
        }


        // Implementación del método CrearCurso
        public async Task<Curso> CrearCurso(Curso curso)
        {
            bool existe = await _contexto.Cursos
            .AnyAsync(c => c.Nombre == curso.Nombre
                    && c.IdGrado == curso.IdGrado);

            if (existe)
            {
                throw new Exception("Ya existe un curso con ese nombre en este grado.");
            }

            await _contexto.Cursos.AddAsync(curso);
            await _contexto.SaveChangesAsync();

            return curso;
        }


        // Implementación del método EliminarCurso
        public async Task EliminarCurso(int idCurso)
        {
            var cursoDesdeDb = await _contexto.Cursos.FirstOrDefaultAsync(c => c.IdCurso == idCurso);
            if (cursoDesdeDb != null)
            {
                _contexto.Cursos.Remove(cursoDesdeDb);
                await _contexto.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Curso con id {idCurso} no encontrado "); 
            }
        }

        // Implementación del método GetCursos
        public async Task<List<Curso>> GetCursos()
        {
            return await _contexto.Cursos
                .Include(c => c.IdGradoNavigation)
                .Include(c => c.IdMaestroNavigation)
                .ToListAsync();
        }

        public async Task<Curso> ObtenerCursoId(int idCurso)
        {
            var curso = await _contexto.Cursos
                .Include(c => c.IdGradoNavigation)
                .Include(c => c.IdMaestroNavigation)
                .FirstOrDefaultAsync(c => c.IdCurso == idCurso);
            if (curso != null){
                return  curso;
            }
            else
            {
                throw new Exception($"Curso con id {idCurso} no encontrado ");
            }
        }

        public async Task<List<Curso>> ObtenerCursosPorGrado(int idGrado)
        {
            var cursos = await _contexto.Cursos
                .Where(c => c.IdGrado == idGrado)
                .Include(c => c.IdMaestroNavigation)
                .ToListAsync();
            return cursos;
        }

        public async Task<List<Curso>> ObtenerCursosPorMaestro(int idMaestro)
        {
            var cursos = await _contexto.Cursos
                .Where(c => c.IdMaestro == idMaestro)
                .Include(c => c.IdGradoNavigation)
                .ToListAsync();
            return cursos;
        }

        public async Task<List<Curso>> ObtenerCursosPorEstudiante(int idEstudiante)
        {
            return await _contexto.Calificaciones
                .Where(cal => cal.IdEstudiante == idEstudiante)
                .Select(cal => cal.IdCursoNavigation)
                .Distinct()
                .Include(c => c.IdGradoNavigation)
                .Include(c => c.IdMaestroNavigation)
                .ToListAsync();
        }
    }
}
