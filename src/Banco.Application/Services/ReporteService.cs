using Banco.Application.DTOs.Reportes;
using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Enums;

namespace Banco.Application.Services
{
    /// <summary>
    /// Servicio que construye el reporte de estado de cuenta
    /// Agrega informacion de multiples fuentes
    /// Cliente, Cuentas, Movimientos
    /// </summary>
    public class ReporteService : IReporteService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPdfGenerator pdfGenerator;
        /// <summary>
        /// Se inyecta UnitOfWork para acceder a datos y un genrador de PDF desacoplado
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="pdfGenerator"></param>
        public ReporteService(IUnitOfWork unitOfWork, IPdfGenerator pdfGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.pdfGenerator = pdfGenerator;
        }
        public async Task<ReporteResponse> GenerarReporteAsync(ReporteRequest request)
        {
            //Obtener cliente por su identificador
            var cliente = await unitOfWork.Clientes.GetByClienteIdAsync(request.ClienteId);
            //Recolectar movimientos de todas las cuentas del cliente
            var movimientosReporte = new List<ReporteMovimientoDto>(); 
            foreach (var cuenta in cliente.Cuentas)
            {
                var movimientos = await unitOfWork.Movimientos
                    .GetByCuentaAndFechaAsync(
                        cuenta.Id,
                        request.FechaInicio,
                        request.FechaFin);

                // Mapear movimientos a DTO del reporte
                movimientosReporte.AddRange(
                    movimientos.Select(m => new ReporteMovimientoDto
                    {
                        Fecha = m.Fecha,
                        NumeroCuenta = cuenta.NumeroCuenta,
                        TipoMovimiento = m.TipoMovimiento.ToString(),
                        Valor = m.Valor,
                        Saldo = m.Saldo
                    }));
            }
            //Calcular totales
            var totalCreditos = movimientosReporte
                .Where(m => m.TipoMovimiento == TipoMovimiento.Credito.ToString())
                .Sum(m => m.Valor);

            var totalDebitos = movimientosReporte
                .Where(m => m.TipoMovimiento == TipoMovimiento.Debito.ToString())
                .Sum(m => Math.Abs(m.Valor));
            //Generar reporte PDF
            var pdfBase64 = pdfGenerator.GenerarReportePdf(
                cliente.Nombre,
                movimientosReporte,
                totalCreditos,
                totalDebitos);
            //Construir respuesta final
            return new ReporteResponse
            {
                Cliente = cliente.Nombre,
                TotalCredito = totalCreditos,
                TotalDebito = totalDebitos,
                Movimientos = movimientosReporte.OrderBy(x => x.Fecha),
                PDFBase64 = pdfBase64
            };
        }
    }
}