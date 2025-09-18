using Microsoft.AspNetCore.Mvc;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Api.Dto;
using System.Collections.Generic;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloController : ControllerBase
    {
        private readonly IServiceArticulo _service;

        public ArticuloController(IServiceArticulo service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var articulos = _service.GetAll();
                var dtos = new List<ArticuloFullDto>();
                foreach (var a in articulos)
                {
                    var dto = new ArticuloFullDto();
                    dto.IdArticulo = a.IdArticulo;
                    dto.Nombre = a.Nombre;
                    dto.PrecioUnitario = a.PrecioUnitario;
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
                var articulo = _service.GetById(id);
                if (articulo == null) return NotFound();

                var dto = new ArticuloFullDto();
                dto.IdArticulo = articulo.IdArticulo;
                dto.Nombre = articulo.Nombre;
                dto.PrecioUnitario = articulo.PrecioUnitario;

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] ArticuloFullDto dto)
        {
            try
            {
                var articulo = new Articulo();
                articulo.Nombre = dto.Nombre;
                articulo.PrecioUnitario = dto.PrecioUnitario;

                _service.Add(articulo);
                dto.IdArticulo = articulo.IdArticulo;

                return CreatedAtAction(nameof(GetById), new { id = dto.IdArticulo }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ArticuloSimpleDto dto)
        {
            try
            {
                var articulo = _service.GetById(id);
                if (articulo == null) return NotFound();

                articulo.Nombre = dto.Nombre;
                articulo.PrecioUnitario = dto.PrecioUnitario;
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
                var articulo = _service.GetById(id);
                if (articulo == null) return NotFound();

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