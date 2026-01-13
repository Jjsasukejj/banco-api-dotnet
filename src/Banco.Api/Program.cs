using Banco.Application.Interfaces.Repositories;
using Banco.Application.Services;
using Banco.Infrastructure;
using Banco.Infrastructure.Data;
using Banco.Infrastructure.Pdf;
using Banco.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// habilita el enrutamiento
app.UseRouting();
// mapea todos los controllers
app.MapControllers();

app.Run();