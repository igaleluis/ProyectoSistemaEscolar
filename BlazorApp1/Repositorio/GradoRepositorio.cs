using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Repositorio
{
    public class GradoRepositorio : IGradoRepositorio
    {
        ApplicationDbContext _contexto;
        public GradoRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<Grado> ActualizarGrado(int idGrado, Grado grado)
        {
            var gradoDesdeDb = await _contexto.Grados.FirstOrDefaultAsync(g => g.IdGrado == idGrado);
            if (gradoDesdeDb != null)
            {
                gradoDesdeDb.Nombre = grado.Nombre;
                gradoDesdeDb.Nivel = grado.Nivel;
                await _contexto.SaveChangesAsync();
                return gradoDesdeDb;
            }
            else
            {
                throw new Exception($"Grado con ID {idGrado} no encontrado.");
            }
        }

        public async Task<Grado> CrearGrado(Grado grado)
        {
            if (grado != null)
            {
                await _contexto.Grados.AddAsync(grado);
                await _contexto.SaveChangesAsync();
                return grado;
            }
            else
            {
                throw new Exception("El objeto Grado proporcionado es nulo.");
            }
        }

        public async Task EliminarGrado(int idGrado)
        {
            var gradoDesdeDb = await _contexto.Grados.FirstOrDefaultAsync(g => g.IdGrado == idGrado);
            if (gradoDesdeDb != null)
            {
                _contexto.Grados.Remove(gradoDesdeDb);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<Grado> GetGradoId(int idGrado)
        {
            var grado = await _contexto.Grados
                .Include(g => g.Cursos)
                .Include(g => g.Estudiantes)
                .FirstOrDefaultAsync(g => g.IdGrado == idGrado);

            if (grado == null)
            {
                throw new Exception($"Grado con ID {idGrado} no encontrado.");
            }
            else
            {
                return grado;
            }
        }

        public async Task<List<Grado>> GetGrados()
        {
            return await _contexto.Grados
                .Include (g => g.Cursos)
                .Include (g=> g.Estudiantes)
                .ToListAsync();

        }
    }
}
