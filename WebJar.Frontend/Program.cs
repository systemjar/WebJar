using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Globalization;

using WebJar.Frontend;
using WebJar.Frontend.AuthenticationProviders;
using WebJar.Frontend.Repositories;
using WebJar.Frontend.Services;
using WebJar.Shared.Servicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });*/
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7032/") });

//Inyectamos el Repositorio del Http del Frontend
builder.Services.AddScoped<IRepository, Repository>();

//Inyectar el Servicio
builder.Services.AddScoped<EmpresaService>(); // Registra el servicio como singleton

//Inyectamos el SweetAlert2
builder.Services.AddSweetAlert2();

//Servicio de autenticacion
builder.Services.AddAuthorizationCore();

//Proveedor de seguridad
//Linea de pruba inicial
//builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x =>
x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x =>
x.GetRequiredService<AuthenticationProviderJWT>());

//Inyectar el Blazor Modal
builder.Services.AddBlazoredModal();

//Inyectamos MudBlazor
builder.Services.AddMudServices();

//Localizacion
var culture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();