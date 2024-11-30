using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebJar.Frontend;
using WebJar.Frontend.Repositories;
using WebJar.Shared.Servicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/*builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });*/
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7032/") });

//Inyectamos el Repositorio del Http del Frontend
builder.Services.AddScoped<IRepository, Repository>();
//Inyectar el Servicio
builder.Services.AddSingleton<EmpresaService>(); // Registra el servicio como singleton
//Inyectamos el SweetAlert2
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();