using Banco.Domain.Enums;

namespace Banco.Domain.Entities
{
    /// <summary>
    /// Representa un movimiento financiero realizado sobre una cuenta
    /// registra el valor del movimiento y el sando resultante
    /// </summary>
    public class Movimiento
    {
        /// <summary>
        /// Fecha del movimiento financiero
        /// </summary>
        public DateTime Fecha { get; private set; }
        /// <summary>
        /// FTipo de movimiento (Credito/Debito)
        /// </summary>
        public TipoMovimiento TipoMovimiento { get; private set; }
        /// <summary>
        /// valor del movimiento
        /// </summary>
        public decimal Valor { get; private set; }
        /// <summary>
        /// saldo del movimiento
        /// </summary>
        public decimal Saldo { get; private set; }
        /// <summary>
        /// relacion con la cuenta afectada
        /// </summary>
        public int CuentaId { get; private set; }
        public Cuenta? Cuenta { get; private set; }
        /// <summary>
        /// Constructor protegido por EF core
        /// </summary>
        protected Movimiento() {}
        public Movimiento(
            DateTime fecha,
            TipoMovimiento tipoMovimiento,
            decimal valor,
            decimal saldo,
            int cuentaId)
        {
            Fecha = fecha;
            TipoMovimiento = tipoMovimiento;
            Valor = valor;
            Saldo = saldo;
            CuentaId = cuentaId;
        }
    }
}