using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Data;
using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;

namespace SistemaParqueadero.Repositories.Implementations
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagoRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> GetAllPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        public async Task<Pago?> GetPagoById(int id) 
        {
            return await _context.Pagos.FindAsync(id);
        }

        public async Task AddPago(Pago pago) 
        {
            await _context.Pagos.AddAsync(pago);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePago(Pago pago) 
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePago(int id) 
        {
            var pago = await GetPagoById(id);
            if (pago != null) 
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }
        }
    }
}
