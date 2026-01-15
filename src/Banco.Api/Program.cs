using Banco.Application.Interfaces.Repositories;
using Banco.Application.Services;
using Banco.Infrastructure;
using Banco.Infrastructure.Data;
using Banco.Infrastructure.Pdf;
using Banco.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;
// Permite usar controllers 
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Registrar BancoDbContext en el contenedor de dependencias
//Se configura EF Core para usar Sql Server tomando la cadena de conexion desde el appsettings.json
builder.Services.AddDbContext<BancoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//registro del servicio de movimiento, con scoped comparto el mismo DbContext durante la request
builder.Services.AddScoped<IMovimientoService, MovimientoService>();
//Servicio de reportes
builder.Services.AddScoped<IReporteService, ReporteService>();
//Servicio de PDF
builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
//Repositorio de Clientes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
//Repositorio de Cuentas
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
//Repositorio de Cuentas
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
//Unidad de trabajo
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Configuracion de CORS, para permitir que frontend Angular consuma esta API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") //origen permitido
            .AllowAnyHeader()                     //headers permitidos 
            .AllowAnyMethod();                    //GET, POST, PUT, PATCH, DELETE
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// habilita el enrutamiento
app.UseRouting();
//Middleware de CORS
app.UseCors("AllowAngularApp");
// mapea todos los controllers
app.MapControllers();

app.Run();