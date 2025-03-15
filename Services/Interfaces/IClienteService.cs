using SistemaParqueadero.Models;

namespace SistemaParqueadero.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente?> GetClienteById(int id);
        Task<Cliente?> GetClienteByDocument(int identificacion);
        Task AddCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}
