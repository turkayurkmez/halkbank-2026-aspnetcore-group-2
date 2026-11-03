using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ShopController(IWebHostEnvironment environment)
        {
            _environment = environment;
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
    }
}
