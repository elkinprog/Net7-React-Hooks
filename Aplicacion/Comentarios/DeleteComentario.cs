using Aplicacion.ExcepcionMidleware;
using Dominio.Dtos;
using MediatR;
using Persistencia.ContextConexion;
using System.Net;

namespace Aplicacion.Comentarios
{
    public class DeleteComentario
    {
        public class Ejecuta: IRequest<ComentarioDto>
        {
            public Guid Id { get; set; }
        }

        public class EliminaHandler : IRequestHandler<Ejecuta,ComentarioDto>
        {
            private readonly CursosOnlineContext _context;

            public EliminaHandler(CursosOnlineContext context)
            {
                this._context = context;    
            }

            public async Task<ComentarioDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var comentario = await _context.Comentario.FindAsync(request.Id);

                if (comentario == null)
                {
                    throw new GenericResponse(HttpStatusCode.NotFound, "Algo sucedió!", "No existe comentario con este id");
                }

                _context.Remove(comentario);
                await _context.SaveChangesAsync();


                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "se eliminó comentario");


            }
        }
    }



}
