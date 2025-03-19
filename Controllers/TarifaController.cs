using Microsoft.AspNetCore.Mvc;
using SistemaParqueadero.Models;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarifaController : ControllerBase
    {
        private readonly ITarifaService _tarifaService;

        public TarifaController(ITarifaService tarifaService) 
        {
            _tarifaService = tarifaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarifa>>> GetAllTarifas() 
        {
            try 
            {
                return Ok(await _tarifaService.GetAllTarifas());
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
        public async Task<ActionResult<Tarifa>> GetTarifaById(int id) 
        {
            try 
            { 
                return Ok(await _tarifaService.GetTarifaById(id));
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

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<Tarifa>>> GetTarifaByTipo(string tipo) 
        {
            try
            {
                return Ok(await _tarifaService.GetTarifaByTipo(tipo));
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

        [HttpGet("vehiculoTipo/{vehiculoTipo}")]
        public async Task<ActionResult<IEnumerable<Tarifa>>> GetTarifaByVehiculoTipo(string vehiculoTipo)
        {
            try
            {
                return Ok(await _tarifaService.GetTarifaByVehiculoTipo(vehiculoTipo));
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

        [HttpPost]
        public async Task<ActionResult> AddTarifa(Tarifa tarifa) 
        {
            try 
            {
                await _tarifaService.AddTarifa(tarifa);
                return Ok("Tarifa registrada exitosamente");
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

        [HttpPut]
        public async Task<ActionResult> UpdateTarifa(Tarifa tarifa) 
        {
            try 
            {
                await _tarifaService.UpdateTarifa(tarifa);
                return Ok("Tarifa actualizada exitosamente");
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTarifa(int id)
        {
            try
            {
                await _tarifaService.DeleteTarifa(id);
                return Ok("Tarifa eliminada exitosamente");
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
    }
}
