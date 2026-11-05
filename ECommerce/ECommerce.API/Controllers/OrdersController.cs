using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var createdOrder = await orderService.CreateAsync(order);
            return Created("https://uydurukadres.com", order);
        }
    }
}
