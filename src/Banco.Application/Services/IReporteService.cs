using Banco.Application.DTOs.Reportes;

namespace Banco.Application.Services
{
    /// <summary>
    /// Contrato del servicio de reportes, orquesta la generacion del estado de cuenta
    /// </summary>
    public interface IReporteService
    {
        Task<ReporteResponse> GenerarReporteAsync(ReporteRequest request);
    }
}