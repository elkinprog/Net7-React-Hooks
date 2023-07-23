using Dominio.Models;
using FluentValidation;
using MediatR;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Cursos
{
    public class UpdateCourse
    {

        public class UpdateCursoRequest : IRequest<CursoDto>
        {

            public Guid       Id               {get;set;} 
            public string     Titulo           {get;set;} 
            public string     Descripcion      {get;set;} 
            public DateTime   FechaPublicacion {get;set;}
            public List<Guid> ListaInstructor  {get;set;}
            public int?       PrecioActual     {get;set;}
            public int?       Promocion        {get;set;}
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

        public class UpdateCursoRequestHandler : IRequestHandler<UpdateCursoRequest, CursoDto>
        {
            private readonly CursosOnlineContext _context;

            public UpdateCursoRequestHandler(CursosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<CursoDto> Handle(UpdateCursoRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.Id);

                if (curso == null)
                {
                    throw new GenericResponse(HttpStatusCode.NotFound, "Algo salió mal!", "No existe curso con id " + request.Id);
                }


                curso.Titulo = request.Titulo;
                curso.Descripcion = request.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion; 


                // Logica para actualizar precio del curso
                var precioEntidad = _context.Precio.Where(x=> x.CursoId == curso.Id).FirstOrDefault();
                if (precioEntidad != null)
                {
                    precioEntidad.Promocion     = request.Promocion     ?? precioEntidad.Promocion;
                    precioEntidad.PrecioActual  = request.PrecioActual  ?? precioEntidad.PrecioActual;
                }
                else
                {
                    precioEntidad = new Precio
                    {
                        Id           = Guid.NewGuid(),
                        PrecioActual = request.PrecioActual ?? 0,
                        Promocion    = request.Promocion ?? 0,
                        CursoId      = curso.Id
                    };
                   await _context.Precio.AddAsync(precioEntidad);
                }


                if (request.ListaInstructor!=null)
                {
                    if (request.ListaInstructor.Count > 0)
                    {
                        // Eliminar los instructores actuales en la base de datos
                        var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id).ToList();
                        foreach (var instructorEliminar in instructoresDB)
                        {
                         _context.CursoInstructor.Remove(instructorEliminar);
                        } 
                        //Fin del procedimiemto para eliminar instructores


                        // Procedimiento para agregar instructores que provienen del cliente
                        foreach (var id in request.ListaInstructor)
                        {
                            var nuevoInstructor = new CursoInstructor
                            {
                                CursoId = request.Id,
                                InstructorId = id
                            };
                            _context.CursoInstructor.Add(nuevoInstructor);
                        }
                        //Fin del prosedimiemto





                    }
                }



                await _context.SaveChangesAsync();
                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "Se actualizo curso");   
            }

           
        }
    }
}
