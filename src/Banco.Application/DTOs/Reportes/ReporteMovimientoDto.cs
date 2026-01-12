namespace Banco.Application.DTOs.Reportes
{
    /// <summary>
    /// DTO que representa un movimiento dentro del reporte
    /// </summary>
    public class ReporteMovimientoDto
    {
        public DateTime Fecha { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoMovimiento { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}