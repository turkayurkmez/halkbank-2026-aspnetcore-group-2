using ECommerce.Application.Exceptions;

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

                await WriteProblemAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İşlenmeyen hata:{Method} {Path}", context.Request.Method, context.Request.Path);

                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex, "Yanıt gönderilirken hata:{Method} {Path}", context.Request.Method, context.Request.Path);

                    throw;
                }
                await WriteProblemAsync(context,ex);
            }
        }

        private async Task WriteProblemAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { error = "Beklenmeyen bir hata oluştu." });

           

            var instance = context.Request.Path.Value;//api/products/create
            var extensions = new Dictionary<string, object>
            {
                ["traceId"] = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier
            };

            IResult result;
            switch (ex)
            {
                case FluentValidation.ValidationException validation:
                   var errors = validation.Errors
                  .GroupBy(e => e.PropertyName)
                  .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new Microsoft.AspNetCore.Mvc.ValidationProblemDetails()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Bir veya daha fazla doğrulama hatası oluştu."
                    });

                    _logger.LogWarning("Doğrulama hatası: {Method} {Path}", context.Request.Method, context.Request.Path);

                    result = Results.ValidationProblem(errors: errors, instance: instance, statusCode: StatusCodes.Status400BadRequest, title: "Doğrulama Hatası", extensions: extensions);

                    break;
                case AppException app:
                    _logger.LogWarning("Kimlik doğrulanamadı");                  

                    result = Problem(app.StatusCode, Title(app.StatusCode), app.Message, instance, extensions);
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    result = Problem(StatusCodes.Status401Unauthorized, "Oturum açılması gerekiyor", message: ex.Message, instance: instance, extensions: extensions); 
                    break;
                default:
                    _logger.LogError("İşlenmeyen hata!");
                    result = Problem(StatusCodes.Status500InternalServerError, "İstenmeyen hata!", "Bilinmeyen bir hata oluştu", instance, extensions);
                    break;
            }

            await result.ExecuteAsync(context);



        }

        private string Title(int statusCode)
        {
            return statusCode switch
            {
                404 => "Kaynak bulunamadı",
                403 => "Yetkisiz kullanıcı",
                409 => "Veriler arası çakışma hatası",
                401 => "Oturum açılması gerekiyor"
            };
        }

        private IResult Problem(int statusCode, string title, string message, string? instance, Dictionary<string, object> extensions)
        {
            return Results.Problem(instance:instance, statusCode:statusCode, title:title, extensions:extensions);
        }
    }
}
