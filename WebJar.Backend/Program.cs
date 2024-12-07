using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebJar.Backend.Data;
using WebJar.Backend.Repositories.Implementations;
using WebJar.Backend.Repositories.Implementations.Conta;
using WebJar.Backend.Repositories.Implementations.Generico;
using WebJar.Backend.Repositories.Interfaces;
using WebJar.Backend.Repositories.Interfaces.Conta;
using WebJar.Backend.Repositories.Interfaces.Generico;
using WebJar.Backend.UnitOfWork.Implementations;
using WebJar.Backend.UnitOfWork.Implementations.Conta;
using WebJar.Backend.UnitOfWork.Implementations.Gererico;
using WebJar.Backend.UnitOfWork.Interfaces;
using WebJar.Backend.UnitOfWork.Interfaces.Conta;
using WebJar.Backend.UnitOfWork.Interfaces.Generico;
using WebJar.Shared.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyectamos el servicio para conectarse al SqlServer
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=CadenaSql"));

//Inyectamos el SeedDB
builder.Services.AddTransient<SeedDb>();

//Inyectamos el Servicio de la EmpresaService para la variable global de la empresa seleccionada
builder.Services.AddSingleton<EmpresaService>();

//Inyectamos los Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<ICuentasRepository, CuentasRepository>();
builder.Services.AddScoped<ITiposContaRepository, TiposContaRepository>();

//Inyectamos las UnitOfWork
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped<IEmpresasUnitOfWork, EmpresasUnitOfWork>();
builder.Services.AddScoped<ICuentasUnitOfWork, CuentasUnitOfWork>();
builder.Services.AddScoped<ITiposContaUnitOfWork, TiposContaUnitOfWork>();

var app = builder.Build();

//Para inyectar el SeedDb en Program
SeedData(app);

static void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopedFactory!.CreateScope();
    var service = scope.ServiceProvider.GetService<SeedDb>();
    service!.SeedAsync().Wait();
}
//Para configurar la seguridad del api
app.UseCors(x => x
    .AllowCredentials() //Cualquier credencial
    .AllowAnyHeader()   //Para permitir el envio de cualquier header

    .AllowAnyMethod()   //Cualquiera puede consumir cualquier metodo
    .SetIsOriginAllowed(origin => true)); //Si no se pone esta linea no va a funcionar

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();