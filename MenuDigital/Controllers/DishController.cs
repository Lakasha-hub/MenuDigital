using Aplication.DTOs.Dish;
using Aplication.Exceptions;
using Aplication.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _services;

        public DishController(IDishService services)
        {
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DishResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] DishFilterRequest queryParams)
        {
            try
            {
                var result = await _services.GetAll(queryParams);
                return Ok(result);
            }
            catch (BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _services.GetById(id);
                return Ok(result);
            }
            catch(BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DishRequest req)
        {
            try
            {
                var result = await _services.Create(req);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ConflictException e)
            {
                return Conflict(new ApiError { Message = e.Message});
            }
            catch (BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] DishUpdateRequest req)
        {
            try
            {
                var result = await _services.Update(id, req);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiError { Message = e.Message });
            }
            catch (ConflictException e)
            {
                return Conflict(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _services.Delete(id);
                return Ok(result);
            }
            catch (BusinessException e)
            {
                return Conflict(new ApiError { Message = e.Message });
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }
    }
}
