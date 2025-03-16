using SistemaParqueadero.Models;

namespace SistemaParqueadero.Services.Interfaces
{
    public interface IVehiculoService
    {
        Task<IEnumerable<Vehiculo>> GetAllVehiculos();
        Task<Vehiculo?> GetVehiculoById(int id);
        Task<Vehiculo?> GetVehiculoByPlaca(string placa);
        Task AddVehiculo(Vehiculo vehiculo);
        Task UpdateVehiculo(Vehiculo vehiculo);
        Task DeleteVehiculo(int id);
    }
}
