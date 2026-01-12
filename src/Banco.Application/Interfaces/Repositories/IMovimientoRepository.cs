using Banco.Domain.Entities;

namespace Banco.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repositorio especifico para la entidad Movimiento
    /// </summary>
    public interface IMovimientoRepository : IGenericRepository<Movimiento>
    {
        /// <summary>
        /// Obtiene los movimientos de una cuenta en un rango de fechas.
        /// </summary>
        /// <param name="cuentaId"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        Task<IEnumerable<Movimiento>> GetByCuentaAndFechaAsync(int cuentaId, DateTime fechaInicio, DateTime fechaFin);
    }
}