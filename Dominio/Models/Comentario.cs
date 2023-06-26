
namespace Dominio.Models
{
    public class Comentario
    {
        public int     Id               {get;set;}
        public string  Alumno           {get;set;}
        public int     Puntaje          {get;set;}
        public string  ComentarioTexto  {get;set;}

        public int     CursoId          {get;set;}
        public Curso   Curso            {get;set;}
    }
}