using Application.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaseadoresContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias - Paseador
builder.Services.AddScoped<IPaseadorRepository, PaseadorRepository>();
builder.Services.AddScoped<IPaseadorService, PaseadorService>();

// Inyección de dependencias - Dueño
builder.Services.AddScoped<IDuenoRepository, DuenoRepository>();
builder.Services.AddScoped<IDuenoService, DuenoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Mapeo de endpoints
app.MapPaseadorEndpoints();
app.MapDuenoEndpoints();

app.Run();
