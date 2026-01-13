using System.Security.Cryptography.X509Certificates;
using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Entities;
using Banco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacion del repositorio de Movimiento usando EF Core
    /// </summary>
    public class MovimientoRepository : GenericRepository<Movimiento>, IMovimientoRepository
    {
        public MovimientoRepository(BancoDbContext bancoDbContext) : base(bancoDbContext)
        {
        }

        public async Task<IEnumerable<Movimiento>> GetByCuentaAndFechaAsync(int cuentaId, DateTime fechaInicio, DateTime fechaFin)
        {
            //Normalizamos fechas para cubrir todo el rango diario
            var inicioDia = fechaInicio.Date; // 00:00:00
            var finDia = fechaFin.Date.AddDays(1).AddTicks(-1); // 23:59:59.9999999
            //Obtiene los movimientos asociados a un cuenta especifica dentro de un rango de fechas
            //Se filtra por CuentaId y Fecha
            return await bancoDbContext.Movimientos
                .AsNoTracking() //incluye todos los movimientos del dia cuando el usuario solo envia fechas sin hora
                .Where(x => 
                    x.CuentaId == cuentaId &&
                    x.Fecha >= inicioDia &&
                    x.Fecha <= finDia)
                .OrderBy(x => x.Fecha)
                .ToListAsync();
        }
    }
}