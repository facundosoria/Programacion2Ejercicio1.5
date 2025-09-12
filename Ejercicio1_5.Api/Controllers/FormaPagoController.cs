using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagoController : ControllerBase
    {
        private readonly ServiceFormaPago _service;

        public FormaPagoController(ServiceFormaPago service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<FormaPago> formasPago = _service.GetAll();
                return Ok(formasPago);
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
                var formaPago = _service.GetById(id);
                if (formaPago == null) return NotFound();
                return Ok(formaPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] FormaPago formaPago)
        {
            try
            {
                _service.Add(formaPago);
                return CreatedAtAction(nameof(GetById), new { id = formaPago.IdFormaPago }, formaPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FormaPago formaPago)
        {
            try
            {
                formaPago.IdFormaPago = id;
                _service.Update(formaPago);
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
