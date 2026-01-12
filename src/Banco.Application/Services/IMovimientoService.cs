using Banco.Domain.Entities;
using Banco.Domain.Enums;

namespace Banco.Application.Services
{
    /// <summary>
    /// Contrato del servicio de movimiento
    /// Define las operaciones de negocio relacionadas con creditos y debitos sobre una cuenta
    /// </summary>
    public interface IMovimientoService
    {
        /// <summary>
        /// Registrar un movimiento (credito/debito) sobre una cuenta
        /// Aplica todas las validaciones de negocio necesarias
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <param name="tipoMovimiento"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        Task<Movimiento> RegistrarMovimientoAsync(string numeroCuenta, TipoMovimiento tipoMovimiento, decimal valor);
    }
}