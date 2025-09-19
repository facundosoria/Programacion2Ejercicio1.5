using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Api.Dto;


namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly IServiceFactura _service;

        public FacturaController(IServiceFactura service)
        {
            _service = service;
        }



        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var facturas = _service.GetAll();
                var dtos = new List<FacturaSimpleDto>();
                foreach (var f in facturas)
                {
                    var dto = new FacturaSimpleDto();
                    dto.NroFactura = f.NroFactura;
                    dto.Fecha = f.Fecha;
                    dto.FormaPago = f.IdFormaPagoNavigation != null ? f.IdFormaPagoNavigation.Nombre : string.Empty;
                    dto.Cliente = f.Cliente;
                    // Mapear detalles (solo cantidad y artículos)
                    foreach (var d in f.DetalleFacturas)
                    {
                        var detalleDto = new DetalleFacturaSimpleDto();
                        detalleDto.Cantidad = d.Cantidad;
                        if (d.IdArticuloNavigation != null)
                        {
                            detalleDto.Articulo = new ArticuloSimpleDto {
                                Nombre = d.IdArticuloNavigation.Nombre,
                                PrecioUnitario = d.IdArticuloNavigation.PrecioUnitario
                            };
                        }
                        dto.Detalles.Add(detalleDto);
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
                var factura = _service.GetById(id);
                if (factura == null) return NotFound();
                var dto = new FacturaFullDto();
                dto.NroFactura = factura.NroFactura;
                dto.Fecha = factura.Fecha;
                dto.FormaPago = factura.IdFormaPagoNavigation != null
                    ? new FormaPagoFullDto {
                        IdFormaPago = factura.IdFormaPagoNavigation.IdFormaPago,
                        Nombre = factura.IdFormaPagoNavigation.Nombre
                    } 
                    : null;
                dto.Cliente = factura.Cliente;
                foreach (var d in factura.DetalleFacturas)
                {
                    var detalleDto = new DetalleFacturaFullDto();
                    detalleDto.IdDetalle = d.IdDetalle;
                    detalleDto.NroFactura = d.NroFactura;
                    detalleDto.Cantidad = d.Cantidad;
                    if (d.IdArticuloNavigation != null)
                    {
                        detalleDto.Articulos.Add(new ArticuloFullDto {
                            IdArticulo = d.IdArticuloNavigation.IdArticulo,
                            Nombre = d.IdArticuloNavigation.Nombre,
                            PrecioUnitario = d.IdArticuloNavigation.PrecioUnitario
                        });
                    }
                    dto.Detalles.Add(detalleDto);
                }
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPost]
        public ActionResult Add([FromBody] FacturaAddDto dto)
        {
            try
            {
                var factura = new Factura
                {
                    Fecha = dto.Fecha,
                    IdFormaPago = dto.IdFormaPago,
                    Cliente = dto.Cliente,
                    DetalleFacturas = new List<DetalleFactura>()
                };

                foreach (var det in dto.Detalles)
                {
                    var detalle = new DetalleFactura
                    {
                        Cantidad = det.Cantidad,
                        IdArticulo = det.Articulo != null ? det.Articulo.IdArticulo : 0
                    };
                    factura.DetalleFacturas.Add(detalle);
                }
                _service.Add(factura);
                return CreatedAtAction(nameof(GetById), new { id = factura.NroFactura }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FacturaAddDto dto)
        {
            try
            {
                var factura = _service.GetById(id);
                if (factura == null) return NotFound();
                factura.Fecha = dto.Fecha;
                factura.IdFormaPago = dto.IdFormaPago;
                factura.Cliente = dto.Cliente;
                // Actualizar detalles (opcional: aquí podrías eliminar y volver a agregar, o actualizar uno a uno)
                factura.DetalleFacturas.Clear();
                foreach (var det in dto.Detalles)
                {
                    var detalle = new DetalleFactura
                    {
                        Cantidad = det.Cantidad,
                        IdArticulo = det.Articulo != null ? det.Articulo.IdArticulo : 0
                    };
                    factura.DetalleFacturas.Add(detalle);
                }
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
                var factura = _service.GetById(id);
                if (factura == null) return NotFound();
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
