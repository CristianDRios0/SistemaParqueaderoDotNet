using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Data;
using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;

namespace SistemaParqueadero.Repositories.Implementations
{
    public class CeldaRepository : ICeldaRepository
    {
        private readonly ApplicationDbContext _context;

        public CeldaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Celda>> GetAllCeldas() 
        {
            return await _context.Celdas.ToListAsync();
        }

        public async Task<Celda?> GetCeldaById(int id) 
        {
            return await _context.Celdas.FindAsync(id);
        }

        public async Task<Celda?> GetCeldaByCodigo(string codigo) 
        {
            return await _context.Celdas.FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<IEnumerable<Celda>> GetCeldaByEstado(string estado) 
        {
            var estadoEnum = Enum.Parse<EstadoCelda>(estado);
            return await _context.Celdas.Where(c => c.Estado == estadoEnum).ToListAsync();
        }

        public async Task AddCelda(Celda celda) 
        {
            await _context.Celdas.AddAsync(celda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCelda(Celda celda) 
        {
            _context.Celdas.Update(celda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCelda(int id) 
        {
            var celda = await GetCeldaById(id);
            if (celda != null) 
            {
                _context.Celdas.Remove(celda);
                await _context.SaveChangesAsync();
            }
        }
    }
}
