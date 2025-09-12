using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly ServiceFactura _service;

        public FacturaController(ServiceFactura service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Factura> facturas = _service.GetAll();
                return Ok(facturas);
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
                var factura = _service.GetById(id);
                if (factura == null) return NotFound();
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Factura factura)
        {
            try
            {
                _service.Add(factura);
                return CreatedAtAction(nameof(GetById), new { id = factura.NroFactura }, factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Factura factura)
        {
            try
            {
                factura.NroFactura = id;
                _service.Update(factura);
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
