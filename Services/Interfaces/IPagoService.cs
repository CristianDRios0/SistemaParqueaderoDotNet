using SistemaParqueadero.Models;

namespace SistemaParqueadero.Services.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<Pago>> GetAllPagos();
        Task<Pago?> GetPagoById(int id);
        Task AddPago(Pago pago);
        Task UpdatePago(Pago pago);
        Task DeletePago(int id);
    }
}
