using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Data;
using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;

namespace SistemaParqueadero.Repositories.Implementations
{
    public class ParqueoRepository : IParqueoRepository
    {
        private readonly ApplicationDbContext _context;

        public ParqueoRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Parqueo>> GetAllParqueos() 
        {
            return await _context.Parqueos.ToListAsync();
        }

        public async Task<Parqueo?> GetParqueoById(int id) 
        {
            return await _context.Parqueos.FindAsync(id);
        }

        public async Task<IEnumerable<Parqueo>> GetParqueoByEstado(string estado)
        {
            var estadoEnum = Enum.Parse<EstadoParqueo>(char.ToUpper(estado[0]) + estado.Substring(1).ToLower());
            return await _context.Parqueos.Where(p => p.Estado == estadoEnum).ToListAsync();
        }

        public async Task AddParqueo(Parqueo parqueo) 
        {
            await _context.Parqueos.AddAsync(parqueo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParqueo(Parqueo parqueo) 
        {
            _context.Parqueos.Update(parqueo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteParqueo(int id) 
        {
            var parqueo = await GetParqueoById(id);
            if (parqueo != null) 
            {
                _context.Parqueos.Remove(parqueo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
