using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Api.Dto;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly IServiceDetalleFactura _service;

        public DetalleFacturaController(IServiceDetalleFactura service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var detalles = _service.GetAll();
                var dtos = new List<DetalleFacturaFullDto>();
                foreach (var d in detalles)
                {
                    var dto = new DetalleFacturaFullDto();
                    dto.IdDetalle = d.IdDetalle;
                    dto.NroFactura = d.NroFactura;
                    dto.Cantidad = d.Cantidad;
                    if (d.IdArticuloNavigation != null)
                    {
                        dto.Articulos.Add(new ArticuloFullDto {
                            IdArticulo = d.IdArticuloNavigation.IdArticulo,
                            Nombre = d.IdArticuloNavigation.Nombre,
                            PrecioUnitario = d.IdArticuloNavigation.PrecioUnitario
                        });
                    }
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
                var detalle = _service.GetById(id);
                if (detalle == null) return NotFound();
                var dto = new DetalleFacturaFullDto();
                dto.IdDetalle = detalle.IdDetalle;
                dto.NroFactura = detalle.NroFactura;
                dto.Cantidad = detalle.Cantidad;
                if (detalle.IdArticuloNavigation != null)
                {
                    dto.Articulos.Add(new ArticuloFullDto {
                        IdArticulo = detalle.IdArticuloNavigation.IdArticulo,
                        Nombre = detalle.IdArticuloNavigation.Nombre,
                        PrecioUnitario = detalle.IdArticuloNavigation.PrecioUnitario
                    });
                }
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPost]
        public ActionResult Add([FromBody] DetalleFacturaFullDto dto)
        {
            try
            {
                var detalle = new DetalleFactura();
                detalle.NroFactura = dto.NroFactura;
                detalle.Cantidad = dto.Cantidad;
                // Si hay artículos en el DTO, tomar el primero para asociar
                if (dto.Articulos != null && dto.Articulos.Count > 0)
                {
                    detalle.IdArticulo = dto.Articulos[0].IdArticulo;
                }
                _service.Add(detalle);
                dto.IdDetalle = detalle.IdDetalle;
                return CreatedAtAction(nameof(GetById), new { id = dto.IdDetalle }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DetalleFacturaFullDto dto)
        {
            try
            {
                var detalle = _service.GetById(id);
                if (detalle == null) return NotFound();
                detalle.NroFactura = dto.NroFactura;
                detalle.Cantidad = dto.Cantidad;
                if (dto.Articulos != null && dto.Articulos.Count > 0)
                {
                    detalle.IdArticulo = dto.Articulos[0].IdArticulo;
                }
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
                var detalle = _service.GetById(id);
                if (detalle == null) return NotFound();
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
