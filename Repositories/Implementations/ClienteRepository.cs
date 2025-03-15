using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Data;
using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;

namespace SistemaParqueadero.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente?> GetClienteByDocument(int identificacion) 
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Identificacion == identificacion);
        }

        public async Task AddCliente(Cliente cliente) 
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCliente(Cliente cliente) 
        {
            var clienteExistente = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteExistente == null) 
                return false;

            _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCliente(int id) 
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;   
            
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
