using Banco.Application.Interfaces.Repositories;
using Banco.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infrastructure.Repositories
{
    /// <summary>
    /// Implementacion generica de repositorio usando EF Core
    /// Encapsula DbSet y evita que EF Core sea expuesto a Application.
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BancoDbContext bancoDbContext;
        protected readonly DbSet<T> dbSet;

        public GenericRepository(BancoDbContext bancoDbContext)
        {
            this.bancoDbContext = bancoDbContext;
            dbSet = bancoDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}