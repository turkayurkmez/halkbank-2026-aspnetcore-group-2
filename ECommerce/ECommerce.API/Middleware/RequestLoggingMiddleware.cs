namespace ECommerce.API.Middleware
{
    /*
     * Herhangi bir middleware singleton ömre sahiptir. Yani uygulama ayağa kalkarken sadece bir kez cons. çalışır ve InvokeAsync metodu hep aynı instance'dan gelir. Bu yüzden, daha büyük yaşamı olan bir bağımlılığı middleware'in cons'dan veremezsiniz. Bu nedenle böyle bir bağımlılığa ihtiyaç duyarsanız; InvokeAsync metodunun parametresi olarak tanımlamalısınız.
     * 
     */
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;    
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        
        public async Task InvokeAsync(HttpContext context)
        {
            //next ÇAĞRILMADAN ÖNCE: istek henüz yeni geliyor.
            _logger.LogInformation("Http isteği, middleware'a geldi...");
            await _next(context);
            //next'den sonra: cevap geri dönüyor, durum (200) artık belli.
            _logger.LogInformation("Bilgi: {Method} [{Path}] -> {StatusCode}, Yanıt uzunluğu:{Body}", context.Request.Method, context.Request.Path, context.Response.StatusCode, context.Response.Body.CanRead);
        }

    }
}
