using SistemaParqueadero.Models;

namespace SistemaParqueadero.Services.Interfaces
{
    public interface IParqueoService
    {
        Task<IEnumerable<Parqueo>> GetAllParqueos();
        Task<Parqueo?> GetParqueoById(int id);
        Task<IEnumerable<Parqueo>> GetParqueoByEstado(string estado);
        Task AddParqueo(Parqueo parqueo);
        Task UpdateParqueo(Parqueo parqueo);
        Task DeleteParqueo(int id);
    }
}
