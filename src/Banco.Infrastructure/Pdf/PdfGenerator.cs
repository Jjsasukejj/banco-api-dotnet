using System.Text;
using Banco.Application.DTOs.Reportes;
using Banco.Application.Services;

namespace Banco.Infrastructure.Pdf
{
    /// <summary>
    /// Implementacion de generacion de PDF, se codifica en Base64
    /// </summary>
    public class PdfGenerator : IPdfGenerator
    {
        public string GenerarReportePdf(string cliente, IEnumerable<ReporteMovimientoDto> movimientos, decimal totalCredito, decimal totalDebitos)
        {
            //Implementacion para la generacion del reporte
            var sb = new StringBuilder();

            sb.AppendLine($"Estado de Cuenta - {cliente}");
            sb.AppendLine($"Total Creditos: {totalCredito}");
            sb.AppendLine($"Total Debitos: {totalDebitos}");
            sb.AppendLine("Movimientos");

            foreach (var m in movimientos)
            {
                sb.AppendLine($"{m.Fecha:dd/MM/yyyy} | {m.NumeroCuenta} | {m.TipoMovimiento} | {m.Valor} | {m.Saldo}");
            }

            //Simulacion de PDF 
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(bytes);
        }
    }
}