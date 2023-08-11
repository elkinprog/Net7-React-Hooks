using Aplicacion.ExcepcionMidleware;
using Dominio.Models;
using MediatR;
using Persistencia.ContextConexion;
using System.Net;

namespace Aplicacion.Comentarios
{
    public  class CreateComentario
    {
    
        public class NuevoComentario: IRequest
        {
            public string  Alumno          {get;set;}
            public int     Puntaje         {get;set;}
            public string  ComentarioTexto {get;set;}
            public Guid    CursoId         {get;set;}
        }


        public class ComentarioHandler : IRequestHandler<NuevoComentario>
        {
            private readonly CursosOnlineContext _context;
            public ComentarioHandler(CursosOnlineContext context)
            {
               this._context = context; 
            }
                
            public async  Task Handle(NuevoComentario request, CancellationToken cancellationToken)
            {
                var comentario = new Comentario
                {
                    Id     = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    Puntaje= request.Puntaje,   
                    ComentarioTexto = request.ComentarioTexto,
                    CursoId = request.CursoId
                };
                _context.Comentario.Add(comentario);
               var resultados = await  _context.SaveChangesAsync();

                if (resultados >  0)
                {
                    throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "se creo comentario");
                }

                throw new GenericResponse(HttpStatusCode.NotFound, "Algo sucedió!", "No se creo comentario");

            }
        }





    }
}
