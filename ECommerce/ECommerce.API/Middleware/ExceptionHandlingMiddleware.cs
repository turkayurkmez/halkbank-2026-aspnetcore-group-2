namespace ECommerce.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;      

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex) when (!context.Response.HasStarted)
            {
                _logger.LogWarning("Doğrulama hatası: {Method} {Path}", context.Request.Method, context.Request.Path);

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new Microsoft.AspNetCore.Mvc.ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bir veya daha fazla doğrulama hatası oluştu."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İşlenmeyen hata:{Method} {Path}", context.Request.Method, context.Request.Path);

                if (context.Response.HasStarted)
                {
                    throw;
                }
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Beklenmeyen bir hata oluştu." });
            }
        }
    }
}
