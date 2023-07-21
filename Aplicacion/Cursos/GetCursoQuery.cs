using AutoMapper;
using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class GetCursoQuery  
    { 
        public class GetCursoQueryRequest: IRequest <List<CursoDto>>{}

        public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<CursoDto>>
        {
            private readonly CursosOnlineContext context;
            private readonly IMapper _mapper;

            public GetCursoQueryHandler(CursosOnlineContext _context, IMapper mapper)
            {
                this.context = _context;
                this._mapper = mapper;
            }

          

          public async Task<List<CursoDto>>Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso
                   .Include(x => x.InstructoresLink)
                   .ThenInclude(x => x.Instructor).ToListAsync();

                var cursoDto = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);

                return cursoDto;
            }
        }
    }
}
