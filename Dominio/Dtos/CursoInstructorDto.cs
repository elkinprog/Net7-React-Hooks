using Dominio.Models;

namespace Dominio.Dtos
{
    public class CursoInstructorDto
    {
        public Guid CursoId { get; set; }
        public Guid InstructorId { get; set; }
    }
}
