using VatroApi.V1.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VatroApi.V1.Interfaces;
using VatroApi.V1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("WebApiDatabase"));
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IControlRepository, ControlRepository>();

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

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


app.UseHttpsRedirection();

app.MapControllers();

app.Run();