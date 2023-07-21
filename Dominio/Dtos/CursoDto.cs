using Dominio.Dtos;

namespace Aplicacion.Cursos
{
    public  class CursoDto
    {


        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public virtual byte[] FotoPortada { get; set; }


        public ICollection<InstructorDto>  Instructores { get; set; }
    }
}
