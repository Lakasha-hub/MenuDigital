using Aplication.DTOs;
using Aplication.Exceptions;
using Aplication.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IDeliveryTypeService _service;

        public DeliveryTypeController(IDeliveryTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }
    }
}
