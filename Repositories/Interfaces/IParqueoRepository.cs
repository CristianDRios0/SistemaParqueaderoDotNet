using SistemaParqueadero.Models;

namespace SistemaParqueadero.Repositories.Interfaces
{
    public interface IParqueoRepository
    {
        Task<IEnumerable<Parqueo>> GetAllParqueos();
        Task<Parqueo?> GetParqueoById(int id);
        Task<IEnumerable<Parqueo>> GetParqueoByEstado(string estado);
        Task AddParqueo(Parqueo parqueo);
        Task UpdateParqueo(Parqueo parqueo);
        Task DeleteParqueo(int id);
    }
}
