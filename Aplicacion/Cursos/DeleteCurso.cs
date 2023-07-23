using MediatR;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public  class DeleteCurso: IRequest<CursoDto>
    {
        public Guid Id { get; set; } 
    }
   

    public class DeleteCursoHandler : IRequestHandler<DeleteCurso, CursoDto>
    {
        private readonly CursosOnlineContext _context;

        public DeleteCursoHandler(CursosOnlineContext _context)
        {
            this._context = _context;
        }

        public async Task<CursoDto>Handle(DeleteCurso request, CancellationToken cancellationToken)
        {
            var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);

            foreach (var instructor in instructoresDB)
            {
                _context.CursoInstructor.Remove(instructor);
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

