namespace Banco.Application.DTOs.Clientes
{
    /// <summary>
    /// DTO de entrada para crear/actualizar un cliente, representa el contrato HTTP
    /// </summary>
    public class ClienteRequest
    {
        public string ClienteId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}