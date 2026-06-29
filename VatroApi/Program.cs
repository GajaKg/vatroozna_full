using VatroApi.V1.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Repositories;
using VatroApi.V1.Services;
using VatroApi.V1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("WebApiDatabase"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:5072", "https://localhost:5072")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Optional, if using cookies/auth
        });
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IControlRepository, ControlRepository>();
builder.Services.AddScoped<IControlService, ControlService>();

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Middlewares
builder.Services.AddTransient<GlobalExceptionMiddleware>();


var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Vatrozona API")
               .WithTheme(ScalarTheme.Moon)
               .WithDefaultHttpClient(
                   ScalarTarget.CSharp,
                   ScalarClient.HttpClient);
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowBlazorLocalhost");
app.MapControllers();

app.Run();