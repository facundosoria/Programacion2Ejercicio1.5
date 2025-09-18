using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Api.Dto;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagoController : ControllerBase
    {
        private readonly IServiceFormaPago _service;

        public FormaPagoController(IServiceFormaPago service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var formasPago = _service.GetAll();
                var dtos = new List<FormaPagoFullDto>();
                foreach (var f in formasPago)
                {
                    var dto = new FormaPagoFullDto();
                    dto.IdFormaPago = f.IdFormaPago;
                    dto.Nombre = f.Nombre;
                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var formaPago = _service.GetById(id);
                if (formaPago == null) return NotFound();
                var dto = new FormaPagoFullDto();
                dto.IdFormaPago = formaPago.IdFormaPago;
                dto.Nombre = formaPago.Nombre;
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPost]
        public ActionResult add([FromBody] FormaPagoFullDto dto)
        {
            try
            {
                var formaPago = new FormaPago();
                formaPago.Nombre = dto.Nombre;
                _service.Add(formaPago);
                dto.IdFormaPago = formaPago.IdFormaPago;
                return CreatedAtAction(nameof(GetById), new { id = dto.IdFormaPago }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FormaPagoSimpleDto dto)
        {
            try
            {
                var formaPago = _service.GetById(id);
                if (formaPago == null) return NotFound();
                formaPago.Nombre = dto.Nombre;
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
                var formaPago = _service.GetById(id);
                if (formaPago == null) return NotFound();
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
