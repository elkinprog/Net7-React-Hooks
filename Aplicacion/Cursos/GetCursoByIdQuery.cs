using Aplicacion.ManejadorErrores;
using Azure;
using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;

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

                public async Task<Curso> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
                {
                    var curso = await _context.Curso.FindAsync(request.Id);

                    //var validacion = await _context.Curso.FirstOrDefaultAsync(p => p.Id > 0);

                    if (curso == null)
                    {
                        throw new ExcepcionError(HttpStatusCode.NotFound, "Algo salió mal!", "No existe curso con id " + request.Id);
                    }

                    await _context.SaveChangesAsync();
                    return curso;
                }
            }
        }
    }


}
