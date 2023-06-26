using MediatR;
using Persistencia; 

namespace Aplicacion.Cursos
{
    public class UpdateCurso
    {

        public class UpdateCursoRequest: IRequest
        {
            public int Id { get; set; }
            public string Titulo { get; set; } = default!;
            public string Descripcion { get; set; } = default!;
            public DateTime? FechaPublicacion { get; set; }
            //public virtual byte[] FotoPortada { get; set; }
        }

        public class UpdateCursoRequestHandler : IRequestHandler<UpdateCursoRequest>
        {
            private readonly CursosOnlineContext _context;

            public UpdateCursoRequestHandler(CursosOnlineContext context)
            {
                this._context = context;    
            }
            public async Task<Unit> Handle(UpdateCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await  _context.Curso.FindAsync(request.Id);
                if (curso == null) 
                {
                    throw new Exception("El curso no existe");
                }
                curso.Titulo= request.Titulo ?? curso.Titulo;  
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                _context.Curso.Add(curso);
                var result = await _context.SaveChangesAsync();

                if (result >0 )
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar curso");




            }

        }
    }
}
