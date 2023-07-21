using AutoMapper;
using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public class GetCursoByIdQuery
    {
        public class GetCursoByIdQueryRequest : IRequest<CursoDto>
        {

            public Guid Id { get; set; }

            public class GetCursoByIdQueryHandler : IRequestHandler<GetCursoByIdQueryRequest, CursoDto>
            {
                private readonly CursosOnlineContext _context;
                private readonly IMapper _mapper;

                public GetCursoByIdQueryHandler(CursosOnlineContext context, IMapper _mapper)
                {
                    this._context = context;
                    this._mapper = _mapper; 
                }

                public async Task<CursoDto> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
                {
                    var curso = await _context.Curso
                        .Include(x => x.InstructoresLink)
                        .ThenInclude(x => x.Instructor)
                        .FirstOrDefaultAsync(x => x.Id == request.Id);
                    

                    if (curso == null)
                    {
                        throw new GenericResponse(HttpStatusCode.NotFound, "Algo salió mal!", "No existe curso con id " + request.Id);
                    }

                    var cursoDto = _mapper.Map<Curso, CursoDto>(curso);

                    await _context.SaveChangesAsync();
                    return cursoDto;
                }
            }
        }
    }


}
