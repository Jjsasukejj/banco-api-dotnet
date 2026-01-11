using Banco.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infrastructure.Data
{
    /// <summary>
    /// Dbcontext principal de la aplicacion
    /// Responsable de mapear las entidades del dominio
    /// hacia el modelo de la relacional de la base de datos
    /// </summary>
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Cuenta> Cuentas => Set<Cuenta>();
        public DbSet<Movimiento> Movimientos => Set<Movimiento>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aplica automaticamente todas las configuraciones 
            //que implementen IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoDbContext).Assembly);
        }
    }
}