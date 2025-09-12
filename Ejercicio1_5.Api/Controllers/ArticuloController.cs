using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;


namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloController : ControllerBase
    {
        private readonly ServiceArticulo _service;

        public ArticuloController(ServiceArticulo service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Articulo> articulos = _service.GetAll();
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var articulo = _service.GetById(id);
                if (articulo == null) return NotFound();
                return Ok(articulo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Articulo articulo)
        {
            try
            {
                _service.Add(articulo);
                return CreatedAtAction(nameof(GetById), new { id = articulo.IdArticulo }, articulo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Articulo articulo)
        {
            try
            {
                articulo.IdArticulo = id;
                _service.Update(articulo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
