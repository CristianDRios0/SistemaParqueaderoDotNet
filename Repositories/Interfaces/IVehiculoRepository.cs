using SistemaParqueadero.Models;

namespace SistemaParqueadero.Repositories.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<IEnumerable<Vehiculo>> GetAllVehiculos();
        Task<Vehiculo?> GetVehiculoByPlaca(string placa);
        Task AddVehiculo(Vehiculo vehiculo);
        Task UpdateVehiculo(Vehiculo vehiculo);
        Task DeleteVehiculo(int id);
    }
}
