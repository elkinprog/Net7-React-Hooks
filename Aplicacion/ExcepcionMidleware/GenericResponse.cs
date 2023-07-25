using System.Net;

namespace Aplicacion.ExcepcionMidleware
{
    public class GenericResponse : Exception
    {

        public HttpStatusCode Codigo { get; }
        public string Titulo;
        public string Mensaje;

        public GenericResponse(HttpStatusCode codigo, string titulo, string mensaje)
        {
            Codigo = codigo;
            Titulo = titulo;
            Mensaje = mensaje;
        }


    }
}
