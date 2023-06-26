namespace Dominio.Models
{
    public class Precio
    {
        public int      Id           {get;set;}
        public int      PrecioActual {get;set;}
        public int      Promocion    {get;set;}

        public int      CursoId      {get;set;}
        public Curso    Curso        {get;set;}
    }
}