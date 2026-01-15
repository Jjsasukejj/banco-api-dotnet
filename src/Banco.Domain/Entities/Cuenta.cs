using Banco.Domain.Base;
using Banco.Domain.Enums;

namespace Banco.Domain.Entities
{
    /// <summary>
    /// representa una cuenta bancaria.
    /// Es responsdable de mantener el saldo y los moviminetos asociados.
    /// </summary>
    public class Cuenta : EntityBase
    {
        /// <summary>
        /// Numero unico de la cuenta
        /// </summary>
        public string NumeroCuenta { get; private set;} = string.Empty;
        /// <summary>
        /// Tipo de cuenta (Ahorro/Corriente)
        /// </summary>
        public TipoCuenta TipoCuenta { get; private set;}
        /// <summary>
        /// Saldo actual de la cuenta
        /// </summary>
        public decimal Saldo { get; private set;}
        /// <summary>
        /// Numero unico de la cuenta
        /// </summary>
        public bool Estado { get; private set;}
        /// <summary>
        /// Relacion con el cliente propietario
        /// </summary>
        public string ClienteId { get; private set; } = string.Empty;
        public int ClienteDbId { get; private set; }
        public Cliente? Cliente { get; private set; }
        /// <summary>
        /// Movimientos asociados a la cuenta
        /// Se expone como solo lectura para mantener consistencia
        /// </summary>
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos;
        private readonly List<Movimiento> _movimientos = new();
        /// <summary>
        /// Contructor protegido requerido por EF Core
        /// </summary>
        protected Cuenta() {}
        /// <summary>
        /// Constructor principal para crear una cuenta valida 
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <param name="tipoCuenta"></param>
        /// <param name="saldo"></param>
        /// <param name="estado"></param>
        /// <param name="clienteId"></param>
        public Cuenta(
            string numeroCuenta,
            TipoCuenta tipoCuenta,
            decimal saldo,
            bool estado,
            string clienteId)
        {
            NumeroCuenta = numeroCuenta;
            TipoCuenta = tipoCuenta;
            Saldo = saldo;
            Estado = estado;
            ClienteId = clienteId;
        }
        /// <summary>
        /// Aplica un movimiento a la cuenta y actualiza el saldo
        /// </summary>
        /// <param name="movimiento"></param>
        public void AplicarMovimiento(Movimiento movimiento)
        {
            Saldo = movimiento.Saldo;
            _movimientos.Add(movimiento);
        }

        public void AsignarCliente(int clienteDbId)
        {
            ClienteDbId = clienteDbId;
        }
    }
}