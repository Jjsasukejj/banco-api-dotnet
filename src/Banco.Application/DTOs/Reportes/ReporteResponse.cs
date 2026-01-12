namespace Banco.Application.DTOs.Reportes
{
    /// <summary>
    /// DTO de salida del reporte, contiene resumen y detalle
    /// </summary>
    public class ReporteResponse
    {
        public string Cliente { get; set; } = string.Empty;
        public decimal TotalCredito { get; set; }
        public decimal TotalDebito { get; set; }
        public IEnumerable<ReporteMovimientoDto> Movimientos { get; set; } = Enumerable.Empty<ReporteMovimientoDto>();
        /// <summary>
        /// PDF del reporte codificado en Base64, permite descargar desde frontend
        /// </summary>
        public string PDFBase64 { get; set; } = string.Empty;
    }
}