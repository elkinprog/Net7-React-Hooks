using Aplicacion.ManejadorErrores;
using Dominio.Models;
using MediatR;
using Persistencia;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;

namespace Aplicacion.Cursos
{
    public  class DeleteCurso: IRequest<Curso>
    {
        public int Id { get; set; }

        public static explicit operator bool(DeleteCurso v)
        {
            throw new NotImplementedException();
        }
    }
   

    public class DeleteCursoHandler : IRequestHandler<DeleteCurso, Curso>
    {
        private readonly CursosOnlineContext _context;

        public DeleteCursoHandler(CursosOnlineContext _context)
        {
            this._context = _context;
        }

        public async Task<Curso> Handle(DeleteCurso request, CancellationToken cancellationToken)
        {
            var curso  = await _context.Curso.FindAsync(request.Id);

            if (curso == null)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "Algo salió mal!", "No existe curso con id " + request.Id);
            }

            _context.Curso.Remove(curso);   
            await _context.SaveChangesAsync();
            return curso;
        }
    }
}

