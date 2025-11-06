using AgendaApi.Endpoints;
using AgendaApi.Repositories;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddOpenApi(); // Generador nativo de Microsoft (JSON súper rápido)
builder.Services.AddOpenApiDocument(); // NSwag (para compatibilidad extra si quieres, opcional)
builder.Services.AddSingleton<IContactoRepository, ContactoRepository>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Sirve el JSON en /openapi/v1.json
    app.MapScalarApiReference(options =>
    {
        options.Title = "Agenda API - .NET 9";
        options.WithTheme(ScalarTheme.BluePlanet);
    });
}

app.UseHttpsRedirection();

app.MapContactoEndpoints();

app.Run();