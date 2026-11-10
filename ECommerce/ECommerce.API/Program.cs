
using ECommerce.API.Middleware;
using ECommerce.API.Notifications;
using ECommerce.API.Options;

using ECommerce.API.Services;
using ECommerce.API.Store;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});// Controller yapısını kullan.
builder.Services.AddResponseCompression();

builder.Services.Configure<ShopOption>(builder.Configuration.GetSection("Shop"));

//builder.Services.AddSingleton<InMemoryStore>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductRepository, EFProductRepository>();

builder.Services.AddSingleton<INotificationChannel, EmailChannel>();
builder.Services.AddSingleton<INotificationChannel, SMSChannel>();
builder.Services.AddSingleton<INotificationChannel, WhatsAppChannel>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));



var app = builder.Build();
//buradan sonra builder.Services kısmına müdahale edemezsiniz!!!!!

var value = builder.Configuration["Shop:MaxOrderItems"];

app.Logger.LogInformation($"appsettings'deki değer:{value}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    using var scope = app.Services.CreateScope();
    await DataSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<AppDbContext>());
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
