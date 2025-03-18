using SistemaParqueadero.Models;

namespace SistemaParqueadero.Repositories.Interfaces
{
    public interface ICeldaRepository
    {
        Task<IEnumerable<Celda>> GetAllCeldas();
        Task<Celda?> GetCeldaById(int id);
        Task<Celda?> GetCeldaByCodigo(string codigo);
        Task<IEnumerable<Celda>> GetCeldaByEstado(string estado);
        Task AddCelda(Celda celda);
        Task UpdateCelda(Celda celda);
        Task DeleteCelda(int id);
    }
}
