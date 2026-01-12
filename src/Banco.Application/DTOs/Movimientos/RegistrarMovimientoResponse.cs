namespace Banco.Application.DTOs.Movimientos
{
    /// <summary>
    /// DTO de salida al registrar un movimiento, devuelve la informacion relevante al cliente.
    /// </summary>
    public class RegistrarMovimientoResponse
    {
        public DateTime Fecha { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoMovimiento { get; set; } = string.Empty;
        public decimal Valor { get; set; } 
        public decimal Saldo { get; set; } 
    }
}