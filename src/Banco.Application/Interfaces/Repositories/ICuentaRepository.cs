using Banco.Domain.Entities;

namespace Banco.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repositorio especifico para la entidad Cuenta
    /// </summary>
    public interface ICuentaRepository : IGenericRepository<Cuenta>
    {
        /// <summary>
        /// Obtiene una cuenta por su numero de cuenta.
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <returns></returns>
        Task<Cuenta?> GetByNumeroCuentaAsync(string numeroCuenta);
    }
}