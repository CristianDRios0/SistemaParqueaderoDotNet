using SistemaParqueadero.Models;

namespace SistemaParqueadero.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAllPagos();
        Task<Pago?> GetPagoById(int id);
        Task AddPago(Pago pago);
        Task UpdatePago(Pago pago);
        Task DeletePago(int id);
    }
}
