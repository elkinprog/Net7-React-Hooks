using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig
{
    public class PrecioConfig
    {

        public PrecioConfig(EntityTypeBuilder<Precio> entity)
        {
            entity.ToTable("Precio");
            entity.HasKey(p => p.Id);


            entity.Property(p => p.PrecioActual).IsRequired();
            entity.Property(p => p.Promocion).IsRequired();

        }

    }
}
