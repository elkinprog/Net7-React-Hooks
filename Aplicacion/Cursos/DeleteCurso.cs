using Dominio.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public  class DeleteCurso: IRequest<Curso>
    {
        public int Id { get; set; }

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
            var curso  = await _context.Curso.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (curso is null)
            {
                throw new Exception("No existe curso con ese Id");
            }
          
            _context.Curso.Remove(curso);   
            await _context.SaveChangesAsync();
            return curso;
        }
        
           

    }
}

