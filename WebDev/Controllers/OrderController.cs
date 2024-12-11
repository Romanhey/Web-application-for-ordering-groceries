using Microsoft.AspNetCore.Mvc;
using WebDev.DTO;
using WebDev.services;

namespace WebDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController
        (
            OrderService service
        )
        : ControllerBase
    {
        private readonly OrderService _orderService = service;

        [HttpPut]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO model)
        {
            await _orderService.AddOrder(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            
            return Ok(await _orderService.GetOrder(id));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDTO model)
        {
            await _orderService.UpdateOrder(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            
            return Ok($"Order with ID {id} has been deleted.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GerOrderByUserId(int userId)
        {
            return Ok(await _orderService.GetOrdersByUserId(userId));
        }
    }
}
