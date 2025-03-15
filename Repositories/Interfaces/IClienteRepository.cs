using SistemaParqueadero.Models;

namespace SistemaParqueadero.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente?> GetClienteById(int id);
        Task<Cliente?> GetClienteByDocument(int identificacion);
        Task AddCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}
