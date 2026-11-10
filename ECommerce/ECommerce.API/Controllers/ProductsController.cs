using ECommerce.API.Services;
using ECommerce.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetProductsAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProductAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name, [FromQuery] decimal? minPrice = 0, [FromQuery] decimal? maxPrice = 0)
        {


            IEnumerable<Product> products = [];

            if (!string.IsNullOrEmpty(name))
            {
                products = await _service.SearchAsync(name);

            }

            if (minPrice != 0 && maxPrice != 0)
            {
                products = await _service.SearchAsync(minPrice.Value, maxPrice.Value);


            }

            return Ok(products);


        }




    }
}
