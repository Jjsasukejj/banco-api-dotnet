using Banco.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banco.Infrastructure.Configurations
{
    /// <summary>
    /// COnfiguracion EF core para la entidad Movimiento
    /// </summary>
    public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            //Define el nombre de la tabla en la base de datos
            //evita que ef core use nombres por convencion
            builder.ToTable("Movimientos");
            //Define la clave primaria de la entidad
            //corresponde a la propiedad Id heredada desde Entitybase
            builder.HasKey(x => x.Id);
            //Configura la propiedad Valor como decimal, ajustando precision y escala 
            builder.Property(x => x.Valor)
                   .HasColumnType("decimal(18,2)");
            //Configura la propiedad Saldo como decimal, ajustando precision y escala 
            builder.Property(x => x.Saldo)
                   .HasColumnType("decimal(18,2)");
            //Configura el enum TipoMovimiento
            //Se almacena como entero en la base de datos
            builder.Property(x => x.TipoMovimiento)
                   .HasConversion<int>();
        }
    }
}