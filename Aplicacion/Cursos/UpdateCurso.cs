using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

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

        public class UpdateCursoRequestHandler : IRequestHandler<UpdateCursoRequest, Curso>
        {
            private readonly CursosOnlineContext _context;

            public UpdateCursoRequestHandler(CursosOnlineContext context)
            {
                this._context = context;
            }

            public async  Task<Curso> Handle(UpdateCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FirstOrDefaultAsync(p => p.Id == request.Id);

                if (curso == null) 
                {
                    throw new Exception("No existe curso con ese Id");
                };

                curso.Titulo = request.Titulo;
                curso.Descripcion = request.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion;

                await _context.SaveChangesAsync();
                return curso;
            }



        }
    }
}
