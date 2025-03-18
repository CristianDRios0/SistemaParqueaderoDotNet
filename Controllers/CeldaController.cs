using Microsoft.AspNetCore.Mvc;
using SistemaParqueadero.Models;
using SistemaParqueadero.Services.Interfaces;

namespace SistemaParqueadero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CeldaController : ControllerBase
    {
        private readonly ICeldaService _celdaService;

        public CeldaController(ICeldaService celdaService) 
        {
            _celdaService = celdaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Celda>>> GetAllCeldas() 
        {
            try
            {
                return Ok(await _celdaService.GetAllCeldas()); 
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
        public async Task<ActionResult<Celda>> GetCeldaById(int id) 
        {
            try 
            {
                var celda = await _celdaService.GetCeldaById(id);
                return Ok(celda);
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

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<Celda>> GetCeldaByCodigo(string codigo) 
        {
            try
            {
                var celda = await _celdaService.GetCeldaByCodigo(codigo);
                return Ok(celda);
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

        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Celda>>> GetCeldaByEstado(string estado) 
        {
            try
            {
                var celdas = await _celdaService.GetCeldaByEstado(estado);
                return Ok(celdas);
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
        public async Task<ActionResult> AddCelda(Celda celda)
        {
            try
            {
                await _celdaService.AddCelda(celda);
                return Ok();
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
        public async Task<ActionResult> UpdateCelda(Celda celda)
        {
            try
            {
                await _celdaService.UpdateCelda(celda);
                return Ok();
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

        [HttpDelete]
        public async Task<ActionResult> DeleteCelda(int id)
        {
            try
            {
                await _celdaService.DeleteCelda(id);
                return Ok();
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
