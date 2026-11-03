using System.Diagnostics;

namespace ECommerce.API.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;
        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Amaç: her response'un ne kadar zaman aldığını hesaplamak:
            var stopWatch = Stopwatch.StartNew();
            _logger.LogInformation("Kronometre başladı. Geçen zaman:{time}", stopWatch.ElapsedMilliseconds);

            //HTTP header, body'den önce çalışır ve bir kez ayrıldığı zaman bir daha değiştirilemez. Geçen süreyi header'a yazdırmak istersem, "yanıt üretilmeden önce beni çağır" anlamına gelen OnStarting metodunu kullanırım:
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Response-Time-MS"] = stopWatch.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });
            await _next(context);
            _logger.LogInformation("Yanıt oluştu. Geçen zaman:{time}", stopWatch.ElapsedMilliseconds);
            //stopWatch.Stop();
        }
    }
}
