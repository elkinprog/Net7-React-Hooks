using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public class CreateCursosComand
    {

        public class CreateCursosComandRequest : IRequest
        {
            public string   Titulo           {get;set;} = default!;
            public string   Descripcion      {get;set;} = default!;
            public DateTime FechaPublicacion {get;set;} = default!;
        };

        public class CreateCursos : IRequestHandler<CreateCursosComandRequest>
        {
            private readonly CursosOnlineContext context;

            public CreateCursos(CursosOnlineContext _context)
            {
                this.context = _context;
            }
            public async Task Handle(CreateCursosComandRequest request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,  
                };
                context.Curso.Add(curso);
                await context.SaveChangesAsync();
                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "se creo curso con id " + curso.Id);
            }
        }
    }
}
