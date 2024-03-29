
namespace Dominio.Models
{
    public class Curso
    {
            public Guid              Id               { get; set; }
            public string            Titulo           { get; set; }
            public string            Descripcion      { get; set; }
            public DateTime          FechaPublicacion { get; set; }
            public virtual byte[]    FotoPortada      { get; set; }
           
            
        public Precio                     PrecioPromocion   { get; set; }

        public ICollection<Comentario>    ComentarioLista   { get; set; }

        public ICollection<CursoInstructor> InstructoresLink { get; set; }
      
    }

}
