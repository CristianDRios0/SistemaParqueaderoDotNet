using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Services.Implementations
{
    public class TarifaService : ITarifaService
    {
        private readonly ITarifaRepository _tarifaRepository;

        public TarifaService(ITarifaRepository tarifaRepository) 
        {
            _tarifaRepository = tarifaRepository;
        }

        public async Task<IEnumerable<Tarifa>> GetAllTarifas() 
        {
            var tarifas = await _tarifaRepository.GetAllTarifas();
            if (!tarifas.Any()) 
            { 
                throw new KeyNotFoundException("No hay tarifas registradas en el sistema.");
            }
            return tarifas;
        }

        public async Task<Tarifa?> GetTarifaById(int id) 
        {
            var tarifa = await _tarifaRepository.GetTarifaById(id);
            if (tarifa == null)
            {
                throw new KeyNotFoundException($"La tarifa con id: {id} no se encuentra en la base de datos");
            }
            return tarifa;
        }

        public async Task<IEnumerable<Tarifa>> GetTarifaByTipo(string tipo) 
        { 
            if (string.IsNullOrEmpty(tipo))
            {
                throw new ArgumentNullException("El tipo de tarifa no puede ser nulo o vacío.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(tipo, @"^[A-Za-z]+$")) 
            {
                throw new ArgumentException("El tipo de tarifa contiene caracteres no validos");
            }

            var tipoEnum = new HashSet<string> { "hora", "menssual" };
            if (!tipoEnum.Contains(tipo.ToLower())) 
            {
                throw new ArgumentException("El tipo buscado no es correcto, debe ser hora o mensual");
            }

            return await _tarifaRepository.GetTarifaByTipo(tipo);
        }

        public async Task<IEnumerable<Tarifa>> GetTarifaByVehiculoTipo(string vehiculoTipo) 
        {
            if (string.IsNullOrWhiteSpace(vehiculoTipo)) 
            {
                throw new ArgumentException("El tipo de vehiculo no puede ser nulo o vacio");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(vehiculoTipo, @"^[A-Za-z]+$")) 
            {
                throw new ArgumentException("El tipo de vehiculo contiene caracteres no permitidos");
            }

            var vehiculoTipoEnum = new HashSet<string> { "automovil", "moto" };
            if (!vehiculoTipoEnum.Contains(vehiculoTipo.ToLower())) 
            {
                throw new ArgumentException("El tipo de vehiculo buscado no es permitido, debe ser automovil o moto");
            }

            return await _tarifaRepository.GetTarifaByVehiculoTipo(vehiculoTipo);
        }

        public async Task AddTarifa(Tarifa tarifa) 
        {
            if (tarifa == null) 
            {
                throw new ArgumentNullException("El valor de Tarifa no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(tarifa.Tipo.ToString().ToLower())) 
            {
                throw new ArgumentException("El tipo de tarifa no puede contener espacio o ser un parametro vacio");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(tarifa.Tipo.ToString(), @"^[A-Za-z]+$")) 
            {
                throw new ArgumentException("El tipo que se esta enviando contiene valores no validos, debe ser hora o mensual");
            }
            if (string.IsNullOrWhiteSpace(tarifa.VehiculoTipo.ToString().ToLower())) 
            {
                throw new ArgumentException("El tipo de vehiculo no puede contener espacio o ser un parametro vacio");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(tarifa.VehiculoTipo.ToString(), @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("El tipo de vehiculo que se esta enviando contiene valores no validos, debe ser automovil o moto");
            }
            if (tarifa.Monto <= 0)
            {
                throw new ArgumentException("El valor de la tarifa no puede ser menor o igual a 0");
            }

            await _tarifaRepository.AddTarifa(tarifa);
        }

        public async Task UpdateTarifa(Tarifa tarifa)
        {
            if (tarifa == null)
            {
                throw new ArgumentNullException("El valor de Tarifa no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(tarifa.Tipo.ToString().ToLower()))
            {
                throw new ArgumentException("El tipo de tarifa no puede contener espacio o ser un parametro vacio");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(tarifa.Tipo.ToString(), @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("El tipo que se esta enviando contiene valores no validos, debe ser hora o mensual");
            }
            if (string.IsNullOrWhiteSpace(tarifa.VehiculoTipo.ToString().ToLower()))
            {
                throw new ArgumentException("El tipo de vehiculo no puede contener espacio o ser un parametro vacio");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(tarifa.VehiculoTipo.ToString(), @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("El tipo de vehiculo que se esta enviando contiene valores no validos, debe ser automovil o moto");
            }
            if (tarifa.Monto <= 0)
            {
                throw new ArgumentException("El valor de la tarifa no puede ser menor o igual a 0");
            }
            await _tarifaRepository.UpdateTarifa(tarifa);
        }

        public async Task DeleteTarifa(int id) 
        {
            var tarifa = await GetTarifaById(id);
            if (tarifa == null) 
            {
                throw new KeyNotFoundException($"La tarifa con id: {id} no se encuentra en la base de datos");
            }
            await _tarifaRepository.DeleteTarifa(id);
        }
    }
}
