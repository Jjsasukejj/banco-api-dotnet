using Banco.Application.DTOs.Reportes;
using Banco.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Api.Controllers
{
    /// <summary>
    /// Controlador que expone el endpoint de reportes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService reporteService;

        public ReportesController(IReporteService reporteService)
        {
            this.reporteService = reporteService;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerReporte([FromQuery] string clienteId, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var request = new ReporteRequest
                {
                    ClienteId = clienteId,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                };

                var reporte = await reporteService.GenerarReporteAsync(request);

                return Ok(reporte); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}