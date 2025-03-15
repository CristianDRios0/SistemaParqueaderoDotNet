using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Models;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService) 
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllClientes() 
        {
            try
            {
                var clientes = await _clienteService.GetAllClientes();
                return Ok(clientes);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id) 
        {
            try
            {
                var cliente = await _clienteService.GetClienteById(id);
                if (cliente == null)
                    return NotFound("Cliente no encontrado");
                return Ok(cliente);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("document/{identificacion}")]
        public async Task<ActionResult<Cliente>> GetClienteByDocument(int identificacion)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByDocument(identificacion);
                if (cliente == null)
                    return NotFound("Cliente no encontrado");
                return Ok(cliente);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCliente([FromBody]Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest("Cliente no puede ser nulo");

                await _clienteService.AddCliente(cliente);
                return CreatedAtAction(nameof(GetClienteById), new { id = cliente.Id }, cliente);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, dbEx.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCliente(int id, [FromBody] Cliente cliente) 
        {
            try 
            {
                if (id != cliente.Id)
                    return BadRequest("El id del cliente no coincide");

                var update = await _clienteService.UpdateCliente(cliente);
                if (!update)
                    return NotFound("Cliente no encontrado");

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            try
            {
                var delete = await _clienteService.DeleteCliente(id);
                if (!delete)
                    return NotFound("Cliente no encontrado");
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
