namespace Banco.Application.DTOs.Reportes
{
    /// <summary>
    /// DTO de entrada para solicitar el reporte, define el rango de fechas y el cliente
    /// </summary>
    public class ReporteRequest
    {
        public string ClienteId { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}