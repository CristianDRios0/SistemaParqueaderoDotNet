using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Data;
using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;

namespace SistemaParqueadero.Repositories.Implementations
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly ApplicationDbContext _context;

        public VehiculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllVehiculos()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<Vehiculo?> GetVehiculoById(int id) 
        {
            return await _context.Vehiculos.FindAsync(id);
        }

        public async Task<Vehiculo?> GetVehiculoByPlaca(string placa)
        {
            return await _context.Vehiculos.FirstOrDefaultAsync(v => v.Placa == placa);
        }

        public async Task AddVehiculo(Vehiculo vehiculo)
        {
            await _context.Vehiculos.AddAsync(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehiculo(Vehiculo vehiculo) 
        { 
            _context.Vehiculos.Update(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehiculo(int id)
        {
            var vehiculo = await GetVehiculoById(id);
            if (vehiculo != null) 
            {
                _context.Vehiculos.Remove(vehiculo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
