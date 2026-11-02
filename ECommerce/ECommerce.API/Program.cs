using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();// Controller yapısını kullan.


var app = builder.Build();
//buradan sonra builder.Services kısmına müdahale edemezsiniz!!!!!

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Use(async (context, next) =>
{
    //next ÇAĞRILMADAN ÖNCE: istek henüz yeni geliyor.
    app.Logger.LogInformation("Http isteği, middleware'a geldi...");
    await next(context);
    //next'den sonra: cevap geri dönüyor, durum (200) artık belli.
    app.Logger.LogInformation("Bilgi: {Method} [{Path}] -> {StatusCode}, Yanıt uzunluğu:{Body}", context.Request.Method, context.Request.Path, context.Response.StatusCode, context.Response.Body.CanRead);
});

app.Use(async (context,next) =>{
    //Amaç: her response'un ne kadar zaman aldığını hesaplamak:
    var stopWatch = Stopwatch.StartNew();
    app.Logger.LogInformation("Kronometre başladı. Geçen zaman:{time}", stopWatch.ElapsedMilliseconds);

    //HTTP header, body'den önce çalışır ve bir kez ayrıldığı zaman bir daha değiştirilemez. Geçen süreyi header'a yazdırmak istersem, "yanıt üretilmeden önce beni çağır" anlamına gelen OnStarting metodunu kullanırım:
    context.Response.OnStarting(() =>
    {
        context.Response.Headers["X-Response-Time-MS"] = stopWatch.ElapsedMilliseconds.ToString();
        return Task.CompletedTask;
    });
    await next(context);
    app.Logger.LogInformation("Yanıt oluştu. Geçen zaman:{time}", stopWatch.ElapsedMilliseconds);
    stopWatch.Stop();
});

app.MapGet("/ping", () =>
{
    Results.Ok(new { status="ok. Uygulama ayakta" });
});

app.MapControllers();

app.Run();
