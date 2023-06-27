using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                private readonly CursosOnlineContext _context;

                public GetCursoByIdQueryHandler(CursosOnlineContext context)
                {
                    this._context = context;
                }

                public async Task<Curso?> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
                {

                    var curso = await  _context.Curso.FirstOrDefaultAsync(p => p.Id == request.Id);

                    if (curso is null)
                    {
                        throw new Exception("No existe curso con ese Id");
                    }

                    return curso;
                }
            }
        }
    }


}
