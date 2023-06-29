using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Persistencia.FluentConfig;

namespace Persistencia
{
        public class CursosOnlineContext : DbContext 
        {
            public CursosOnlineContext(DbContextOptions options) : base (options) {   
            }
    
            protected override void OnModelCreating(ModelBuilder modelBuilder){

                modelBuilder.Entity<CursoInstructor>().HasKey(sc => new {sc.InstructorId,sc.CursoId});

            new CursoConfig(modelBuilder.Entity<Curso>());
            new InstructorConfig(modelBuilder.Entity<Instructor>());
            new ComentarioConfig(modelBuilder.Entity<Comentario>());
            new PrecioConfig(modelBuilder.Entity<Precio>());
        }

       

        public DbSet<Curso> Curso { get; set; }
            public DbSet<CursoInstructor> CursoInstructor { get; set; }
            public DbSet<Instructor> Instructor { get; set; }
            public DbSet<Comentario> Comentario {get;set;} 
            public DbSet<Precio> Precio {get;set;}  
        }      

} 