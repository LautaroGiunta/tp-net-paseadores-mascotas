using API.Clients;
using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Paseadores.Blazor;
using Paseadores.Blazor.Auth;

// Fechas y montos con formato argentino, igual que en escritorio (ej: "sept 2026", "1.234,56")
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-AR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-AR");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var urlApi = new Uri("http://localhost:5206");

builder.Services.AddScoped<AuthTokenHandler>();
builder.Services.AddScoped<SesionService>();

builder.Services.AddHttpClient<DuenoApiClient>(c => c.BaseAddress = urlApi)
       .AddHttpMessageHandler<AuthTokenHandler>();
builder.Services.AddHttpClient<PaseadorApiClient>(c => c.BaseAddress = urlApi)
       .AddHttpMessageHandler<AuthTokenHandler>();
builder.Services.AddHttpClient<PerroApiClient>(c => c.BaseAddress = urlApi)
       .AddHttpMessageHandler<AuthTokenHandler>();
builder.Services.AddHttpClient<AdministradorApiClient>(c => c.BaseAddress = urlApi)
       .AddHttpMessageHandler<AuthTokenHandler>();
builder.Services.AddHttpClient<ReporteApiClient>(c => c.BaseAddress = urlApi)
       .AddHttpMessageHandler<AuthTokenHandler>();
builder.Services.AddHttpClient<PaseoApiClient>(c => c.BaseAddress = urlApi)
       .AddHttpMessageHandler<AuthTokenHandler>();

// El login NO lleva token (es el único endpoint abierto), va sin handler
builder.Services.AddHttpClient<AuthApiClient>(c => c.BaseAddress = urlApi);

await builder.Build().RunAsync();
