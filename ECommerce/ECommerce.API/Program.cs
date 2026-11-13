

using ECommerce.API.Middleware;
using ECommerce.Application;
using ECommerce.Application.Mapping;
using ECommerce.Application.Notifications;
using ECommerce.Application.Options;
using ECommerce.Application.Requests;
using ECommerce.Application.Services;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.UnitOfWorkPattern;
using FluentValidation;
using Mapster;
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

//Bu extension metod, Application içinde yazıldı. O katmanda yer alan tüm dependency injection gereksinimlerni bu metot karşılıyor:
builder.Services.AddAplication();

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
