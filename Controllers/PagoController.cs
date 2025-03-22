using Microsoft.AspNetCore.Mvc;
using SistemaParqueadero.Models;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService) 
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetAllPagos() 
        {
            try
            {
                return Ok(await _pagoService.GetAllPagos());
            }
            catch (KeyNotFoundException ke)
            {
                return NotFound(ke.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPagoById(int id) 
        {
            try
            {
                return Ok(await _pagoService.GetPagoById(id));
            }
            catch (KeyNotFoundException ke)
            {
                return NotFound(ke.Message);
            }
            catch (Exception e) 
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddParqueo(Pago pago) 
        {
            try
            {
                await _pagoService.AddPago(pago);
                return Ok("Pago creado correctamente");
            }
            catch (ArgumentException ae) 
            {
                return BadRequest(ae.Message);
            }
            catch (KeyNotFoundException ke)
            {
                return NotFound(ke.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePago(Pago pago) 
        {
            try
            {
                await _pagoService.UpdatePago(pago);
                return Ok("Pago Actualizado correctamente");
            }
            catch (InvalidOperationException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePago(int id) 
        {
            try
            {
                await _pagoService.DeletePago(id);
                return Ok("Pago Eliminado correctamente");
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
