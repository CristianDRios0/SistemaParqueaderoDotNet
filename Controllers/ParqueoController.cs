using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaParqueadero.Models;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParqueoController : ControllerBase
    {
        private readonly IParqueoService _parqueoService;

        public ParqueoController(IParqueoService parqueoService)
        {
            _parqueoService = parqueoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parqueo>>> GetAllParqueos()
        {
            try
            {
                return Ok(await _parqueoService.GetAllParqueos());
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
        public async Task<ActionResult<Parqueo>> GetParqueoById(int id)
        {
            try
            {
                return Ok(await _parqueoService.GetParqueoById(id));
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

        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Parqueo>>> GetParqueoByEstado(string estado)
        {
            try
            {
                return Ok(await _parqueoService.GetParqueoByEstado(estado));
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
        public async Task<ActionResult> AddParqueo(Parqueo parqueo)
        {
            try
            {
                await _parqueoService.AddParqueo(parqueo);
                return Ok("Parqueo agregado correctamente");
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (InvalidDataException ie)
            {
                return BadRequest(ie.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateParqueo(Parqueo parqueo)
        {
            try
            {
                await _parqueoService.UpdateParqueo(parqueo);
                return Ok("Parqueo actualizado correctamente");
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
        public async Task<ActionResult> DeleteParqueo(int id)
        {
            try
            {
                await _parqueoService.DeleteParqueo(id);
                return Ok("Parqueo eliminado correctamente");
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
