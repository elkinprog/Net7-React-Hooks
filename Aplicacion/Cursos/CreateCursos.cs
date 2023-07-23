using Dominio.Models;
using MediatR;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public class CreateCursos
    {

        public class CreateCursosComand : IRequest
        {
            public string     Titulo            {get;set;}
            public string     Descripcion       {get;set;}
            public DateTime   FechaPublicacion  {get;set;}
            public List<Guid> ListaInstructor   {get;set;}
                
            public int        PrecioActual      {get;set;}
            public int        Promocion         {get;set;}

        };

        public class CreateCurso : IRequestHandler<CreateCursosComand>
        {
            private readonly CursosOnlineContext _context;
            
            public CreateCurso(CursosOnlineContext context)
            {
                this._context = context;
            }
            public async Task Handle(CreateCursosComand request, CancellationToken cancellationToken)
            {
                Guid _cursoId = Guid.NewGuid();
                var curso = new Curso
                {
                    Id = _cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                };
                 _context.Curso.Add(curso);


              
                if (request.ListaInstructor!= null)
                {
                    foreach (var id in request.ListaInstructor)
                    {
                       var  CursoInstructor = new CursoInstructor
                        {
                            CursoId= _cursoId,
                            InstructorId = id,

                        }; 

                        _context.CursoInstructor.Add(CursoInstructor);
                    }
                }


                // Agregar logica para insertar precio del curso

                var precioEntidad = new Precio
                {
                    CursoId         = _cursoId,
                    PrecioActual    = request.PrecioActual,
                    Promocion       = request.Promocion,
                    Id              = Guid.NewGuid(),
                };
                _context.Precio.Add(precioEntidad);




                await _context.SaveChangesAsync();

                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "se creo curso con id " + curso.Id);
            }

            
        }
    }
}
