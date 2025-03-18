using SistemaParqueadero.Models;

namespace SistemaParqueadero.Repositories.Interfaces
{
    public interface ITarifaRepository
    {
        Task<IEnumerable<Tarifa>> GetAllTarifas();
        Task<Tarifa?> GetTarifaById(int id);
        Task<IEnumerable<Tarifa>> GetTarifaByTipo(string tipo);
        Task<IEnumerable<Tarifa>> GetTarifaByVehiculoTipo(string vehiculoTipo);
        Task AddTarifa(Tarifa tarifa);
        Task UpdateTarifa(Tarifa tarifa);
        Task DeleteTarifa(int id);
    }
}
