using SistemaParqueadero.Models;
using SistemaParqueadero.Repositories.Interfaces;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Services.Implementations
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IParqueoService _parqueoService;
        private readonly ITarifaRepository _tarifaRepository;

        public PagoService(IPagoRepository pagoRepository, IClienteRepository clienteRepository, IParqueoService parqueoService, ITarifaRepository tarifaRepository) 
        {
            _pagoRepository = pagoRepository;
            _clienteRepository = clienteRepository;
            _parqueoService = parqueoService;
            _tarifaRepository = tarifaRepository;
        }

        public async Task<IEnumerable<Pago>> GetAllPagos() 
        {
            var pagos = await _pagoRepository.GetAllPagos();
            if (pagos == null) 
            {
                throw new KeyNotFoundException("No hay pagos registrados en el sistema.");
            }
            return pagos;
        }

        public async Task<Pago?> GetPagoById(int id) 
        {
            var pago = await _pagoRepository.GetPagoById(id);
            if (pago == null)
            {
                throw new KeyNotFoundException($"No se encontró un pago con el id {id}.");
            }
            return pago;
        }

        public async Task AddPago(Pago pago) 
        {
            if (pago.ClienteId == null) 
            {
                throw new ArgumentNullException("El cliente es obligatorio para poder registrar el pago");
            }

            var cliente = await _clienteRepository.GetClienteById(pago.ClienteId.Value);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"No se encontró un cliente con el id {pago.ClienteId}");
            }

            if (pago.ParqueoId == null)
            {
                throw new ArgumentNullException("El parqueo es obligatorio para poder registrar el pago");
            }

            var parqueo = await _parqueoService.GetParqueoById(pago.ParqueoId.Value);
            if (parqueo == null) 
            {
                throw new KeyNotFoundException($"No se encontró un parqueo con el id {pago.ParqueoId}");
            }

            var tarifa = await _tarifaRepository.GetTarifaById(parqueo.TarifaId);
            if (tarifa != null)
            {
                var zonaColombia = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                var fechaInicioparqueo = TimeZoneInfo.ConvertTime(parqueo.FechaEntrada, zonaColombia);
                var fechaFinParqueo = TimeZoneInfo.ConvertTime(DateTime.Now,zonaColombia);
                var tiempoParqueo = fechaFinParqueo - fechaInicioparqueo;
                var totalMinutosParqueo = tiempoParqueo.TotalMinutes;
                var valorMinutoParqueo = tarifa.Monto / 60;
                var valorFinal = totalMinutosParqueo * valorMinutoParqueo;

                if (tarifa.Tipo == TipoTarifa.Hora)
                {
                    parqueo.TotalPagado = (int)Math.Round(valorFinal);
                    parqueo.FechaSalida = fechaFinParqueo;
                    parqueo.Estado = EstadoParqueo.Finalizado;
                    pago.Monto = (int)Math.Round(valorFinal);
                }

                if (tarifa.Tipo == TipoTarifa.Mensual)
                {
                    parqueo.TotalPagado = tarifa.Monto;
                    parqueo.FechaSalida = fechaFinParqueo;
                    parqueo.Estado = EstadoParqueo.Finalizado;
                    pago.Monto = tarifa.Monto;
                }

                await _parqueoService.UpdateParqueo(parqueo);
            }
            else 
            {
                throw new ArgumentException("No es posible asignar tarifa a este pago ya que no se encontro en la base de datos");
            }

            await _pagoRepository.AddPago(pago);

        }

        public async Task UpdatePago(Pago pago) 
        {
            var pagoExistente = await _pagoRepository.GetPagoById(pago.Id);
            if (pagoExistente == null) 
            {
                throw new InvalidOperationException("No es posible actualizar un pago que no existe");
            }

            pago.Monto = pagoExistente.Monto;

            if (pago.ParqueoId != null) 
            {
                pagoExistente.ParqueoId = pago.ParqueoId;
            }
            if (pago.ClienteId != null) 
            {
                pagoExistente.ClienteId = pago.ClienteId;
            }

            await _pagoRepository.UpdatePago(pagoExistente);
        }

        public async Task DeletePago(int id) 
        {
            var pago = await GetPagoById(id);
            if (pago == null) 
            {
                throw new ArgumentException($"El pago con id: {id} no existe, por lo que no es posible eliminarlo");
            }

            await _pagoRepository.DeletePago(id);
        }
    }
}
