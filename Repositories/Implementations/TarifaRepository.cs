using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Data;
using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;

namespace SistemaParqueadero.Repositories.Implementations
{
    public class TarifaRepository : ITarifaRepository
    {
        private readonly ApplicationDbContext _context;

        public TarifaRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarifa>> GetAllTarifas() 
        {
            return await _context.Tarifas.ToListAsync();
        }

        public async Task<Tarifa?> GetTarifaById(int id) 
        {
            return await _context.Tarifas.FindAsync(id);
        }

        public async Task<IEnumerable<Tarifa>> GetTarifaByTipo(string tipo) 
        { 
            var tipoEnum = Enum.Parse<TipoTarifa>(char.ToUpper(tipo[0]) + tipo.Substring(1).ToLower());
            return await _context.Tarifas.Where(t => t.Tipo == tipoEnum).ToListAsync();
        }

        public async Task<IEnumerable<Tarifa>> GetTarifaByVehiculoTipo(string vehiculoTipo) 
        {
            var vehiculoTipoEnum = Enum.Parse<TipoVehiculo>(char.ToUpper(vehiculoTipo[0]) + vehiculoTipo.Substring(1).ToLower());
            return await _context.Tarifas.Where(vt => vt.VehiculoTipo == vehiculoTipoEnum).ToListAsync();
        }

        public async Task AddTarifa(Tarifa tarifa) 
        {
            await _context.Tarifas.AddAsync(tarifa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTarifa(Tarifa tarifa) 
        {
            _context.Tarifas.Update(tarifa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTarifa(int id) 
        {
            var tarifa = await GetTarifaById(id);
            if (tarifa != null) 
            {
                _context.Tarifas.Remove(tarifa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
