using Banco.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banco.Infrastructure.Configurations
{
    /// <summary>
    /// Configuracion EF Core para la entidad Cuenta
    /// </summary>
    public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            //Define el nombre de la tabla en la base de datos
            //evita que ef core use nombres por convencion
            builder.ToTable("Cuentas");
            //Define la clave primaria de la entidad
            //corresponde a la propiedad Id heredada desde Entitybase
            builder.HasKey(x => x.Id);
            //Configura la propiedad NumeroCuenta como obligatoria
            //Representa el numero de cuenta del cliente
            builder.Property(x => x.NumeroCuenta)
                   .IsRequired() //No permite valores nulos
                   .HasMaxLength(20); //Define el tamaño maximo de la BD
            //Crea un indice unico sobre NumeroCuenta
            //Garantiza que no existan dos clientes con el mismo numero de cuenta
            builder.HasIndex(x => x.NumeroCuenta)
                   .IsUnique();
            //Configura la propiedad Saldo como decimal, ajustando precision y escala 
            builder.Property(x => x.Saldo)
                   .HasColumnType("decimal(18,2)");
            //Configura el enum TipoCuenta
            //Se almacena como entero en la base de datos
            builder.Property(x => x.TipoCuenta)
                   .HasConversion<int>();
            //Define la relacion uno a muchos
            //una cuenta puede tener muchos movimientos 
            //un movimiento pertenece a una sola cuenta
            //se define explicitamente la clave foranea CuentaId
            builder.HasMany(x => x.Movimientos)
                   .WithOne(x => x.Cuenta)
                   .HasForeignKey(x => x.CuentaId);
        }
    }
}