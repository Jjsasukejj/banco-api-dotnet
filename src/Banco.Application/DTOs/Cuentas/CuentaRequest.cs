using Banco.Domain.Enums;

namespace Banco.Application.DTOs.Cuentas
{
    /// <summary>
    /// DTO de entrada para crear una cuenta
    /// </summary>
    public class CuentaRequest
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public TipoCuenta TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
    }
}