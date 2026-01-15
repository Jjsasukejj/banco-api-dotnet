using System.Globalization;
using Banco.Application.DTOs.Reportes;
using Banco.Application.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Banco.Infrastructure.Pdf
{
    /// <summary>
    /// Implementacion de generacion de PDF, se codifica en Base64
    /// </summary>
    public class PdfGenerator : IPdfGenerator
    {
        public string GenerarReportePdf(string cliente, IEnumerable<ReporteMovimientoDto> movimientos, decimal totalCredito, decimal totalDebitos)
        {
            var culture = new CultureInfo("en-US");
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Estado de Cuenta - {cliente}")
                            .FontSize(18)
                            .Bold();
                        
                        col.Item().Text($"Total Créditos: {totalCredito}");
                        col.Item().Text($"Total Débitos: {totalDebitos}");

                        col.Item().PaddingVertical(10).Text("Movimientos").Bold();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cd =>
                            {
                                cd.RelativeColumn();
                                cd.RelativeColumn();
                                cd.RelativeColumn();
                                cd.RelativeColumn();
                                cd.RelativeColumn();
                            });
                            table.Header(header =>
                            {
                                header.Cell().Text("Fecha").Bold();
                                header.Cell().Text("Cuenta").Bold();
                                header.Cell().Text("Tipo").Bold();
                                header.Cell().Text("Valor").Bold();
                                header.Cell().Text("Saldo").Bold();
                            });
                            foreach(var mov in movimientos)
                            {
                                table.Cell().Text(mov.Fecha.ToString("dd/MM/yyyy"));
                                table.Cell().Text(mov.NumeroCuenta);
                                table.Cell().Text(mov.TipoMovimiento);
                                table.Cell().Text(mov.Valor.ToString("C", culture));
                                table.Cell().Text(mov.Saldo.ToString("C", culture));
                            }
                        });
                    });
                });
            });
            //Pdf en bytes
            byte[] pdfBytes = document.GeneratePdf();
            //Pdf en base64
            return Convert.ToBase64String(pdfBytes);
        }
    }
}