namespace Dominio.Models
{
    public class Precio
    {
        public Guid     Id           {get;set;}
        public int      PrecioActual {get;set;}
        public int      Promocion    {get;set;}
        public Guid     CursoId      {get;set;}

        public Curso    Curso        {get;set;}
    }
}