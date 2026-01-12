using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Banco.Infrastructure.Data
{
    /// <summary>
    /// factory utilizada pro EF Core en tiempo de diseño, permite vcrear una instancia de 
    /// BancoDbContext sin depender del contenedor de inyeccion de dependencias ni de los
    /// servicio de la aplicacion.
    /// </summary>
    public class BancoDbContextFactory : IDesignTimeDbContextFactory<BancoDbContext>
    {
        public BancoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BancoDbContext>();
            //cadena de conexion usada solo para tooling(migrations/scripts)
            optionsBuilder.UseSqlServer("Server=localhost;Database=BancoDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new BancoDbContext(optionsBuilder.Options);
        }
    }
}