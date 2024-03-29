﻿
using Aplicacion.ExcepcionMidleware;
using AutoMapper;
using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.ContextConexion;
using System.Net;

namespace Aplicacion.Cursos
{
    public class GetCourseId
    {
        public class GetCursoById : IRequest<CursoDto>
        {
            public Guid Id { get; set; 
        }

            public class GetCursoIdHandler : IRequestHandler<GetCursoById, CursoDto>
            {
                private readonly CursosOnlineContext _context;
                private readonly IMapper _mapper;

                public GetCursoIdHandler(CursosOnlineContext context, IMapper _mapper)
                {
                    this._context = context;
                    this._mapper = _mapper; 
                }

                public async Task<CursoDto> Handle(GetCursoById request, CancellationToken cancellationToken)
                {
                    var curso = await _context.Curso
                        .Include(x => x.ComentarioLista)
                        .Include(x => x.PrecioPromocion)
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
