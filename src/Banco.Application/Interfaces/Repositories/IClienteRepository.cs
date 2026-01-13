using Banco.Domain.Entities;

namespace Banco.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repositorio especifico para la entidad Cliente
    /// Aqui se define consultas propias del dominio Cliente
    /// </summary>
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        /// <summary>
        /// Obtiene un cliente usando su identificador
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        Task<Cliente?> GetByClienteIdAsync(string clienteId);
        /// <summary>
        /// Verifica si ya existe un cliente con el ClienteId especificado
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        Task<bool> ExisteClienteIdAsync(string clienteId);
    }
}