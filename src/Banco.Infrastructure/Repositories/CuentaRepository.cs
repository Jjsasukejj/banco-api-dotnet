using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Entities;
using Banco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacion del repositorio de Cuenta usando EF Core
    /// </summary>
    public class CuentaRepository : GenericRepository<Cuenta>, ICuentaRepository
    {
        public CuentaRepository(BancoDbContext bancoDbContext) : base(bancoDbContext)
        {
        }

        public async Task<Cuenta?> GetByNumeroCuentaAsync(string numeroCuenta)
        {
            //Obtiene un cuenta a partir de su numero de cuenta (NumeroCuenta) incluyendo sus movimientos asociados
            //Se utiliza include para cargar los movimientos en una sola consulta
            return await bancoDbContext.Cuentas.Include(x => x.Movimientos).FirstOrDefaultAsync(x => x.NumeroCuenta == numeroCuenta);
        }
    }
}