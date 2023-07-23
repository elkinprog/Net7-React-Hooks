namespace Dominio.Dtos
{
    public class ComentarioDto
    {
        public Guid     Id              {get;set;}
        public string   Alumno          {get;set;}
        public int      Puntaje         {get;set;}
        public string   ComentarioTexto {get;set;}
        public Guid     CursoId         {get;set;}

    }
}
