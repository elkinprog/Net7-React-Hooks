using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig
{
    internal class InstructorConfig
    {

        public InstructorConfig(EntityTypeBuilder<Instructor> entity)
        {
            entity.ToTable("Instructor");
            entity.HasKey(x => x.Id);

            entity.Property(p => p.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Apellido).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Grado).IsRequired();
            entity.Property(p => p.FotoPerfil);


        }

    }
}
