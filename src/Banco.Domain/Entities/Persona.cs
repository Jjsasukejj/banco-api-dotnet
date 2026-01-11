using Banco.Domain.Base;

namespace Banco.Domain.Entities
{
    /// <summary>
    /// Representa a un persona dentro del dominio
    /// se define como abstracta por que no debe instanciarse directamente,
    /// sino ser heredada por entidades concreatas como Cliente
    /// </summary>
    public class Persona : EntityBase
    {
        /// <summary>
        /// Nombre completo de la persona
        /// </summary>
        public string Nombre { get; protected set; } = string.Empty;
        /// <summary>
        /// Genero de la persona
        /// </summary>
        public string Genero { get; protected set; } = string.Empty;
        /// <summary>
        /// Edad de la persona
        /// </summary>
        public int Edad { get; protected set; }
        /// <summary>
        /// Identificacion de la persona(cedula, DNI, etc)
        /// </summary>
        public string Identificacion { get; protected set; } = string.Empty;
        /// <summary>
        /// Direccion domiciliaria de la persona
        /// </summary>
        public string Direccion { get; protected set; } = string.Empty;
        /// <summary>
        /// NOmero telefonico de la persona
        /// </summary>
        public string Telefono { get; protected set; } = string.Empty;
    }
}