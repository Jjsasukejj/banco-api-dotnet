namespace Banco.Domain.Base
{
    /// <summary>
    /// Calse base para todas las entidades del dominio.
    /// Centraliza la propiedad Id y evita duplicacion.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Identificador unico de la entidad
        /// El setter es protegido para evitar modificaciones externas.
        /// </summary>
        public int Id {get; protected set; }
    }
}