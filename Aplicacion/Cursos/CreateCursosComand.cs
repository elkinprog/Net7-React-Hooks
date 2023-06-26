using Dominio.Models;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class CreateCursosComand
    {

        public class CreateCursosComandRequest:IRequest {

            public string Titulo { get; set; } = default!;
            public string Descripcion { get; set; } = default!;
            public DateTime FechaPublicacion { get; set; }
            //public virtual byte[] FotoPortada { get; set; }
        };

        public class CreateCursosComandRequestHandle : IRequestHandler<CreateCursosComandRequest>
        {
            private readonly CursosOnlineContext context;

            public CreateCursosComandRequestHandle(CursosOnlineContext _context)
            {
                this.context = _context;
            }

            public async Task<Unit> Handle(CreateCursosComandRequest request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    //FotoPortada = request.FotoPortada,
                };
                context.Curso.Add(curso);
                var result = await context.SaveChangesAsync();

                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar curso");

            }
        }
    }
}
