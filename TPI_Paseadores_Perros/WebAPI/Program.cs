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

// Inyección de dependencias - Perro
builder.Services.AddScoped<IPerroRepository, PerroRepository>();
builder.Services.AddScoped<IPerroService, PerroService>();

// Inyección de dependencias - Paseo
builder.Services.AddScoped<IPaseoRepository, PaseoRepository>();
builder.Services.AddScoped<IPaseoService, PaseoService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Data.PaseadoresContext>();
    context.Database.Migrate();

}
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
app.MapPerroEndpoints();
app.MapPaseoEndpoints();

app.Run();
