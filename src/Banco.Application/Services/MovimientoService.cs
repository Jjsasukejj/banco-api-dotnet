using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Entities;
using Banco.Domain.Enums;

namespace Banco.Application.Services
{
    /// <summary>
    /// Servicio que contiene la logica de negocio para el manejo de movimientos bancarios
    /// Aqui se validan reglas como saldo disponible y cupo diario
    /// </summary>
    public class MovimientoService : IMovimientoService
    {
        private readonly IUnitOfWork unitOfWork;
        /// <summary>
        /// El servicio depende del unitOfWork para acceder a repositorios y persistir cambios de forma controlada
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MovimientoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// registra un movimiento de credito o debito sobre una cuenta
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <param name="tipoMovimiento"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Movimiento> RegistrarMovimientoAsync(string numeroCuenta, TipoMovimiento tipoMovimiento, decimal valor)
        {
            //Obtener la cuenta
            var cuenta = await unitOfWork.Cuentas.GetByNumeroCuentaAsync(numeroCuenta);
            //Validamos si existe la cuenta 
            if (cuenta == null)
            {
                throw new InvalidOperationException("La cuenta no existe.");
            }

            if (!cuenta.Estado)
            {
                throw new InvalidOperationException("La cuenta se encuentra inactiva");
            }
            //Determinar el valor real del movimiento
            //Cretido valor positivo
            //Debito valor negativo
            decimal valorMovimiento = tipoMovimiento == TipoMovimiento.Debito ? -valor : valor;
            //Validamos si tiene saldo disponible (solo para debitos)
            if (tipoMovimiento == TipoMovimiento.Debito)
            {
                //Si el saldo actual es menor al valor a debitar, se rechaza
                if (cuenta.Saldo < valor)
                {
                    throw new InvalidOperationException("Saldo no disponible.");
                }
                //Validacion de cupo diario
                //Se consulta cuanto se ha retirado hoy
                var hoy = DateTime.Today;

                var movimientosHoy = await unitOfWork.Movimientos
                    .GetByCuentaAndFechaAsync(cuenta.Id, hoy, hoy.AddDays(1));

                //Se suma unicamente los debitos realizados hoy
                var totalDebitadoHoy = movimientosHoy
                    .Where(x => x.TipoMovimiento == TipoMovimiento.Debito)
                    .Sum(x => Math.Abs(x.Valor));
                //Si el nuevo retiro supera el limite diario, se rechaza
                if (totalDebitadoHoy + valor > 1000)
                {
                    throw new InvalidOperationException("Cupo diario excedido.");
                }
            }
            //Calcular el nuevo saldo
            var nuevoSaldo = cuenta.Saldo + valorMovimiento;
            //crear la entidad Movimiento
            var movimiento = new Movimiento
            (
                fecha: DateTime.Now,
                tipoMovimiento: tipoMovimiento,
                valor: valorMovimiento,
                saldo: nuevoSaldo,
                cuentaId: cuenta.Id
            );
            //Actualizar el estado de la cuenta
            //El saldo se actualiza a traves del metodo de dominio
            cuenta.AplicarMovimiento(movimiento);
            //registrar movimiento en el repositorio
            await unitOfWork.Movimientos.AddAsync(movimiento);
            //persistir todos los cambios en una sola transaccion logica
            await unitOfWork.SaveChangesAsync();
            //retornar el movimiento creado
            return movimiento;
        }
    }
}