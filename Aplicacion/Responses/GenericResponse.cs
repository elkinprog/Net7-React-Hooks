using System.Net;

namespace WebApi.Responses
{
    public class GenericResponse : Exception
    {

        public HttpStatusCode Codigo { get;}
        public string Titulo;
        public string Mensaje;        

        public GenericResponse(HttpStatusCode codigo, string titulo, string mensaje)
        {
            this.Codigo  = codigo;
            this.Titulo  = titulo;
            this.Mensaje = mensaje;
        }


    }
}
