using System.Text;
using Application.Services;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaseadoresContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Swagger con el botón Authorize para pegar el token del login
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pegar unicamente el token devuelto por /api/auth/login (sin escribir 'Bearer')."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// La misma configuración usa TokenService para firmar y la API para validar
var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>()
    ?? throw new InvalidOperationException("Falta la seccion 'Jwt' en appsettings.json.");

builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Emisor,
            ValidAudience = jwtConfig.Audiencia,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.ClaveSecreta)),
            // Sin margen extra sobre el vencimiento
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazor", app =>
    {
        // Acá ponés exactamente la URL que te muestra el navegador cuando abrís Blazor
        app.WithOrigins("http://localhost:7202", "https://localhost:7202")
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Politicas.SoloAdmin, politica =>
        politica.RequireRole(Roles.Admin));

    options.AddPolicy(Politicas.AdminODueno, politica =>
        politica.RequireRole(Roles.Admin, Roles.Dueno));

    options.AddPolicy(Politicas.GestionDePaseos, politica =>
        politica.RequireRole(Roles.Admin, Roles.Dueno, Roles.Paseador));
});

// Inyección de dependencias - Usuario
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
app.UseCors("PermitirBlazor");
app.UseAuthentication();
app.UseAuthorization();

// Mapeo de endpoints
app.MapPaseadorEndpoints();
app.MapDuenoEndpoints();
app.MapPerroEndpoints();
app.MapPaseoEndpoints();
app.MapAuthEndpoints();

app.Run();
