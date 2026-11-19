

using ECommerce.API.Authorization;
using ECommerce.API.Middleware;
using ECommerce.Application;
using ECommerce.Application.Auth;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configration) =>
                        configration.ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        );

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
builder.Services.AddAplication(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme).Configure<IOptions<JwtOptions>>((bearer, jwtAccessor) =>
{
    //gelen token'ı nasıl onaylayacaksınız (verification)
    var jwt = jwtAccessor.Value;
    bearer.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt.Issuer,
        ValidateAudience = true,
        ValidAudience = jwt.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey)),
        ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
        ValidateLifetime = true,
        NameClaimType = "unique_name",
        RoleClaimType = "role"
    };
    
});



builder.Services.AddAuthorizationBuilder()
                .AddPolicy("MinimumAge18", policy => policy.RequireAuthenticatedUser().AddRequirements(new MinimumAgeRequirement(18)));
              


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

app.UseSerilogRequestLogging();

app.UseResponseCompression();

app.MapGet("/ping", () =>
{
    Results.Ok(new { status="ok. Uygulama ayakta" });
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
