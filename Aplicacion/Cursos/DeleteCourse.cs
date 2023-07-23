using MediatR;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public  class DeleteCourse: IRequest<CursoDto>
    {
        public Guid Id { get; set; } 
    }
   

    public class DeleteCursoHandler : IRequestHandler<DeleteCourse, CursoDto>
    {
        private readonly CursosOnlineContext _context;

        public DeleteCursoHandler(CursosOnlineContext _context)
        {
            this._context = _context;
        }

        public async Task<CursoDto>Handle(DeleteCourse request, CancellationToken cancellationToken)
        {
            var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
            foreach (var instructor in instructoresDB)
            {
                _context.CursoInstructor.Remove(instructor);
            }

            var comentariosDB = _context.Comentario.Where(x => x.CursoId == request.Id);
            foreach (var comentarios in comentariosDB)
            {
                _context.Comentario.Remove(comentarios);
            } 

            var precioDB = _context.Precio.Where(x => x.CursoId == request.Id);
            foreach (var precios in precioDB)
            {
                _context.Precio.Remove(precios);
            }

            var curso = await _context.Curso.FindAsync(request.Id);

            if (curso == null)
            {
                throw new GenericResponse(HttpStatusCode.NotFound, "Algo salió mal!", "No existe curso con este id");
            }

            _context.Curso.Remove(curso);

            await _context.SaveChangesAsync();

            throw new GenericResponse(HttpStatusCode.OK, "Bien echo", "Se elimino curso");

        }
    }
}

