using Banco.Domain.Enums;

namespace Banco.Application.DTOs.Movimientos
{
    /// <summary>
    /// DTO de entrada para registrar un movimiento, representa el cuerpo del request HTTP
    /// </summary>
    public class RegistrarMovimientoRequest
    {
        /// <summary>
        /// Numero de cuenta sobre el cual se realiza el movimiento
        /// </summary>
        public string NumeroCuenta { get; set;} = string.Empty;
        /// <summary>
        /// Tipo de movimiento (credito o debito)
        /// </summary>
        public TipoMovimiento TipoMovimiento { get; set;}
        /// <summary>
        /// Valor del movimiento para debito se envia positivo
        /// </summary>
        public decimal Valor { get; set;}
    }
}