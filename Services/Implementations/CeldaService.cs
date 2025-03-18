using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Services.Implementations
{
    public class CeldaService : ICeldaService
    {
        private readonly ICeldaRepository _celdaRepository;

        public CeldaService(ICeldaRepository celdaRepository) 
        {
            _celdaRepository = celdaRepository;
        }

        public async Task<IEnumerable<Celda>> GetAllCeldas() 
        {
            var celdas = await _celdaRepository.GetAllCeldas();
            if (!celdas.Any()) 
            {
                throw new KeyNotFoundException("No hay celdas registradas en el sistema.");
            }
            return celdas;
        }
        public async Task<Celda?> GetCeldaById(int id) 
        {
            var celda = await _celdaRepository.GetCeldaById(id);
            if (celda == null) 
            {
                throw new KeyNotFoundException($"La celda con id: {id} no se encuentra en la base de datos");
            }
            return celda;
        }

        public async Task<Celda?> GetCeldaByCodigo(string codigo) 
        {
            if (string.IsNullOrWhiteSpace(codigo)) 
            {
                throw new ArgumentException("El código de la celda no puede ser nulo o vacío.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(codigo, @"^[A-Za-z0-9]+$")) 
            {
                throw new ArgumentException("El código de la celda contiene caracteres no validos");
            }

            var celda = await _celdaRepository.GetCeldaByCodigo(codigo);
            if (celda == null)
            {
                throw new KeyNotFoundException($"La celda con código {codigo} no se encuentra registrada");
            }
            return celda;
        }

        public async Task<IEnumerable<Celda>> GetCeldaByEstado(string estado) 
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new ArgumentException("El estado de la celda no puede ser nulo o vacío.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(estado, @"^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("El estado de la celda contiene caracteres no validos");
            }

            var estadosValidos = new HashSet<string> { "libre", "ocupado", "reservado" };
            if (!estadosValidos.Contains(estado.ToLower())) 
            {
                throw new ArgumentException("El estado de la celda no es válido.");
            }

            return await _celdaRepository.GetCeldaByEstado(estado.ToLower());
        }

        public async Task AddCelda(Celda celda) 
        {
            if (celda == null) 
            {
                throw new ArgumentNullException("La celda no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(celda.Codigo)) 
            {
                throw new ArgumentException("El código de la celda no puede ser nulo o vacío.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(celda.Codigo, @"^[A-Za-z0-9]+$")) 
            {
                throw new ArgumentException("El código de la celda contiene caracteres no validos");
            }

            var celdaExistente = await _celdaRepository.GetCeldaByCodigo(celda.Codigo);
            if (celdaExistente != null)
            {
                throw new DuplicateWaitObjectException($"La celda con código {celda.Codigo} ya se encuentra registrada en el sistema.");
            }

            var tipoValido = new HashSet<string> { "automovil", "moto" };
            if (!tipoValido.Contains(celda.Tipo.ToString().ToLower())) 
            {
                throw new ArgumentException("El tipo de no es valido, debe ser automovil o moto");
            }

            var estadoValido = new HashSet<string> { "libre", "ocupado", "reservado" };
            if (!estadoValido.Contains(celda.Estado.ToString().ToLower())) 
            {
                throw new ArgumentException("El estado de la celda solo puede libre, ocupado o reservado");
            }

            await _celdaRepository.AddCelda(celda);
        }

        public async Task UpdateCelda(Celda celda) 
        {
            if (celda == null)
            {
                throw new ArgumentNullException("La celda no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(celda.Codigo))
            {
                throw new ArgumentException("El código de la celda no puede ser nulo o vacío.");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(celda.Codigo, @"^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("El código de la celda contiene caracteres no validos");
            }

            var celdaExistente = await _celdaRepository.GetCeldaByCodigo(celda.Codigo);
            if (celdaExistente != null)
            {
                throw new DuplicateWaitObjectException($"La celda con código {celda.Codigo} ya se encuentra registrada en el sistema.");
            }

            var tipoValido = new HashSet<string> { "automovil", "moto" };
            if (!tipoValido.Contains(celda.Tipo.ToString().ToLower()))
            {
                throw new ArgumentException("El tipo de no es valido, debe ser automovil o moto");
            }

            var estadoValido = new HashSet<string> { "libre", "ocupado", "reservado" };
            if (!estadoValido.Contains(celda.Estado.ToString().ToLower()))
            {
                throw new ArgumentException("El estado de la celda solo puede libre, ocupado o reservado");
            }

            await _celdaRepository.UpdateCelda(celda);
        }

        public async Task DeleteCelda(int id) 
        {
            var celda = await _celdaRepository.GetCeldaById(id);
            if (celda == null) 
            { 
                throw new KeyNotFoundException($"La celda con id: {id} no se encuentra en la base de datos");
            }
            await _celdaRepository.DeleteCelda(id);
        }
    }
}
