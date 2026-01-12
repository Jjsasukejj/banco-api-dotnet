namespace Banco.Application.Interfaces.Repositories
{
    /// <summary>
    /// Centraliza el acceso a los repositorios y controla la persistencia de los cambios
    /// Garantiza que multiples operaciones se confirmen como una sola transaccion logica 
    /// </summary>
    public interface IUnitOfWork
    {
        IClienteRepository Clientes { get; }
        ICuentaRepository Cuentas { get; }
        IMovimientoRepository Movimientos { get; }
        /// <summary>
        /// Persiste todos los cambios realizados en el repositorio.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}