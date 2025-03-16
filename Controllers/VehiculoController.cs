using Microsoft.AspNetCore.Mvc;
using SistemaParqueadero.Models;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public VehiculoController(IVehiculoService vehiculoService) 
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetAllVehiculos() 
        {
            try
            {
                var vehiculos = await _vehiculoService.GetAllVehiculos();
                return Ok(vehiculos);
            }
            catch (Exception e) 
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculoById(int id) 
        {
            try
            {
                var vehiculo = await _vehiculoService.GetVehiculoById(id);
                return Ok(vehiculo);
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

        [HttpGet("placa/{placa}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculoByPlaca(string placa) 
        {
            try
            {
                var vehiculo = await _vehiculoService.GetVehiculoByPlaca(placa);
                return Ok(vehiculo);
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

        [HttpPost]
        public async Task<ActionResult> AddVehiculo([FromBody]Vehiculo vehiculo) 
        {
            try
            {
                await _vehiculoService.AddVehiculo(vehiculo);
                return CreatedAtAction(nameof(GetVehiculoById), new { id = vehiculo.Id }, vehiculo);
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
        public async Task<ActionResult> UpdateVehiculo([FromBody] Vehiculo vehiculo) 
        {
            try
            {
                await _vehiculoService.UpdateVehiculo(vehiculo);
                return Ok("Vehiculo Actualizado correctamente");
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehiculo(int id) 
        {
            try
            {
                await _vehiculoService.DeleteVehiculo(id);
                return Ok("Vehiculo eliminado correctamente");
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
