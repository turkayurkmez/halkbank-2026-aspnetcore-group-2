using ECommerce.API.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IOptions<ShopOption> _options;
        private readonly ShopOption _shopOption;
        private readonly IConfiguration _configuration;

        public ShopController(IWebHostEnvironment environment, IOptions<ShopOption> options, IConfiguration configuration)
        {
            _environment = environment;
            _options = options;
            _shopOption = _options.Value;
            _configuration = configuration;
        }
        [HttpGet("boom")]
        public IActionResult Boom()
        {
            if (!_environment.IsDevelopment())
            {
                return NotFound();
            }

            throw new InvalidOperationException("Bu hata, deney amaçlı fırlatıldı");
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            var paymentKeyConfigured = !string.IsNullOrEmpty(_configuration["ExternalServices:PaymentApiKey"]);
            Console.WriteLine(paymentKeyConfigured);
            return Ok(new
            {
                storeName = _shopOption.StoreName,
                currency = _shopOption.Currency,
                maxOrderItems = _shopOption.MaxOrderItems,
                paymentKeyConfigured
            });
        }
    }
}
