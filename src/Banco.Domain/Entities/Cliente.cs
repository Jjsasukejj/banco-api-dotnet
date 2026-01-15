namespace Banco.Domain.Entities
{
    /// <summary>
    /// Representa a un cliente del banco
    /// Hereda de Persona para reutilizar atributos comunes
    /// y agrega informacion especifica del contexto bancario
    /// </summary>
    public class Cliente : Persona
    {
        /// <summary>
        /// identificador funcional del cliente (clave unica de negocio)
        /// </summary>
        public string ClienteId { get; private set;} = string.Empty;
        /// <summary>
        /// Contraseña del cliente.
        /// En un escenario real se almacenaria cifrada
        /// </summary>
        public string Contrasena { get; private set;} = string.Empty;
        /// <summary>
        /// Indica si el cliente se encuentra activo o inactivo
        /// </summary>
        public bool Estado { get; private set;}
        /// <summary>
        /// Coleccion de cuentas asociadas al cliente.
        /// Se expone como solo lectura para proteger el dominio.
        /// </summary>
        public IReadOnlyCollection<Cuenta> Cuentas => _cuentas;
        private readonly List<Cuenta> _cuentas = new();
        /// <summary>
        /// Constructor protegido requerido por ORMs como EF core
        /// </summary>
        protected Cliente() {}
        /// <summary>
        /// Contructor principal para crear un cliente valido dentro del dominio.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="nombre"></param>
        /// <param name="genero"></param>
        /// <param name="edad"></param>
        /// <param name="identificacion"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        /// <param name="contrasena"></param>
        /// <param name="estado"></param>
        public Cliente(
            string clienteId,
            string nombre,
            string genero,
            int edad,
            string identificacion,
            string direccion,
            string telefono,
            string contrasena,
            bool estado)
        {
            ClienteId = clienteId;
            Nombre = nombre;
            Genero = genero;
            Edad = edad;
            Identificacion = identificacion;
            Direccion = direccion;
            Telefono = telefono;
            Contrasena = contrasena;
            Estado = estado;
        }
        /// <summary>
        /// Agrega una cuenta al cliente, controla la relacion Cliente - Cuenta dentro del dominio
        /// </summary>
        /// <param name="cuenta"></param>
        public void AgregarCuenta(Cuenta cuenta)
        {
            if (cuenta == null)
            {
                throw new ArgumentNullException(nameof(cuenta));
            }

            //Regla de dominio 
            if (cuenta.ClienteId != ClienteId)
            {
                throw new InvalidOperationException("La cuenta no pertenece a este cliente.");
            }

            _cuentas.Add(cuenta);
        }
    }
}