using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

using WebJar.Backend.Data;
using WebJar.Backend.Helpers;
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
using WebJar.Shared.Entities;
using WebJar.Shared.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Para poder mandar l token al Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders Backend", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
            Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
            Example: 'Bearer 12345abcdef'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});

//Inyectamos el servicio para conectarse al SqlServer
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=CadenaSql"));

//Inyectamos el SeedDB
builder.Services.AddTransient<SeedDb>();

//Inyectamos el Servicio de la EmpresaService para la variable global de la empresa seleccionada
builder.Services.AddSingleton<EmpresaService>();

//Inyectar el servicio de correo
builder.Services.AddScoped<IMailHelper, MailHelper>();

//Inyectamos los Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<ICuentasRepository, CuentasRepository>();
builder.Services.AddScoped<IPolizasRepository, PolizasRepository>();
builder.Services.AddScoped<ITiposContaRepository, TiposContaRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

//Inyectamos las UnitOfWork
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped<IEmpresasUnitOfWork, EmpresasUnitOfWork>();
builder.Services.AddScoped<ICuentasUnitOfWork, CuentasUnitOfWork>();
builder.Services.AddScoped<IPolizasUnitOfWork, PolizasUnitOfWork>();
builder.Services.AddScoped<ITiposContaUnitOfWork, TiposContaUnitOfWork>();
builder.Services.AddScoped<IUsuariosUnitOfWork, UsuariosUnitOfWork>();

//Estas lineas es para decirle al sistema como comportarse con los usuarios
builder.Services.AddIdentity<Usuario, IdentityRole>(x =>
{
    x.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    x.SignIn.RequireConfirmedEmail = true;
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    x.Lockout.MaxFailedAccessAttempts = 3;
    x.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

//Para manejar tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
    ClockSkew = TimeSpan.Zero
});

//Para manejar la localizacion
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[]
//    {
//        new CultureInfo("es-GT"),
//        new CultureInfo("en-US")
//    };

//    options.DefaultRequestCulture = new RequestCulture("es-GT");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//});

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