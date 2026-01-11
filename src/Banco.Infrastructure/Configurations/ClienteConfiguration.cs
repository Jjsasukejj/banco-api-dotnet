using Banco.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banco.Infrastructure.Configurations
{
    /// <summary>
    /// Configuracion EF Core para la entidad Cliente
    /// </summary>
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Define el nombre de la tabla en la base de datos
            //evita que ef core use nombres por convencion
            builder.ToTable("Clientes");
            //Define la clave primaria de la entidad
            //corresponde a la propiedad Id heredada desde Entitybase
            builder.HasKey(x => x.Id);
            //Configura la propiedad ClienteId como obligatoria
            //Representa la clave de negocio del cliente
            builder.Property(x => x.ClienteId)
                   .IsRequired() //No permite valores nulos
                   .HasMaxLength(20); //Define el tamaño maximo de la BD
            //Crea un indice unico sobre ClienteId
            //Garantiza que no existan dos clientes con el mismo identificador
            builder.HasIndex(x => x.ClienteId)
                   .IsUnique();
            //Configura la propiedad nombre como obligatoria 
            //Se limita el tamaño para proteger integridad y performance
            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);
            //Define la relacion uno a muchos
            //un cliente puede tener muchas Cuentas 
            //una cuenta pertenece a un solo cliente
            //se define explicitamente la clave foranea ClienteId
            builder.HasMany(x => x.Cuentas)
                   .WithOne(x => x.Cliente)
                   .HasForeignKey(x => x.ClienteId);
        }
    }
}