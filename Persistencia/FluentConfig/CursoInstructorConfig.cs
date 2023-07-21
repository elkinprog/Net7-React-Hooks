using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig
{
    internal class CursoInstructorConfig
    {

        public CursoInstructorConfig(EntityTypeBuilder<CursoInstructor> entity)
        {

            entity.ToTable("CursoInstructor");

            entity
                .HasOne<Curso>(sc => sc.Curso)
                .WithMany(sc => sc.InstructoresLink)
                .HasForeignKey(sc => sc.CursoId);

            entity
              .HasOne<Instructor>(sc => sc.Instructor)
              .WithMany(sc => sc.CursoLink)
              .HasForeignKey(sc => sc.InstructorId);
        }

    }
}
