
namespace Dominio.Models
{
    public class Instructor
    {
        public Guid             Id           {get;set;}
        public string           Nombre       {get;set;}
        public string           Apellido     {get;set;}
        public string           Grado        {get;set;}
        public virtual byte[]   FotoPerfil   {get;set;}

        public ICollection<CursoInstructor> CursoLink {get;set;}
        
    }
}
