using Aplication.DTOs.Order;
using Aplication.Exceptions;
using Aplication.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigital.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderCreateResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OrderRequest req)
        {
            try
            {
                var result = await _service.Create(req);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new ApiError { Message = "Error interno del servidor" });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] OrderFilterRequest filter)
        {
            try
            {
                var result = await _service.GetAll(filter);
                return Ok(result);
            }
            catch (BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new ApiError { Message = "Error interno del servidor" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderUpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateRequest req)
        {
            try
            {
                var result = await _service.Update(id, req);
                return Ok(result);
            }
            catch (BusinessException e)
            {
                return BadRequest(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new ApiError { Message = "Error interno del servidor" });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound(new ApiError { Message = e.Message });
            }
            catch (Exception e)
            {
                Console.Write(e);
                return StatusCode(500, new ApiError { Message = e.Message });
            }
        }

        [HttpPut("{id}/item/{itemId}")]
        [ProducesResponseType(typeof(OrderUpdateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem(int id, int itemId, OrderItemUpdateRequest status)
        {
            try
            {
                var result = await _service.UpdateItem(id, itemId, status);
                return Ok(result);
            }
            catch (BusinessException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return StatusCode(500, new ApiError { Message = e.Message });
            }
        }
    } 
}
