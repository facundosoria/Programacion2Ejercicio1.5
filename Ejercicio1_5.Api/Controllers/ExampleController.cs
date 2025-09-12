using Microsoft.AspNetCore.Mvc;

namespace Ejercicio1_5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "API funcionando correctamente" });
        }
    }
}
