using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Services.Implementations
{
    public class ParqueoService : IParqueoService
    {

        private readonly IParqueoRepository _parqueoRepository;
        private readonly ICeldaRepository _celdaRepository;

        public ParqueoService(IParqueoRepository parqueoRepository, ICeldaRepository celdaRepository) 
        {
            _parqueoRepository = parqueoRepository;
            _celdaRepository = celdaRepository;
        }

        public async Task<IEnumerable<Parqueo>> GetAllParqueos() 
        {
            var parqueos = await _parqueoRepository.GetAllParqueos();
            if (!parqueos.Any())
            {
                throw new KeyNotFoundException("No hay parqueos registrados en la BD");
            }
            return parqueos;
        }

        public async Task<Parqueo?> GetParqueoById(int id) 
        {
            var parqueo = await _parqueoRepository.GetParqueoById(id);
            if (parqueo == null)
            {
                throw new KeyNotFoundException($"No se encontró el parqueo con el id: {id} proporcionado");
            }
            return parqueo;
        }

        public async Task<IEnumerable<Parqueo>> GetParqueoByEstado(string estado) 
        {
            if (string.IsNullOrWhiteSpace(estado)) 
            {
                throw new ArgumentException("El estado no puede contener espacios en blanco o enviar un estado en blanco");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(estado, @"^[A-Za-z]+$")) 
            {
                throw new ArgumentException("El estado contiene caracteres no permitidos");
            }

            var parqueoEstado = new HashSet<string> { "activo", "finalizado" };
            if (!parqueoEstado.Contains(estado.ToLower()))
            {
                throw new ArgumentException("El estado proporcionado no es correcto, debe ser activo o finalizado");
            }

            return await _parqueoRepository.GetParqueoByEstado(estado);
        }

        public async Task AddParqueo(Parqueo parqueo) 
        {
            if (string.IsNullOrWhiteSpace(parqueo.Estado.ToString().ToLower())) 
            {
                throw new ArgumentNullException("El estado no puede contener espacios en blanco o enviar un estado en blanco");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(parqueo.Estado.ToString().ToLower(), @"^[A-Za-z]+$")) 
            {
                throw new ArgumentException("El estado contiene caracteres no permitidos");
            }

            var parqueoEstado = new HashSet<string> { "activo", "finalizado" };
            if (!parqueoEstado.Contains(parqueo.Estado.ToString().ToLower()))
            {
                throw new ArgumentException("El estado proporcionado no es correcto, debe ser activo o finalizado");
            }

            if (parqueo.CeldaId == 0)
            {
                throw new ArgumentNullException("El id de la celda no puede ser 0");
            }

            var zonaColombia = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            parqueo.FechaEntrada = TimeZoneInfo.ConvertTime(parqueo.FechaEntrada, zonaColombia);

            var estadoCelda = await _celdaRepository.GetCeldaById(parqueo.CeldaId);
            if (estadoCelda != null)
            {
                if (estadoCelda.Estado.ToString().ToLower() == "ocupado" || estadoCelda.Estado.ToString().ToLower() == "reservado") 
                {   
                    throw new InvalidDataException("La celda ya se encuentra ocupada o reservada");
                }
                estadoCelda.Estado = EstadoCelda.Ocupado;
                await _celdaRepository.UpdateCelda(estadoCelda);
            }
            await _parqueoRepository.AddParqueo(parqueo);
        }

        public async Task UpdateParqueo(Parqueo parqueo) 
        {
            if (string.IsNullOrWhiteSpace(parqueo.Estado.ToString().ToLower()))
            {
                throw new ArgumentNullException("El estado no puede contener espacios en blanco o enviar un estado en blanco");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(parqueo.Estado.ToString().ToLower(), @"^[A-Za-z]+$"))
            {
                throw new ArgumentException("El estado contiene caracteres no permitidos");
            }

            var parqueoEstado = new HashSet<string> { "activo", "finalizado" };
            if (!parqueoEstado.Contains(parqueo.Estado.ToString().ToLower()))
            {
                throw new ArgumentException("El estado proporcionado no es correcto, debe ser activo o finalizado");
            }

            var celdaExistente = await _celdaRepository.GetCeldaById(parqueo.CeldaId);

            if (celdaExistente == null)
            {
                throw new ArgumentNullException("La celda que deseas asignar no existe");
            }

            if (parqueo.Estado == EstadoParqueo.Finalizado)
            {
                celdaExistente.Estado = EstadoCelda.Libre;
                await _celdaRepository.UpdateCelda(celdaExistente);
                Console.WriteLine($"Celda {celdaExistente.Id} marcada como LIBRE");
            }

            await _parqueoRepository.UpdateParqueo(parqueo);
        }

        public async Task DeleteParqueo(int id) 
        {
            var parqueo = await _parqueoRepository.GetParqueoById(id);
            if (parqueo == null)
            {
                throw new KeyNotFoundException($"No se encontró el parqueo con el id: {id} proporcionado");
            }
            await _parqueoRepository.DeleteParqueo(id);
        }
    }
}
