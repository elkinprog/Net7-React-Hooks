
namespace Dominio.Models
{
    public class Instructor
    {
        public int      Id           {get;set;}
        public string   Nombre       {get;set;}
        public string   Apellido     {get;set;}
        public int      Grado        {get;set;}
        public virtual byte[]   FotoPerfil   {get;set;}

        public IList <CursoInstructor>? CursoLink {get;set;}
        
    }
}
