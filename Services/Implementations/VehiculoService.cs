using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Services.Implementations
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoService(IVehiculoRepository vehiculoRepository) 
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllVehiculos() 
        {
            var vehiculos = await _vehiculoRepository.GetAllVehiculos();
            if (!vehiculos.Any()) 
            {
                throw new KeyNotFoundException("No hay vehiculos registrados en el sistema.");
            }
            return vehiculos;
        }

        public async Task<Vehiculo?> GetVehiculoById(int id) 
        {
            var vehiculo = await _vehiculoRepository.GetVehiculoById(id);
            if (vehiculo == null) 
            {
                throw new KeyNotFoundException($"El vehiculo con id: {id} no se encuentra en la base de datos");
            }
            return vehiculo;
        } 

        public async Task<Vehiculo?> GetVehiculoByPlaca(string placa) 
        {
            if (string.IsNullOrWhiteSpace(placa)) 
            {
                throw new ArgumentException("Debes ingresar una placa para poder buscarla.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(placa, @"^[A-Za-z0-9]+$")) 
            {
                throw new ArgumentException("La placa contiene caracteres no validos");
            }

            var vehiculo = await _vehiculoRepository.GetVehiculoByPlaca(placa);
            if (vehiculo == null)
            {
                throw new KeyNotFoundException($"El vehiculo con placa {placa} no se encuentra registrado");
            }

            return vehiculo;
        }

        public async Task AddVehiculo(Vehiculo vehiculo) 
        {
            if (vehiculo == null)
            {
                throw new ArgumentNullException("El vehiculo no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(vehiculo.Placa)) 
            {
                throw new ArgumentException("Debes ingresar un valor valido para la placa del vehiculo");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(vehiculo.Placa, @"^[A-Za-z0-9]+$")) 
            {
                throw new ArgumentException("La placa contiene caracteres no validos");
            }

            await _vehiculoRepository.AddVehiculo(vehiculo);
        }

        public async Task UpdateVehiculo(Vehiculo vehiculo) 
        {
            if (vehiculo == null)
            {
                throw new ArgumentNullException("El vehiculo no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(vehiculo.Placa))
            {
                throw new ArgumentException("Debes ingresar un valor valido para la placa del vehiculo");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(vehiculo.Placa, @"^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("La placa contiene caracteres no validos");
            }

            var vehiculoExistente = await _vehiculoRepository.GetVehiculoById(vehiculo.Id);
            if (vehiculoExistente == null) 
            {
                throw new KeyNotFoundException($"El vehiculo con placa {vehiculo.Placa} no se encuentra registrado");
            }

            await _vehiculoRepository.UpdateVehiculo(vehiculo);
        }

        public async Task DeleteVehiculo(int id) 
        {
            var vehiculoExistente = await _vehiculoRepository.GetVehiculoById(id);
            if (vehiculoExistente == null) 
            {
                throw new KeyNotFoundException("No es posible eliminar el vehiculo ya que este no se encuentra registrado");
            }

            await _vehiculoRepository.DeleteVehiculo(id);
        }
    }
}
