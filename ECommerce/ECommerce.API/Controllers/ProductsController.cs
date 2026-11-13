using ECommerce.API.Extensions;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Queries.GetProducts;

using ECommerce.Application.Responses;
using ECommerce.Application.Services;

using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IValidator<CreateProductCommand> createValidator;
        private readonly IMediator _mediator;

        public ProductsController(IProductService service, IValidator<CreateProductCommand> createValidator, IMediator mediator)
        {
            _service = service;
            this.createValidator = createValidator;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetProductsQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProductAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand request)
        {

            var validation = await createValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return validation.ToValidationProblem(this);
            }


            var response = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
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
