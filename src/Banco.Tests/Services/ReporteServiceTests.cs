using Banco.Application.DTOs.Reportes;
using Banco.Application.Interfaces.Repositories;
using Banco.Application.Services;
using Banco.Domain.Entities;
using Banco.Domain.Enums;
using FluentAssertions;
using Moq;

namespace Banco.Tests.Services
{
    public class ReporteServiceTests
    {
        [Theory]
        // credito, debito, totalCreditoEsperado, totalDebitoEsperado
        [InlineData(500, 300, 500, 300)]
        [InlineData(1000, 200, 1000, 200)]
        [InlineData(250, 50, 250, 50)]
        public async Task GenerarReporteAsync_ConMovimientos_DebeCalcularTotalesYGenerarPdf(
            decimal valorCredito,
            decimal valorDebito,
            decimal totalCreditoEsperado,
            decimal totalDebitoEsperado)
        {
            //Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var pdfMock = new Mock<IPdfGenerator>();

            var cliente = new Cliente(
                clienteId: "CLI001",
                nombre: "Victor Cadena",
                genero: "M",
                edad: 30,
                identificacion: "123",
                direccion: "X",
                telefono: "999",
                contrasena: "123",
                estado: true
            );

            var cuenta = new Cuenta(
                numeroCuenta: "478758",
                tipoCuenta: TipoCuenta.Ahorro,
                saldo: 3000m,
                estado: true,
                clienteId: "CLI001"
            );

            var fechMovimiento = new DateTime(2026,1,10);

            //Asignamos la cuenta al cliente
            cliente.AgregarCuenta(cuenta);

            // Movimientos parametrizados
            var movimientos = new List<Movimiento>
            {
                // Credito
                new Movimiento(
                    fechMovimiento,
                    TipoMovimiento.Credito,
                    valorCredito,
                    cuenta.Saldo + valorCredito,
                    cuenta.Id
                ),

                // Debito (se guarda como negativo en tu dominio)
                new Movimiento(
                    fechMovimiento,
                    TipoMovimiento.Debito,
                    -valorDebito,
                    cuenta.Saldo + valorCredito - valorDebito,
                    cuenta.Id
                )
            };

            // Mock repositorio clientes
            uowMock
                .Setup(x => x.Clientes.GetByClienteIdAsync("CLI001"))
                .ReturnsAsync(cliente);

            // Mock repositorio movimientos
            uowMock
                .Setup(x => x.Movimientos.GetByCuentaAndFechaAsync(
                    cuenta.Id,
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .ReturnsAsync(movimientos);

            // Mock PDF generator
            pdfMock
                .Setup(x => x.GenerarReportePdf(
                    cliente.Nombre,
                    It.IsAny<IEnumerable<ReporteMovimientoDto>>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()))
                .Returns("PDF_BASE64_FAKE");

            var service = new ReporteService(uowMock.Object, pdfMock.Object);

            var request = new ReporteRequest
            {
                ClienteId = "CLI001",
                FechaInicio = new DateTime(2026, 1, 1),
                FechaFin = new DateTime(2026, 1, 31)
            };

            // Act
            var result = await service.GenerarReporteAsync(request);

            // Assert
            result.TotalCredito.Should().Be(totalCreditoEsperado);
            result.TotalDebito.Should().Be(totalDebitoEsperado);
            result.PDFBase64.Should().Be("PDF_BASE64_FAKE");

            pdfMock.Verify(x => x.GenerarReportePdf(
                cliente.Nombre,
                It.IsAny<IEnumerable<ReporteMovimientoDto>>(),
                totalCreditoEsperado,
                totalDebitoEsperado
            ), Times.Once);

        }
    }
}