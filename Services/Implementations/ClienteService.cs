using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository) 
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _clienteRepository.GetAllClientes();
        }
        public async Task<Cliente?> GetClienteById(int id)
        {
            return await _clienteRepository.GetClienteById(id);
        }
        public async Task<Cliente?> GetClienteByDocument(int identificacion)
        {
            return await _clienteRepository.GetClienteByDocument(identificacion);
        }
        public async Task AddCliente(Cliente cliente)
        {
            await _clienteRepository.AddCliente(cliente);
        }
        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            return await _clienteRepository.UpdateCliente(cliente);
        }
        public async Task<bool> DeleteCliente(int id)
        {
            return await _clienteRepository.DeleteCliente(id);
        }
    }
}
