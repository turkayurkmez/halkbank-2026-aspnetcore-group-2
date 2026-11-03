using ECommerce.API.Middleware;
using ECommerce.API.Options;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();// Controller yapısını kullan.
builder.Services.AddResponseCompression();

builder.Services.Configure<ShopOption>(builder.Configuration.GetSection("Shop"));

var app = builder.Build();
//buradan sonra builder.Services kısmına müdahale edemezsiniz!!!!!

var value = builder.Configuration["Shop:MaxOrderItems"];

app.Logger.LogInformation($"appsettings'deki değer:{value}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

/*
 * Daha önce Use ile bütün işi tek metotta yürüttüğümüz senaryo yerine daha temiz bir çüzüm için Middleware/ altına sınıfları taşıdık ve aşağıdaki gibi pipeline'a  ekledik:
 */

app.UseMiddleware<ExceptionHandlingMiddleware>();// kendisinden sonraki herhangi bir middleware'de hata olduğunda yine bu arkadaşın çalışabilmesi için pipeline'da ilk çalışması gereken ilk middleware her zaman Exception middleware'dir 
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();

app.UseResponseCompression();

app.MapGet("/ping", () =>
{
    Results.Ok(new { status="ok. Uygulama ayakta" });
});

app.MapControllers();

app.Run();
