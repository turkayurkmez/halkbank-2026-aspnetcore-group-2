using ECommerce.API.Extensions;
using ECommerce.Application.Requests;
using ECommerce.Application.Responses;
using ECommerce.Application.Services;
using ECommerce.Domain;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IValidator<CreateProductRequest> createValidator;

        public ProductsController(IProductService service, IValidator<CreateProductRequest> createValidator)
        {
            _service = service;
            this.createValidator = createValidator;
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
        public async Task<IActionResult> Create(CreateProductRequest request)
        {

            var validation = await createValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return validation.ToValidationProblem(this);
            }


            var product = request.Adapt<Product>();


            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name, [FromQuery] decimal? minPrice = 0, [FromQuery] decimal? maxPrice = 0)
        {


            IEnumerable<ProductListResponse> products = [];

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

        [HttpGet("GetAllWithCategory")]
        public async Task<IActionResult> GetAllWithCategory()
        {
            var products = await _service.GetProductsWithCategoryAsync();
            return Ok(products);
        }




    }
}
