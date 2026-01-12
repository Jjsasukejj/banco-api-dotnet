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
            //Obtiene los movimientos asociados a un cuenta especifica dentro de un rango de fechas
            //Se filtra por CuentaId y Fecha
            return await bancoDbContext.Movimientos
                .Where(x => 
                    x.CuentaId == cuentaId &&
                    x.Fecha >= fechaInicio &&
                    x.Fecha <= fechaFin)
                .OrderBy(x => x.Fecha)
                .ToListAsync();
        }
    }
}