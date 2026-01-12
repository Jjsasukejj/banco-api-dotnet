namespace Banco.Application.DTOs.Clientes
{
    /// <summary>
    /// DTO de salida para exponer clientes por la api
    /// </summary>
    public class ClienteResponse
    {
        public string ClienteId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}