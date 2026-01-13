using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Entities;
using Banco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacion del repositorio de Cliente usando EF Core
    /// </summary>
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(BancoDbContext bancoDbContext) : base(bancoDbContext)
        {
        }

        public async Task<bool> ExisteClienteIdAsync(string clienteId)
        {
            //Verifica si ya existe un clinente con el mismo CLienteId
            return await bancoDbContext.Clientes
                .AnyAsync(x => x.ClienteId == clienteId);
        }

        public async Task<Cliente?> GetByClienteIdAsync(string clienteId)
        {
            //Obtiene un cliente a partir de su identificador (ClienteId) incluyendo sus cuentas asociadas
            //Se utiliza include para cargar las cuentas en una sola consulta
            return await bancoDbContext.Clientes.Include(x => x.Cuentas).FirstOrDefaultAsync(x => x.ClienteId == clienteId);
        }
    }
}