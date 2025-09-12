using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly ServiceDetalleFactura _service;

        public DetalleFacturaController(ServiceDetalleFactura service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<DetalleFactura> detalles = _service.GetAll();
                return Ok(detalles);
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
                var detalle = _service.GetById(id);
                if (detalle == null) return NotFound();
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] DetalleFactura detalle)
        {
            try
            {
                _service.Add(detalle);
                return CreatedAtAction(nameof(GetById), new { id = detalle.IdDetalle }, detalle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DetalleFactura detalle)
        {
            try
            {
                detalle.IdDetalle = id;
                _service.Update(detalle);
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
