using Banco.Application.Interfaces.Repositories;
using Banco.Infrastructure.Data;

namespace Banco.Infrastructure
{
    /// <summary>
    /// Implementacion concreta del patron unit of work
    /// Orquesta los repositorios y controla la persistencia de cambios usando un unico DbContext
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BancoDbContext bancoDbContext;
        public IClienteRepository Clientes { get; }
        public ICuentaRepository Cuentas { get; }
        public IMovimientoRepository Movimientos { get; }

        public UnitOfWork(
            BancoDbContext bancoDbContext,
            IClienteRepository clientes,
            ICuentaRepository cuentas,
            IMovimientoRepository movimientos)
        {
            this.bancoDbContext = bancoDbContext;
            Clientes = clientes;
            Cuentas = cuentas;
            Movimientos = movimientos;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await bancoDbContext.SaveChangesAsync();
        }
    }
}