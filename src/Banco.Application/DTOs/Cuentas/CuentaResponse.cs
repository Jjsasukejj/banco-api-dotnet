using Banco.Domain.Enums;

namespace Banco.Application.DTOs.Cuentas
{
    /// <summary>
    /// DTO de salida para exponer cuentas
    /// </summary>
    public class CuentaResponse
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public TipoCuenta TipoCuenta { get; set; }
        public decimal Saldo { get; set; }
        public bool Estado { get; set; }
    }
}