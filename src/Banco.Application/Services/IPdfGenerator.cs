using Banco.Application.DTOs.Reportes;

namespace Banco.Application.Services
{
    /// <summary>
    /// Contrato para generacion de PDFs, permite cambiar la implementacion sin afectar el servicio
    /// </summary>
    public interface IPdfGenerator
    {
        string GenerarReportePdf(string cliente, IEnumerable<ReporteMovimientoDto> movimientos, decimal totalCredito, decimal totalDebitos);
    }
}