﻿using Aplicacion.ManejadorErrores;
using Dominio.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public class UpdateCurso
    {

        public class UpdateCursoRequest : IRequest<Curso>
        {
            public int      Id               {get;set; } 
            public string   Titulo           {get;set; } 
            public string   Descripcion      {get;set; } 
            public DateTime FechaPublicacion {get;set; }
        }

        public class UpdateCursoRequetVAlidation : AbstractValidator<UpdateCursoRequest>
        {
            public UpdateCursoRequetVAlidation()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
        }

        public class UpdateCursoRequestHandler : IRequestHandler<UpdateCursoRequest, Curso>
        {
            private readonly CursosOnlineContext _context;

            public UpdateCursoRequestHandler(CursosOnlineContext context)
            {
                this._context = context;
            }

            public async  Task<Curso> Handle(UpdateCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.Id);

                if (curso == null)
                {
                    throw new ExcepcionError(HttpStatusCode.NotFound, "Algo salió mal!", "No existe curso con id " + request.Id);
                }

                curso.Id = request.Id;
                curso.Titulo = request.Titulo;
                curso.Descripcion = request.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion;

                await _context.SaveChangesAsync();
                throw new ExcepcionError(HttpStatusCode.OK, "Bien echo!", "se actualizo curso con id " + request.Id);   
            }

           
        }
    }
}
