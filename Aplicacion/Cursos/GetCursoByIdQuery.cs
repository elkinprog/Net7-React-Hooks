using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class GetCursoByIdQuery
    {
        public class GetCursoByIdQueryRequest : IRequest<Curso>
        {

            public int Id { get; set; }

            public class GetCursoByIdQueryHandler : IRequestHandler<GetCursoByIdQueryRequest, Curso>
            {
                private readonly CursosOnlineContext context;
                public GetCursoByIdQueryHandler(CursosOnlineContext _context)
                {
                    this.context = _context;
                }
                public async Task<Curso> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
                {
                    var curso = await context.Curso.FindAsync(request.Id);
                    return curso;

                }
            }
        }
    }


}
