using Dominio.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.FluentConfig
{
    public class ComentarioConfig
    {

        public ComentarioConfig(EntityTypeBuilder<Comentario> entity)
        {
            entity.ToTable("Comentario");
            entity.HasKey(p => p.Id);


            entity
                .HasOne<Curso>(s => s.Curso)
                .WithMany(s => s.ComentarioLista)
                .HasForeignKey(s => s.CursoId);

            entity.Property(p => p.Alumno).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Puntaje).IsRequired();
            entity.Property(p => p.ComentarioTexto).IsRequired().HasMaxLength(250);

        }


    }
}
