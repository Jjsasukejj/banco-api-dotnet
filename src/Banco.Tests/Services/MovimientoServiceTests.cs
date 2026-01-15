using Banco.Application.Interfaces.Repositories;
using Banco.Application.Services;
using Banco.Domain.Entities;
using Banco.Domain.Enums;
using FluentAssertions;
using Moq;

namespace Banco.Tests.Services
{
    public class MovimientoServiceTests
    {
        [Theory]
        [InlineData(100, 500)]
        [InlineData(0, 1)]
        [InlineData(50, 100)]
        public async Task RegistrarMovimientoAsync_DebitoConSaldoInsuficiente_DebeLanzarExcepcion(
            decimal saldoInicial,
            decimal valorDebito)
        {
            //Arrange (preparacion)
            var uowMock = new Mock<IUnitOfWork>();
            //Creamos una cuenta con saldo bajo
            var cuenta = new Cuenta(
                numeroCuenta: "478758",
                tipoCuenta: TipoCuenta.Ahorro,
                saldo: saldoInicial,
                estado: true,
                clienteId: "CLI001"
            );
            //Simulamos que el servicio de cuentas encuentra la cuenta por numero
            uowMock.Setup(x => x.Cuentas.GetByNumeroCuentaAsync("478758")).ReturnsAsync(cuenta);
            //Creamos el servicio real usando los mocks
            var servicio = new MovimientoService(uowMock.Object);
            //Act (Accion)
            Func<Task> act = async () => await servicio.RegistrarMovimientoAsync("478758", TipoMovimiento.Debito, valorDebito);
            //Assert (Validacion)
            await act
                .Should()
                .ThrowAsync<InvalidOperationException>();
            //No debe guardar nada en base de datos
            uowMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Theory]
        [InlineData(0, 500, 500)]
        [InlineData(1000, 250, 1250)]
        [InlineData(3000, 1000, 4000)]
        public async Task RegistrarMovimientoAsync_CreditoValido_DebeRegistrarMovimientoYActualizarSaldo(
            decimal saldoInicial,
            decimal valorCredito,
            decimal saldoEsperado)
        {
            //Arrange
            var uowMock = new Mock<IUnitOfWork>();

            // Cuenta inicial con saldo variable
            var cuenta = new Cuenta(
                numeroCuenta: "478758",
                tipoCuenta: TipoCuenta.Ahorro,
                saldo: saldoInicial,
                estado: true,
                clienteId: "CLI001"
            );
            // Mock del repositorio de cuentas
            uowMock.Setup(x => x.Cuentas.GetByNumeroCuentaAsync("478758")).ReturnsAsync(cuenta);

            // Mock del repositorio de movimientos (AddAsync)
            uowMock.Setup(x => x.Movimientos.AddAsync(It.IsAny<Movimiento>())).Returns(Task.CompletedTask);

            var servicio = new MovimientoService(uowMock.Object);
            //Act
            var movimiento = await servicio.RegistrarMovimientoAsync("478758", TipoMovimiento.Credito, valorCredito);
            // Se crea un movimiento
            movimiento.Should().NotBeNull();
            movimiento.TipoMovimiento.Should().Be(TipoMovimiento.Credito);
            movimiento.Valor.Should().Be(valorCredito);
            movimiento.Saldo.Should().Be(saldoEsperado);
            // El saldo de la cuenta se actualiza
            cuenta.Saldo.Should().Be(saldoEsperado);
            // Se guarda en base de datos
            uowMock.Verify(x => x.Movimientos.AddAsync(It.IsAny<Movimiento>()), Times.Once);
            uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}