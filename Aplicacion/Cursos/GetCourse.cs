﻿using AutoMapper;
using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.ContextConexion;

namespace Aplicacion.Cursos
{
    public class GetCourse  
    { 
        public class GetCursoRequest: IRequest <List<CursoDto>>{}

        public class GetCursoHandler : IRequestHandler<GetCursoRequest, List<CursoDto>>
        {
            private readonly CursosOnlineContext context;
            private readonly IMapper _mapper;

            public GetCursoHandler(CursosOnlineContext _context, IMapper mapper)
            {
                this.context = _context;
                this._mapper = mapper;
            }

          
          public async Task<List<CursoDto>>Handle(GetCursoRequest request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso
                   .Include(x=> x.ComentarioLista)
                   .Include(x => x.PrecioPromocion)
                   .Include(x => x.InstructoresLink)
                   .ThenInclude(x => x.Instructor).ToListAsync();
                    
                var cursoDto = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);

                return cursoDto;
            }
        }
    }
}
