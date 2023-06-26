using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class GetCursoQuery
    { 
        public class GetCursoQueryRequest: IRequest <List<Curso>>{}

        public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<Curso>>
        {
            private readonly CursosOnlineContext context;

            public GetCursoQueryHandler(CursosOnlineContext _context)
            {
                this.context = _context;
            }

            async Task<List<Curso>> IRequestHandler<GetCursoQueryRequest, List<Curso>>.Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso.ToListAsync();
                return cursos;
            }
        }
    }
}
