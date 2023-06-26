using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig
{
    internal class CursoConfig
    {


        public CursoConfig(EntityTypeBuilder<Curso> entity)
        {

            entity.ToTable("Curso");
            entity.HasKey("Id");

            entity
                .HasOne<Precio>(s => s.PrecioPromocion)
                .WithOne(s => s.Curso)
                .HasForeignKey<Precio>(s => s.CursoId);

            entity.Property(p => p.Titulo).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Descripcion).IsRequired().HasMaxLength(250);
            entity.Property(p => p.FechaPublicacion).IsRequired().HasMaxLength(50);
            entity.Property(p => p.FotoPortada);
        }


    }
}
