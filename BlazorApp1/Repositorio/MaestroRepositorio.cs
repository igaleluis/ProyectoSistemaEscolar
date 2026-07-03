using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp1.Repositorio
{
    public class MaestroRepositorio : IMaestroRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public MaestroRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // Implementación del método CrearMaestro
        public async Task<Maestro> CrearMaestro(Maestro maestro)
        {

            if(maestro != null)
            {
             await   _contexto.Maestros.AddAsync(maestro);
             await   _contexto.SaveChangesAsync();
                return maestro;
            }
            else
            {
                throw new ArgumentNullException(nameof(maestro), "El maestro no puede ser nulo.");
            }
        }

        // Implementación del método EliminarMaestro
        public async Task EliminarMaestro(int idMaestro)
        {
            var maestroDesdeDb = await _contexto.Maestros.FirstOrDefaultAsync(m => m.IdMaestro == idMaestro);
            if (maestroDesdeDb != null)
            {
                _contexto.Maestros.Remove(maestroDesdeDb);
                await _contexto.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró un maestro con IdMaestro {idMaestro}.");
            }
        }

        // Implementación del método GetMaestroId
        public async Task<Maestro> GetMaestroId(int idMaestro)
        {
            var maestro = await _contexto.Maestros
                .Include(m => m.IdUsuarioNavigation)
                .Include(m => m.Cursos)
                .FirstOrDefaultAsync(m => m.IdMaestro == idMaestro);
            if (maestro == null)
                throw new KeyNotFoundException($"No se encontró un maestro con Id {idMaestro}");

            return maestro;
        }

        public async Task<List<Maestro>> GetMaestros()
        {
            return await _contexto.Maestros
                .Include(m => m.IdUsuarioNavigation)
                .Include(m => m.Cursos)
                .ToListAsync();
        }

        public async Task<List<Usuario>> GetUsuarioDisponibleMaestro()
        {
            return await _contexto.Usuarios
                .Where(u => u.Rol == "Maestro" &&
                            !_contexto.Maestros.Any(m => m.IdUsuario == u.IdUsuario))
                .ToListAsync();
        }

    }
}
